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

namespace Sample.BaseAPI.Controllers
{
    [Route("api/userinroom")]
    [ApiController]
    [Description("Quản lý thông tin khách hàng thuê phòng")]
    [Authorize]
    public class UserInRoomController : BaseController<UserInRoom, UserInRoomModel, UserInRoomRequest, BaseSearch>
    {
        public UserInRoomController(IServiceProvider serviceProvider, ILogger<BaseController<UserInRoom, UserInRoomModel, UserInRoomRequest, BaseSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IUserInRoomService>();
        }
        
    }
}
