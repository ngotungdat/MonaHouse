using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        [Description("Trung tâm phản hồi")]
        [Authorize]
        public class FeedbackController : BaseController<Feedback, FeedbackModel, FeedbackRequest, FeedbackSearch>
        {
            protected IPackageService packageService;
            protected IUserService userService;
            public FeedbackController(IServiceProvider serviceProvider, ILogger<BaseController<Feedback, FeedbackModel, FeedbackRequest, FeedbackSearch>> logger
                , IConfiguration configuration, IWebHostEnvironment env) : base(serviceProvider, logger, env)
            {
                this.domainService = serviceProvider.GetRequiredService<IFeedbackService>();
                userService = serviceProvider.GetRequiredService<IUserService>();
            }

            [HttpPut]
            [Description("cập nhật trạng thái phản hồi")]
            [Route("UpdateStatusFeedBack")]
            [AppAuthorize(new string[] { CoreContants.Update })]
            public async Task<AppDomainResult> UpdateStatusFeedBack([FromQuery]string itemModel)
            {
                AppDomainResult appDomainResult = new AppDomainResult();
                bool success = false;
                if (ModelState.IsValid)
                {
                    var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);

                    Feedback feedback = domainService.GetById(Int32.Parse(itemModel));
                    if (feedback == null) throw new Exception("Lỗi trong quá trình xử lý");
                    //update feedback
                    feedback.Handler = user.UserName;
                    feedback.Status = "3";

                    success = await this.domainService.UpdateAsync(feedback);
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
                {
                    throw new AppException(ModelState.GetErrorMessage());
                }
                return appDomainResult;
            }
        }

}
