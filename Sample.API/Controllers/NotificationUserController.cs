using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.BaseAPI.Controllers;
using Sample.Entities;
using Sample.Entities.DomainEntities;
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
    [Description("Thông báo đến người dùng")]
    [Authorize]
    public class NotificationUserController : BaseController<NotificationUser, NotificationUserModel, NotificationUserRequest, Entities.Search.BaseSearch>
    {
        protected IPackageService packageService;
        protected INotificationService notificationService;
        protected INotificationUserService notificationUserService;
        protected IUserService userService;
        public NotificationUserController(IServiceProvider serviceProvider, ILogger<BaseController<NotificationUser, NotificationUserModel, NotificationUserRequest, Entities.Search.BaseSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<INotificationUserService>();
            this.notificationService = serviceProvider.GetRequiredService<INotificationService>();
            this.userService = serviceProvider.GetRequiredService<IUserService>();
            this.notificationUserService = serviceProvider.GetRequiredService<INotificationUserService>();
        }
        [HttpPut]
        [Route("NotificationUserUpdateIsSeen")]
        [AppAuthorize(new string[] { CoreContants.Update })]
        public async Task<AppDomainResult> NotificationUserUpdateIsSeen()
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                    success = await notificationUserService.UpdateIsSeenByUser(user);
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
    }
}
