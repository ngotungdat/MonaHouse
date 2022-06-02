using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.BaseAPI.Controllers;
using Sample.Entities;
using Sample.Entities.Search;
using Sample.Extensions;
using Sample.Interface.Services;
using Sample.Models;
using Sample.Request;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Description("Quản lý gói phần mềm cho thuê của ngoài dùng")]
    [Authorize]
    public class PackageOfUserController : BaseController<PackageOfUser, PackageOfUserModel, PackageOfUserRequest, PackageOfUserSearch>
    {
        protected IPackageOfUserService PackageOfUserService;
        protected IPackageService packageService;
        protected IUserService userService;
        private IConfiguration configuration;
        public PackageOfUserController(IServiceProvider serviceProvider, ILogger<BaseController<PackageOfUser, PackageOfUserModel, PackageOfUserRequest, PackageOfUserSearch>> logger
            , IConfiguration configuration
            , IUserService userService
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IPackageOfUserService>();
            this.PackageOfUserService = serviceProvider.GetRequiredService<IPackageOfUserService>();
            this.packageService = serviceProvider.GetRequiredService<IPackageService>();
            this.userService = serviceProvider.GetRequiredService<IUserService>();
            this.configuration = configuration;
        }
        [HttpPut]
        [Route("AcceptPackage")]
        [AppAuthorize(new string[] { CoreContants.Update })]
        public async Task<AppDomainResult> AcceptPackage([FromBody] PackageOfUserRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                var item = mapper.Map<PackageOfUser>(itemModel);
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                if (item != null)
                {
                    // Kiểm tra item có tồn tại chưa?
                    //item.TenantId = user.TenantId;
                    var messageUserCheck = await this.domainService.GetExistItemMessage(item);
                    if (!string.IsNullOrEmpty(messageUserCheck))
                        throw new AppException(messageUserCheck);
                    success = await this.PackageOfUserService.AppceptPackage(item);
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
        [HttpPut]
        [Route("ExtendPackage")]
        [AppAuthorize(new string[] { CoreContants.Update })]
        public async Task<AppDomainResult> ExtendPackage([FromBody] PackageOfUserRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                var item = mapper.Map<PackageOfUser>(itemModel);
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                if (item != null)
                {
                    // Kiểm tra item có tồn tại chưa?
                    //item.TenantId = user.TenantId;
                    var messageUserCheck = await this.domainService.GetExistItemMessage(item);
                    if (!string.IsNullOrEmpty(messageUserCheck))
                        throw new AppException(messageUserCheck);
                    success = await this.PackageOfUserService.ExtendPackage(item);
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

        [HttpGet]
        [Route("ReportPackageOfUser")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> ReportPackageOfUser([FromQuery] int Year, int PackageId)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            if (ModelState.IsValid)
            {
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                if (user == null /*|| user.RoleNumber != 0 || user.RoleNumber != 3 || user.RoleNumber != 4*/)
                {
                    throw new Exception("Không phải là admin, CSKH, chủ trọ, nhân viên chủ trọ");
                }
                List<ReportPackageOfUser> ReportPackageOfUsers = await PackageOfUserService.ReportPackageOfUser(Year, PackageId);

                appDomainResult = new AppDomainResult
                {
                    Data = ReportPackageOfUsers,
                    Success = true,
                    ResultCode = (int)HttpStatusCode.OK,
                    ResultMessage = ApiMessage.GETALL_SUCCESS
                };
            }
            else
            {
                throw new AppException(ModelState.GetErrorMessage());
            }
            return appDomainResult;
        }
    }
}
