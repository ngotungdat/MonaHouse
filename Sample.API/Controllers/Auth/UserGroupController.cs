using Sample.Entities;
using Sample.Extensions;
using Sample.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Sample.Models.Auth;
using Sample.Interface.Services.Auth;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Sample.BaseAPI.Controllers;
using Sample.Request.Auth;
using Sample.Entities.Auth;
using Sample.Entities.Search;

namespace Sample.API.Controllers.Auth
{
    [Route("api/user-group")]
    [ApiController]
    [Description("Nhóm người dùng")]
    [Authorize]
    public class UserGroupController : BaseCatalogueController<UserGroups, UserGroupModel, UserGroupRequest, UserInGroupSearch>
    {
        private readonly IUserInGroupService userInGroupService;
        private readonly IPermitObjectService permitObjectService;
        private readonly IUserGroupService userGroupService;
        private readonly IPermissionService permissionService;
        private readonly IPermitObjectPermissionService permitObjectPermissionService;

        public UserGroupController(IServiceProvider serviceProvider
            , ILogger<BaseController<UserGroups, UserGroupModel, UserGroupRequest, UserInGroupSearch>> logger
            , IWebHostEnvironment env)
            : base(serviceProvider, logger, env)
        {
            this.catalogueService = serviceProvider.GetRequiredService<IUserGroupService>();
            userInGroupService = serviceProvider.GetRequiredService<IUserInGroupService>();
            permissionService = serviceProvider.GetRequiredService<IPermissionService>();
            permitObjectPermissionService = serviceProvider.GetRequiredService<IPermitObjectPermissionService>();
            userGroupService = serviceProvider.GetRequiredService<IUserGroupService>();
            permitObjectService = serviceProvider.GetRequiredService<IPermitObjectService>();
        }

        

        /// <summary>
        /// Lấy thông tin nhóm theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public override async Task<AppDomainResult> GetById(int id)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            var item = await this.catalogueService.GetByIdAsync(id);
            if (item != null)
            {
                if (LoginContext.Instance.CurrentUser != null)
                {
                    var itemModel = mapper.Map<UserGroupModel>(item);
                    var userInGroups = await this.userInGroupService.GetAsync(e => !e.Deleted && e.UserGroupId == id);
                    //if (userInGroups != null)
                    //    itemModel.UserIds = userInGroups.Select(e => e.UserId).ToList();

                    var permitObjectPermissions = await this.permitObjectPermissionService.GetAsync(e => !e.Deleted && e.UserGroupId == id);
                    if (permitObjectPermissions != null)
                        itemModel.PermitObjectPermissions = mapper.Map<IList<PermitObjectPermissionModel>>(permitObjectPermissions);
                    appDomainResult = new AppDomainResult()
                    {
                        Success = true,
                        Data = itemModel,
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
                else throw new KeyNotFoundException("Item không tồn tại");
            }
            else
                throw new KeyNotFoundException("Item không tồn tại");

            return appDomainResult;
        }

        /// <summary>
        /// Lấy danh sách quyền
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-permissions")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetPermissionCatalogue()
        {
            var listPermissions = await this.permissionService.GetAsync(e => !e.Deleted);
            var listPermissionModels = mapper.Map<List<PermissionModel>>(listPermissions);
            return new AppDomainResult()
            {
                Data = listPermissionModels,
                Success = true,
                ResultCode = (int)HttpStatusCode.OK
            }; ;
        }

        /// <summary>
        /// Lấy danh sách phân trang người dùng thuộc nhóm
        /// </summary>
        /// <param name="searchUserInGroup"></param>
        /// <returns></returns>
        [HttpGet("get-user-in-groups")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetUserInGroups([FromQuery] UserInGroupSearch searchUserInGroup)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            var pagedList = await this.userInGroupService.GetPagedListData(searchUserInGroup);
            var pagedListModel = mapper.Map<PagedList<UserInGroupModel>>(pagedList);
            appDomainResult = new AppDomainResult()
            {
                Data = pagedListModel,
                Success = true,
                ResultCode = (int)HttpStatusCode.OK
            };
            return appDomainResult;
        }

        // By Bao nguyen
        [HttpGet]
        [Route("GetUserGroupByPermitObjectID")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public  async Task<AppDomainResult> GetUserGroupByPermitObjectID(string Id)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            var item = await userGroupService.GetUserGroupsByPermitObjectId(Id);
            if (item != null)
            {
                if (LoginContext.Instance.CurrentUser != null)
                {
                    var itemModel = mapper.Map<UserGroupModel>(item);
                    appDomainResult = new AppDomainResult()
                    {
                        Success = true,
                        Data = itemModel,
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
                else throw new KeyNotFoundException("Item không tồn tại");
            }
            else
                throw new KeyNotFoundException("Item không tồn tại");

            return appDomainResult;
        }
    }
}
