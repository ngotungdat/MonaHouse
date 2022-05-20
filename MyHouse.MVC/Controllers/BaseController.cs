using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyHouse.MVC.Models;
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
        /// Lấy Session: token, domain api, user hiện tại
        /// </summary>
        /// <param name="getUser"></param>
        /// <returns></returns>
        public async Task<CoreModel> GetCurrentSessionAsync()
        {
            try {
                string domain = GetCurrentDomain();
                string token = HttpContext.Session.GetString("token");
                if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(token))
                    return null;
                RestClient client = new RestClient(domain + "api/user/getcurrentuser");
                client.Timeout = -1;
                RestRequest request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + token);
                IRestResponse response = await client.ExecuteAsync(request);
                var obj = JObject.Parse(response.Content);
                Users users = JsonConvert.DeserializeObject<Users>(obj["Data"].ToString());
                bool Expire = bool.Parse(HttpContext.Session.GetString("expire"));
                return new CoreModel()
                {
                    Expire = Expire,
                    Domain = domain,
                    Token = token,
                    Users = users
                };

            }
            catch (Exception e) {

                return null;
            }
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
