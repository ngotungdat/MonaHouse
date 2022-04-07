using Sample.Entities;
using Sample.Extensions;
using Sample.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Sample.Models.Auth;
using Sample.Interface.Services.Auth;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Sample.BaseAPI.Controllers;
using Sample.Request.Auth;
using Sample.Entities.Auth;
using Sample.Entities.Search;

namespace Sample.API.Controllers.Auth
{
    [Route("api/user-in-group")]
    [ApiController]
    [Description("Nhóm người dùng trong nhóm")]
    [Authorize]
    public class UserInGroupController : BaseController<UserInGroups, UserInGroupModel, UserInGroupRequest, UserInGroupSearch>
    {
        public UserInGroupController(IServiceProvider serviceProvider, ILogger<BaseController<UserInGroups, UserInGroupModel, UserInGroupRequest, UserInGroupSearch>> logger, IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IUserInGroupService>();
           
        }
    }
}
