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
using Sample.Interface;
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
    [Description("Tiện ích phòng")]
    public class RoomUtilitiesController : BaseController<RoomUtilities, RoomUtilitiModel, RoomUtilitieRequest, RoomUtilitiSearch>
    {
        protected IRoomUtilitiService roomUtilitieService;
      
        public RoomUtilitiesController(IServiceProvider serviceProvider, ILogger<BaseController<RoomUtilities, RoomUtilitiModel, RoomUtilitieRequest, RoomUtilitiSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IRoomUtilitiService>();
            this.roomUtilitieService = serviceProvider.GetRequiredService<IRoomUtilitiService>();
        }

        [HttpPost]
        [Route("Update_Utilities_RoomService")]
        [AppAuthorize(new string[] { CoreContants.Update })]
        public async Task<AppDomainResult> UpdateUtilitiesRoomService([FromBody] UpdateUtilitiesRoomRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                success = await roomUtilitieService.UpdateUtilitiesRoom(itemModel);
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
