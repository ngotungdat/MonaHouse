﻿using Microsoft.AspNetCore.Hosting;
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
    [Description("Quản lý thông tin nhà")]
    public class BranchController : BaseController<Branch, BranchModel, BranchRequest, BranchSearch>
    {
        protected IBranchService branchService;
        private IConfiguration configuration;
        public BranchController(IServiceProvider serviceProvider, ILogger<BaseController<Branch, BranchModel, BranchRequest, BranchSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IBranchService>();
            this.branchService = serviceProvider.GetRequiredService<IBranchService>();
            this.configuration = configuration;
        }
    }
}