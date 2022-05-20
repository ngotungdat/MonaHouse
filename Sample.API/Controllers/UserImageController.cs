using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Description("Quản lý hình ảnh người dùng")]
    public class UserImageController : BaseController<UserImage, UserImageModel, UserImageRequest, BaseSearch>
    {
        protected IUserService userService;
        protected IUserImageService UserImageService;
        private IConfiguration configuration;

        public UserImageController(IServiceProvider serviceProvider, ILogger<BaseController<UserImage, UserImageModel, UserImageRequest, BaseSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IUserImageService>();
            this.UserImageService = serviceProvider.GetRequiredService<IUserImageService>();
            this.configuration = configuration;
        }
        [HttpGet]
        [Route("GetUserImagesByUserID")]
        [AppAuthorize(new string[] { CoreContants.ViewAll })]
        public async Task<AppDomainResult> GetUserImagesByUserID([FromQuery] int userId)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            if (userId != null)
            {
                List<UserImage> result = await UserImageService.GetUserImageByUserId(userId);
                List<UserImageModel> data = mapper.Map<List<UserImageModel>>(result);
                appDomainResult = new AppDomainResult
                {
                    Data = data,
                    Success = true,
                    ResultCode = (int)HttpStatusCode.OK,
                    ResultMessage = ApiMessage.GETALL_SUCCESS
                };


                return appDomainResult;
            }
            throw new Exception("Lỗi trong quá trình xủ lý");
        }
        [HttpPost]
        [Route("UpdateUserImage")]
        [AppAuthorize(new string[] { CoreContants.Update })]
        public async Task<AppDomainResult> UpdateUserImage([FromBody] UpdateUserImageRequest updateUserImageRequest)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            if (updateUserImageRequest != null)
            {
                bool result = await UserImageService.UpdateUserImages(updateUserImageRequest);
                if (!result)
                {
                    throw new Exception("Lỗi trong quá trình xủ lý");
                }
                appDomainResult = new AppDomainResult
                {
                    Success = true,
                    ResultCode = (int)HttpStatusCode.OK,
                    ResultMessage = ApiMessage.UPDATE_SUCCESS
                };


                return appDomainResult;
            }
            throw new Exception("Lỗi trong quá trình xủ lý");
        }
    }
}
