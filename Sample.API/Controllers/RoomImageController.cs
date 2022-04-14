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
    [Description("Quản lý thông tin nhà")]
    public class RoomImageController : BaseController<RoomImage, RoomImageModel, RoomImageRequest, BaseSearch>
    {
        protected IBranchService branchService;
        protected IUserService userService;
        private IConfiguration configuration;
        public RoomImageController(IServiceProvider serviceProvider, ILogger<BaseController<RoomImage, RoomImageModel, RoomImageRequest, BaseSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IRoomImageService>();
            this.configuration = configuration;
        }
    }
}
