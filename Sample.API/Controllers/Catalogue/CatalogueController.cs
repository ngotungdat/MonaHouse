using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.API.Controllers.Auth
{
    [Route("api/catalogue")]
    [ApiController]
    [Description("Quản lý danh mục")]
    [Authorize]
    public class CatalogueController : Sample.BaseAPI.Controllers.Catalogue.CatalogueController
    {
        public CatalogueController(IServiceProvider serviceProvider, IMapper mapper, IConfiguration configuration) : base(serviceProvider, mapper, configuration)
        {
        }
    }
}