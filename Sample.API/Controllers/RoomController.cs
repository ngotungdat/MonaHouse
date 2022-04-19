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
    [Description("Quản lý thông tin phòng")]
    public class RoomController : BaseController<Room, RoomModel, RoomRequest, RoomSearch>
    {
        protected IBranchService branchService;
        protected IUserService userService;
        private IConfiguration configuration;
        protected IRoomService roomService;
        public RoomController(IServiceProvider serviceProvider, ILogger<BaseController<Room, RoomModel, RoomRequest, RoomSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IRoomService>();
            roomService = serviceProvider.GetRequiredService<IRoomService>();
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("AddNewRoom")]
        [AppAuthorize(new string[] { CoreContants.AddNew })]
        public async Task<AppDomainResult> AddNewRoom([FromBody] RoomWithImgRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                    success = await roomService.AddNewRoomWithImage(itemModel);
                    if (success)
                        appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                    else
                        throw new Exception("Lỗi trong quá trình xử lý");
                    appDomainResult.Success = success;
                }
                else
                    throw new AppException("Item không tồn tại");
            
            return appDomainResult;
        }
    }
}
