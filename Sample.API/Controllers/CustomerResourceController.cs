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
        [Description("Nguồn khách hàng")]
        [Authorize]
        public class CustomerResourceController : BaseController<CustomerResources, CustomerResourceModel, FeedBackTypeRequest, BaseSearch>
        {
            protected IPackageService packageService;
            public CustomerResourceController(IServiceProvider serviceProvider, ILogger<BaseController<CustomerResources, CustomerResourceModel, FeedBackTypeRequest, BaseSearch>> logger
                , IConfiguration configuration, IWebHostEnvironment env) : base(serviceProvider, logger, env)
            {
                this.domainService = serviceProvider.GetRequiredService<ICustomerResourceService>();
            }
        }
}
