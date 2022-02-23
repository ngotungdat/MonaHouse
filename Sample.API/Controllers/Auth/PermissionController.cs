using Sample.Entities.DomainEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Models.Auth;
using Sample.Interface.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using Sample.BaseAPI.Controllers;
using Sample.Request.Auth;
using Sample.Entities.Auth;

namespace Sample.API.Controllers.Auth
{
    [Route("api/permission")]
    [ApiController]
    [Description("Quyền người dùng")]
    [Authorize]
    public class PermissionController : BaseCatalogueController<Permissions, PermissionModel, PermissionRequest, BaseSearch>
    {
        protected PermissionController(IServiceProvider serviceProvider, ILogger<BaseController<Permissions, PermissionModel, PermissionRequest, BaseSearch>> logger, IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.catalogueService = serviceProvider.GetRequiredService<IPermissionService>();
        }
    }
}
