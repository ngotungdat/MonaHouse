using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        /// <summary>
        /// Lấy User hiện tại
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Lấy User hiện tại - dùng cho view
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Users> GetCurrentUserAsync(string token, string domain)
        {
            if (string.IsNullOrEmpty(token)|| string.IsNullOrEmpty(domain))
                return null;
            RestClient client = new RestClient(domain + "api/user/getcurrentuser");
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = await client.ExecuteAsync(request);
            var obj = JObject.Parse(response.Content);
            Users users = JsonConvert.DeserializeObject<Users>(obj["Data"].ToString());
            return users;
        }
        /// <summary>
        /// Lấy tên miền api
        /// </summary>
        /// <returns></returns>
        public string GetCurrentDomain()
        {
            IConfiguration appSettingsSection = configuration.GetSection("AppSettings");
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            return appSettings.Domain;
        }
    }
}
