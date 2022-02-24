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
using Sample.Entities.Search;
using Sample.Entities.DomainEntities;
using Sample.BaseAPI.Controllers;

namespace Sample.API.Controllers
{
    [Route("api/Area")]
    [ApiController]
    [Description("Quản lý Tỉnh/TP")]
    [Authorize]
    public class AreaController : BaseController<Area, AreaModel, AreaRequest, BaseSearch>
    {
        protected IAreaService areaService;
        public AreaController(IServiceProvider serviceProvider, ILogger<BaseController<Area, AreaModel, AreaRequest, BaseSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IAreaService>();
        }
    }
}
