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
    [Route("api/file")]
    [ApiController]
    [Description("Upload file lên hệ thống")]
    [Authorize]
    public class FileController : Sample.BaseAPI.Controllers.BaseFileController
    {
        public FileController(IServiceProvider serviceProvider, ILogger<FileController> logger, IWebHostEnvironment env, IMapper mapper, IConfiguration configuration) : base(serviceProvider, logger, env, mapper, configuration)
        {
        }
    }
}