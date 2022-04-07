using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.BaseAPI.Controllers;
using Sample.Entities.DomainEntities;
using Sample.Extensions;
using Sample.Interface.Services;
using Sample.Interface.Services.Auth;
using Sample.Models;
using Sample.Models.Auth;
using Sample.Request.Auth;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sample.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    [Description("Hợp đồng mẫu")]
    [Authorize]
    public class PermitObjectPermissionController : BaseController<Entities.Auth.PermitObjectPermissions, PermitObjectPermissionModel, PermitObjectPermissionRequest, BaseSearch>
    {
        IUserService userService;
        IPermitObjectPermissionService permitObjectPermissionService;
        public PermitObjectPermissionController(IServiceProvider serviceProvider, ILogger<BaseController<Entities.Auth.PermitObjectPermissions, PermitObjectPermissionModel, PermitObjectPermissionRequest, BaseSearch>> logger
            ,IUserService UserService
            , IPermitObjectPermissionService PermitObjectPermissionService
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            userService = UserService;
            permitObjectPermissionService = PermitObjectPermissionService;

            this.domainService = serviceProvider.GetRequiredService<IPermitObjectPermissionService>();
        }

        [HttpPost]
        [Route("AddPermitObjectPermission")]
        [AppAuthorize(new string[] { CoreContants.AddNew })]
        public async Task<AppDomainResult> AddPermitObjectPermission([FromBody] PermitObjectPermissionAddNewRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                    success = await permitObjectPermissionService.AddNewPermitObjectPermissions(itemModel);
                    if (success)
                        appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                    else
                        throw new Exception("Lỗi trong quá trình xử lý");
                    appDomainResult.Success = success;
            }
            else
            {
                throw new AppException(ModelState.GetErrorMessage());
            }
            return appDomainResult;
        }
        [HttpPost]
        [Route("UpdatePermitObjectPermission")]
        [AppAuthorize(new string[] { CoreContants.AddNew })]
        public async Task<AppDomainResult> UpdatePermitObjectPermission([FromBody] PermitObjectPermissionAddNewRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                success = await permitObjectPermissionService.UpdatePermitObjectPermissions(itemModel);
                if (success)
                    appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                else
                    throw new Exception("Lỗi trong quá trình xử lý");
                appDomainResult.Success = success;
            }
            else
            {
                throw new AppException(ModelState.GetErrorMessage());
            }
            return appDomainResult;
        }

        [HttpGet]
        [Route("GetPermitObjectPermissionsByPermitObjectId")]
        [AppAuthorize(new string[] { CoreContants.AddNew })]
        public async Task<AppDomainResult> GetPermitObjectPermissionsByPermitObject([FromQuery] int itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            if (ModelState.IsValid)
            {
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                var result = await permitObjectPermissionService.GetPermitObjectPermissionsByPermitObjectId(itemModel);
                if (result != null)
                {
                    appDomainResult.Data = result;
                    appDomainResult.Success = true;
                    appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                }
                else
                    throw new Exception("Lỗi trong quá trình xử lý");
               
            }
            else
            {
                throw new AppException(ModelState.GetErrorMessage());
            }
            return appDomainResult;
        }

    }
}
