using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.BaseAPI.Controllers;
using Sample.Entities;
using Sample.Entities.Search;
using Sample.Interface.Services;
using Sample.Models;
using Sample.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Description("Quản lý tầng")]
    [Authorize]
    public class FloorController : BaseController<Floor, FloorModel, FloorRequest, FloorSearch>
    {
        protected IFloorService floorService;
        private IConfiguration configuration;
        public FloorController(IServiceProvider serviceProvider, ILogger<BaseController<Floor, FloorModel, FloorRequest, FloorSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IFloorService>();
            this.floorService = serviceProvider.GetRequiredService<IFloorService>();
            this.configuration = configuration;
        }

    }
}
