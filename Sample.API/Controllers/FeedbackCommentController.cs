using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.BaseAPI.Controllers;
using Sample.Entities;
using Sample.Entities.DomainEntities;
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
using static Sample.Utilities.CoreContants;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Description("Bình luận phản hồi")]
    [Authorize]
    public class FeedbackCommentController : BaseController<FeedbackComment, FeedbackCommentModel, FeedbackCommentRequest, BaseSearch>
    {
        protected IPackageService packageService;
        protected IFeedbackCommentService FeedbackCommentService;
        protected IFeedbackService feedbackService;
        protected IUserService userService;
        public FeedbackCommentController(IServiceProvider serviceProvider, ILogger<BaseController<FeedbackComment, FeedbackCommentModel, FeedbackCommentRequest, BaseSearch>> logger
                , IConfiguration configuration, IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IFeedbackCommentService>();
            FeedbackCommentService = serviceProvider.GetRequiredService<IFeedbackCommentService>();
            userService = serviceProvider.GetRequiredService<IUserService>();
            feedbackService = serviceProvider.GetRequiredService<IFeedbackService>();
        }
        [HttpGet]
        [Route("GetFeedbackCommentByFeedbackId")]
        [AppAuthorize(new string[] { CoreContants.ViewAll })]
        public async Task<AppDomainResult> GetFeedbackComment([FromQuery] BaseSearch baseSearch)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            if (ModelState.IsValid)
            {
                baseSearch.SearchContent = baseSearch.SearchContent ?? "";
                baseSearch.TenantId = userService.GetById(LoginContext.Instance.CurrentUser.UserId).TenantId;
                PagedList<FeedbackComment> pagedData = await this.FeedbackCommentService.GetFeedbackCommentByFeedbackId(baseSearch);
                PagedList<FeedbackCommentModel> pagedDataModel = mapper.Map<PagedList<FeedbackCommentModel>>(pagedData);
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

        [HttpPost]
        [AppAuthorize(new string[] { CoreContants.AddNew })]
        public override async Task<AppDomainResult> AddItem([FromBody] FeedbackCommentRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                var item = mapper.Map<FeedbackComment>(itemModel);
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                if (item != null)
                {
                    item.TenantId = user.TenantId;
                    item.CreatedBy = user.UserName;
                    // Kiểm tra item có tồn tại chưa?
                    var messageUserCheck = await this.domainService.GetExistItemMessage(item);
                    if (!string.IsNullOrEmpty(messageUserCheck))
                        throw new AppException(messageUserCheck);

                    Feedback feedback = feedbackService.GetById(Int32.Parse(item.FeedbackId));
                    if(feedback== null) throw new Exception("Lỗi trong quá trình xử lý");

                    //update feedback
                    //neu user la khach hang thi khogn cho cap nhat feedback
                    if (user.RoleNumber != ((int)RoleUser.ThueTro))
                    {
                        feedback.Handler = item.CreatedBy;
                        if (feedback.Status == "1")
                        {
                            feedback.Status = "2";
                        }
                    }
                    
                    
                    success = await this.domainService.CreateAsync(item);
                    if (success)
                    {
                        // tao comment => updatefeedback
                        var result = await this.feedbackService.UpdateAsync(feedback);
                        if(result== false) throw new Exception("Lỗi trong quá trình xử lý");

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

    }


}   
