using Sample.Entities;
using Sample.Extensions;
using Sample.Interface.Services;
using Sample.Interface.Services.Auth;
using Sample.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Sample.Models;
//using Sample.Models.Auth;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Sample.Request;
using Sample.Entities.Search;

namespace Sample.BaseAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Description("Quản lý thông tin người dùng")]
    [Authorize]
    public class UserController : BaseController<Users, UserModel, UserRequest, UserSearch>
    {
        protected IUserService userService;
        protected IUserInGroupService userInGroupService;
        protected IUserGroupService userGroupService;
        private IConfiguration configuration;
        public UserController(IServiceProvider serviceProvider, ILogger<BaseController<Users, UserModel, UserRequest, UserSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IUserService>();
            this.userService = serviceProvider.GetRequiredService<IUserService>();
            userInGroupService = serviceProvider.GetRequiredService<IUserInGroupService>();
            userGroupService = serviceProvider.GetRequiredService<IUserGroupService>();
            this.configuration = configuration;
        }

        /// <summary>
        /// Lấy thông tin theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public override async Task<AppDomainResult> GetById(int id)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            //if (id == 0)
            //{
            //    throw new KeyNotFoundException("id không tồn tại");
            //}
            var item = await this.domainService.GetByIdAsync(id);

            if (item != null)
            {
                if (LoginContext.Instance.CurrentUser != null)
                {
                    var itemModel = mapper.Map<UserModel>(item);
                    //itemModel.ConfirmPassWord = item.Password;
                    //var userInGroups = await userInGroupService.GetAsync(e => !e.Deleted);
                    //if (userInGroups != null)
                    //{
                    //    foreach (var userInGroup in userInGroups)
                    //    {
                    //        var userGroup = await userGroupService.GetByIdAsync(userInGroup.UserGroupId);
                    //        itemModel.UserGroup = mapper.Map<UserGroupModel>(userGroup);
                    //    }
                    //}
                    //var userLevel = await userLevelService.GetByIdAsync(itemModel.LevelId ?? 0);
                    //if (userLevel != null)
                    //    itemModel.UserLevel = mapper.Map<UserLevelModel>(userLevel);

                    //var saler = await this.domainService.GetByIdAsync(itemModel.SaleId ?? 0);
                    //if (saler != null)
                    //    itemModel.Saler = mapper.Map<UserModel>(saler);

                    //var orderer = await this.domainService.GetByIdAsync(itemModel.DatHangId ?? 0);
                    //if (orderer != null)
                    //    itemModel.Orderer = mapper.Map<UserModel>(orderer);

                    appDomainResult = new AppDomainResult()
                    {
                        Success = true,
                        Data = itemModel,
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
                else throw new KeyNotFoundException("Không có quyền truy cập");
            }
            else
            {
                throw new KeyNotFoundException("Item không tồn tại");
            }
            return appDomainResult;
        }

        /// <summary>
        /// Lấy thông tin tất cả user 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllUser")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetAllUser()
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            
            var item = await this.domainService.GetAllAsync();

            if (item != null)
            {
                if (LoginContext.Instance.CurrentUser != null)
                {
                    var itemModel = mapper.Map<List<UserModel>>(item);
                    appDomainResult = new AppDomainResult()
                    {
                        Success = true,
                        Data = itemModel,
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
                else throw new KeyNotFoundException("Không có quyền truy cập");
            }
            else
            {
                throw new KeyNotFoundException("Item không tồn tại");
            }
            return appDomainResult;
        }

        
        [HttpGet]
        [Route("GetAllUserByTenantId")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetAllUserByTenantId([FromQuery] int TenantId)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            List<Users> items = (List<Users>)await this.domainService.GetAsync(p=>p.TenantId == TenantId);
            
            if (items != null)
            {
                if (LoginContext.Instance.CurrentUser != null)
                {
                    var itemModel = mapper.Map<List<UserModel>>(items);
                    appDomainResult = new AppDomainResult()
                    {
                        Success = true,
                        Data = itemModel,
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
                else throw new KeyNotFoundException("Không có quyền truy cập");
            }
            else
            {
                throw new KeyNotFoundException("Item không tồn tại");
            }
            return appDomainResult;
        }

        /// <summary>
        /// Cập nhật thông tin item
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [AppAuthorize(new string[] { CoreContants.Update })]
        public override async Task<AppDomainResult> UpdateItem([FromBody] UserRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                var item = mapper.Map<Users>(itemModel);
                item.DateOfBirth = itemModel.BirthDate;
                item.Updated = DateTime.UtcNow.AddHours(7);
                item.UpdatedBy = LoginContext.Instance.CurrentUser.UserName;
                if (itemModel.IsResetPassword)
                    item.Password = SecurityUtilities.HashSHA1(itemModel.NewPassWord);
                if (item != null)
                {
                    // Kiểm tra item có tồn tại chưa?
                    var messageUserCheck = await this.domainService.GetExistItemMessage(item);
                    if (!string.IsNullOrEmpty(messageUserCheck))
                        throw new AppException(messageUserCheck);
                    success = await userService.UpdateAsync(item);
                    if (success)
                        appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                    else
                        throw new Exception("Lỗi trong quá trình xử lý");
                    appDomainResult.Success = success;
                }
                else
                    throw new KeyNotFoundException("Item không tồn tại");
            }
            else
                throw new AppException(ModelState.GetErrorMessage());

            return appDomainResult;
        }

        /// <summary>
        /// Thêm mới user
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AppAuthorize(new string[] { CoreContants.AddNew })]
        public override async Task<AppDomainResult> AddItem([FromBody] UserRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {

                var item = mapper.Map<Users>(itemModel);
                item.Password = SecurityUtilities.HashSHA1(itemModel.Password);
                if (item != null)
                {
                    var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                    if (user != null)
                    {
                        item.TenantId = user.TenantId;
                        item.RoleNumber = itemModel.UserGroupIds[0];
                        item.DateOfBirth = itemModel.BirthDate;
                        item.UserGroupId= itemModel.UserGroupIds[0];
                        item.CitizenIdentification = itemModel.CitizenIdentification;
                    }
                    // Kiểm tra item có tồn tại chưa?
                    var messageUserCheck = await this.domainService.GetExistItemMessage(item);
                    if (!string.IsNullOrEmpty(messageUserCheck))
                        throw new AppException(messageUserCheck);
                    success = await userService.CreateAsync(item);
                    if (success)
                        appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                    else
                        throw new Exception("Lỗi trong quá trình xử lý");
                    appDomainResult.Success = success;
                }
                else
                    throw new AppException("Item không tồn tại");
            }
            else
            {
                throw new AppException(ModelState.GetErrorMessage());
            }
            return appDomainResult;
        }

        /// <summary>
        /// Lấy danh sách item phân trang
        /// </summary>
        /// <param name="baseSearch"></param>
        /// <returns></returns>
        [HttpGet]
        [AppAuthorize(new string[] { CoreContants.ViewAll })]
        public override async Task<AppDomainResult> Get([FromQuery] UserSearch baseSearch)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            if (ModelState.IsValid)
            {
                PagedList<Users> pagedData = await this.domainService.GetPagedListData(baseSearch);
                PagedList<UserModel> pagedDataModel = mapper.Map<PagedList<UserModel>>(pagedData);
                appDomainResult = new AppDomainResult
                {
                    Data = pagedDataModel,
                    Success = true,
                    ResultCode = (int)HttpStatusCode.OK
                };
            }
            else
                throw new AppException(ModelState.GetErrorMessage());

            return appDomainResult;
        }

        /// <summary>
        /// Lấy user hiện tại theo token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getcurrentuser")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetCurrentUser()
        {
            Users user = await domainService.GetByIdAsync(LoginContext.Instance.CurrentUser.UserId);
            return new AppDomainResult()
            {
                Data = user,
                ResultCode = (int)HttpStatusCode.OK,
                Success = true
            };
        }
    }
}
