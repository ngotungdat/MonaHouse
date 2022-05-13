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
    [Description("Quản lý gói phần mềm cho thuê của ngoài dùng")]
    [Authorize]
    public class PackageOfUserController : BaseController<PackageOfUser, PackageOfUserModel, PackageOfUserRequest, PackageOfUserSearch>
    {
        protected IPackageService packageService;
        private IConfiguration configuration;
        public PackageOfUserController(IServiceProvider serviceProvider, ILogger<BaseController<PackageOfUser, PackageOfUserModel, PackageOfUserRequest, PackageOfUserSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IPackageOfUserService>();
            this.packageService = serviceProvider.GetRequiredService<IPackageService>();
            this.configuration = configuration;
        }
    }
}
