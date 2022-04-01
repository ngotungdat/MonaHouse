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
    [Description("Trung Tâm Thông Báo")]
    [Authorize]
    public class NotificationController : BaseController<Notification, NotificationModel, NotificationRequest, NotificationSearch>
    {
        protected IPackageService packageService;
        protected INotificationService notificationService;
        protected IUserService userService;
        private IConfiguration configuration;

        public NotificationController(IServiceProvider serviceProvider, ILogger<BaseController<Notification, NotificationModel, NotificationRequest, NotificationSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<INotificationService>();
            this.notificationService = serviceProvider.GetRequiredService<INotificationService>();
            this.userService = serviceProvider.GetRequiredService<IUserService>();
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("AddNotification")]
        [AppAuthorize(new string[] { CoreContants.AddNew })]
        public async Task<AppDomainResult> AddNotification([FromBody] NotificationRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                var item = mapper.Map<Notification>(itemModel);
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                if (item != null)
                {
                    item.TenantId = user.TenantId;
                    item.CreatedBy = user.UserName;
                    // Kiểm tra item có tồn tại chưa?
                    var messageUserCheck = await this.domainService.GetExistItemMessage(item);
                    if (!string.IsNullOrEmpty(messageUserCheck))
                        throw new AppException(messageUserCheck);
                    success = await notificationService.CreatNotificationAndNotificationUser(item);
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
    }
}
