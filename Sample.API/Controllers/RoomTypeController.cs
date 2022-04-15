using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.BaseAPI.Controllers;
using Sample.Entities;
using Sample.Entities.DomainEntities;
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
        [Description("Thể loại phòng")]
        [Authorize]
        public class RoomTypeController : BaseController<RoomType, RoomTypeModel, RoomTypeRequest, BaseSearch>
        {
            protected IPackageService packageService;
            public RoomTypeController(IServiceProvider serviceProvider, ILogger<BaseController<RoomType, RoomTypeModel, RoomTypeRequest, BaseSearch>> logger
                , IConfiguration configuration, IWebHostEnvironment env) : base(serviceProvider, logger, env)
            {
                this.domainService = serviceProvider.GetRequiredService<IRoomTypeService>();
            }
        }
}
