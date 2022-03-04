using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Sample.Entities;
using Sample.Extensions;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected IConfiguration configuration;
        public BaseController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async Task<AppDomainResult> GetCurrentUserAsync()
        {
            string domain = GetCurrentDomain();
            string token = HttpContext.Session.GetString("token");
            var client = new RestClient(domain + "api/user/getcurrentuser");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await client.ExecuteAsync<AppDomainResult>(request);
            return (AppDomainResult)response;
        }
        public string GetCurrentDomain()
        {
            IConfiguration appSettingsSection = configuration.GetSection("AppSettings");
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            return appSettings.Domain;
        }
    }
}
