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
    [Description("Quản lý hình ảnh phòng")]
    public class RoomImageController : BaseController<RoomImage, RoomImageModel, RoomImageRequest, BaseSearch>
    {
        protected IUserService userService;
        protected IRoomImageService roomImageService;
        private IConfiguration configuration;

        public RoomImageController(IServiceProvider serviceProvider, ILogger<BaseController<RoomImage, RoomImageModel, RoomImageRequest, BaseSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IRoomImageService>();
            this.roomImageService = serviceProvider.GetRequiredService<IRoomImageService>();
            this.configuration = configuration;
        }
        [HttpGet]
        [Route("GetImageRoomByRoomID")]
        [AppAuthorize(new string[] { CoreContants.ViewAll })]
        public async Task<AppDomainResult> GetImageRoomByRoomID([FromQuery] int RoomId)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            if (RoomId != null)
            {
                List<RoomImage> result = await roomImageService.GetImageRoomByRoomId(RoomId);
                List<RoomImageModel> data = mapper.Map<List<RoomImageModel>>(result);
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
        [Route("UpdateImageRoom")]
        [AppAuthorize(new string[] { CoreContants.Update })]
        public async Task<AppDomainResult> UpdateImageRoom([FromBody] UpdateRoomImageRequest updateRoomImageRequest)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            if (updateRoomImageRequest != null)
            {
                bool result = await roomImageService.UpdateImagesRoom(updateRoomImageRequest);
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
