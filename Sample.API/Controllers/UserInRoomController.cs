using Sample.Entities;
using Sample.Extensions;
using Sample.Interface.Services;
using Sample.Interface.Services.Auth;
using Sample.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Sample.Models;
//using Sample.Models.Auth;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Sample.Request;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;

namespace Sample.BaseAPI.Controllers
{
    [Route("api/userinroom")]
    [ApiController]
    [Description("Quản lý thông tin khách hàng thuê phòng")]
    [Authorize]
    public class UserInRoomController : BaseController<UserInRoom, UserInRoomModel, UserInRoomRequest, UserInRoomSearch>
    {
        IUserInRoomService userInRoomService;
        public UserInRoomController(IServiceProvider serviceProvider, ILogger<BaseController<UserInRoom, UserInRoomModel, UserInRoomRequest, UserInRoomSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IUserInRoomService>();
            userInRoomService = serviceProvider.GetRequiredService<IUserInRoomService>();
        }
        [HttpGet()]
        [Route("GetByUserInRoomByRoomId")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetByUserInRoomByRoomId(int id)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
           
            var item = await this.userInRoomService.GetByUserInRoomByRoomId(id);

            if (item != null)
            {
                if (LoginContext.Instance.CurrentUser != null)
                {
                    var itemModel = mapper.Map<List<UserInRoomModel>>(item);

                    appDomainResult = new AppDomainResult()
                    {
                        Success = true,
                        Data = itemModel,
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
                else throw new KeyNotFoundException("Không có quyền truy cập");
            }
            else
            {
                throw new KeyNotFoundException("Item không tồn tại");
            }
            return appDomainResult;


        }
    }
}
