using Sample.Entities.DomainEntities;
using Sample.Extensions;
using Sample.Interface;
using Sample.Interface.Services;
using Sample.Models.DomainModels;
using Sample.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Sample.Interface.Services.DomainServices;
using Sample.Request.DomainRequests;

namespace Sample.BaseAPI.Controllers
{
    [ApiController]
    public abstract class BaseController<E, T, R, F> : ControllerBase where E : Entities.DomainEntities.AppDomain where T : AppDomainModel where R : AppDomainRequest where F : BaseSearch, new()
    {
        protected readonly ILogger<BaseController<E, T, R, F>> logger;
        protected readonly IServiceProvider serviceProvider;
        protected readonly IMapper mapper;
        protected IDomainService<E, F> domainService;
        protected IWebHostEnvironment env;
        private readonly IUserService userService;

        public BaseController(IServiceProvider serviceProvider, ILogger<BaseController<E, T, R, F>> logger, IWebHostEnvironment env)
        {
            this.env = env;
            this.logger = logger;
            this.mapper = serviceProvider.GetService<IMapper>();
            this.serviceProvider = serviceProvider;
            userService = serviceProvider.GetRequiredService<IUserService>();
        }

        /// <summary>
        /// Lấy thông tin theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public virtual async Task<AppDomainResult> GetById(int id)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            //if (id == 0)
            //{
            //    throw new KeyNotFoundException("id không tồn tại");
            //}
            var item = await this.domainService.GetByIdAsync(id);
            if (item != null)
            {
                var itemModel = mapper.Map<T>(item);
                appDomainResult = new AppDomainResult()
                {
                    Success = true,
                    Data = itemModel,
                    ResultCode = (int)HttpStatusCode.OK,
                    ResultMessage = ApiMessage.GETBYID_SUCCESS
                };
            }
            else
            {
                throw new KeyNotFoundException("Item không tồn tại");
            }
            return appDomainResult;
        }

        /// <summary>
        /// Thêm mới item
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AppAuthorize(new string[] { CoreContants.AddNew })]
        public virtual async Task<AppDomainResult> AddItem([FromBody] R itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                var item = mapper.Map<E>(itemModel);
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                if (item != null)
                {
                    item.TenantId = user.TenantId;
                    item.CreatedBy = user.UserName;
                    // Kiểm tra item có tồn tại chưa?
                    var messageUserCheck = await this.domainService.GetExistItemMessage(item);
                    if (!string.IsNullOrEmpty(messageUserCheck))
                        throw new AppException(messageUserCheck);
                    success = await this.domainService.CreateAsync(item);
                    if (success)
                    {
                        appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                        appDomainResult.ResultMessage = ApiMessage.CREATE_SUCCESS;
                    }
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

        [HttpPut]
        [AppAuthorize(new string[] { CoreContants.Update })]
        public virtual async Task<AppDomainResult> UpdateItem([FromBody] R itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                var item = mapper.Map<E>(itemModel);
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                if (item != null)
                {
                    // Kiểm tra item có tồn tại chưa?
                    item.TenantId = user.TenantId;
                    var messageUserCheck = await this.domainService.GetExistItemMessage(item);
                    if (!string.IsNullOrEmpty(messageUserCheck))
                        throw new AppException(messageUserCheck);
                    success = await this.domainService.UpdateAsync(item);
                    if (success)
                    {
                        appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                        appDomainResult.ResultMessage = ApiMessage.UPDATE_SUCCESS;
                    }
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
        /// Xóa item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [AppAuthorize(new string[] { CoreContants.Delete })]
        public virtual async Task<AppDomainResult> DeleteItem(int id)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            bool success = await this.domainService.DeleteAsync(id);
            if (success)
            {
                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                appDomainResult.Success = success;
                appDomainResult.ResultMessage = ApiMessage.DELETE_SUCCESS;
            }
            else
                throw new Exception("Lỗi trong quá trình xử lý");

            return appDomainResult;
        }

        /// <summary>
        /// Lấy danh sách item phân trang
        /// </summary>
        /// <param name="baseSearch"></param>
        /// <returns></returns>
        [HttpGet]
        [AppAuthorize(new string[] { CoreContants.ViewAll })]
        public virtual async Task<AppDomainResult> Get([FromQuery] F baseSearch)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            if (ModelState.IsValid)
            {
                baseSearch.SearchContent = baseSearch.SearchContent ?? "";
                baseSearch.TenantId = userService.GetById(LoginContext.Instance.CurrentUser.UserId).TenantId;
                PagedList<E> pagedData = await this.domainService.GetPagedListData(baseSearch);
                PagedList<T> pagedDataModel = mapper.Map<PagedList<T>>(pagedData);
                appDomainResult = new AppDomainResult
                {
                    Data = pagedDataModel,
                    Success = true,
                    ResultCode = (int)HttpStatusCode.OK,
                    ResultMessage = ApiMessage.GETALL_SUCCESS
                };
            }
            else
                throw new AppException(ModelState.GetErrorMessage());

            return appDomainResult;
        }

        /// <summary>
        /// Lấy thông tin quyền của chức năng
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-permission-detail")]
        public virtual async Task<AppDomainResult> GetPermission()
        {
            List<int> permissionIds = new List<int>();
            bool isViewAll = await this.userService.HasPermission(LoginContext.Instance.CurrentUser.UserId, ControllerContext.ActionDescriptor.ControllerName
                    , new List<string>() { CoreContants.ViewAll });
            if (isViewAll) permissionIds.Add((int)CoreContants.PermissionContants.ViewAll);

            bool isView = await this.userService.HasPermission(LoginContext.Instance.CurrentUser.UserId, ControllerContext.ActionDescriptor.ControllerName
                    , new List<string>() { CoreContants.View });
            if (isView) permissionIds.Add((int)CoreContants.PermissionContants.View);

            bool isAddNew = await this.userService.HasPermission(LoginContext.Instance.CurrentUser.UserId, ControllerContext.ActionDescriptor.ControllerName
                    , new List<string>() { CoreContants.AddNew });
            if (isAddNew) permissionIds.Add((int)CoreContants.PermissionContants.AddNew);

            bool isUpdate = await this.userService.HasPermission(LoginContext.Instance.CurrentUser.UserId, ControllerContext.ActionDescriptor.ControllerName
                    , new List<string>() { CoreContants.Update });
            if (isUpdate) permissionIds.Add((int)CoreContants.PermissionContants.Update);

            bool isDelete = await this.userService.HasPermission(LoginContext.Instance.CurrentUser.UserId, ControllerContext.ActionDescriptor.ControllerName
                    , new List<string>() { CoreContants.Delete });
            if (isDelete) permissionIds.Add((int)CoreContants.PermissionContants.Delete);


            bool isDownload = await this.userService.HasPermission(LoginContext.Instance.CurrentUser.UserId, ControllerContext.ActionDescriptor.ControllerName
                    , new List<string>() { CoreContants.Download });
            if (isDownload) permissionIds.Add((int)CoreContants.PermissionContants.Download);


            bool isExport = await this.userService.HasPermission(LoginContext.Instance.CurrentUser.UserId, ControllerContext.ActionDescriptor.ControllerName
                    , new List<string>() { CoreContants.Export });
            if (isExport) permissionIds.Add((int)CoreContants.PermissionContants.Export);


            bool isImport = await this.userService.HasPermission(LoginContext.Instance.CurrentUser.UserId, ControllerContext.ActionDescriptor.ControllerName
                   , new List<string>() { CoreContants.Import });
            if (isImport) permissionIds.Add((int)CoreContants.PermissionContants.Import);


            bool isUpload = await this.userService.HasPermission(LoginContext.Instance.CurrentUser.UserId, ControllerContext.ActionDescriptor.ControllerName
                   , new List<string>() { CoreContants.Upload });
            if (isUpload) permissionIds.Add((int)CoreContants.PermissionContants.Upload);

            return new AppDomainResult()
            {
                Data = permissionIds,
                Success = true,
                ResultCode = (int)HttpStatusCode.OK
            };
        }
    }
}
