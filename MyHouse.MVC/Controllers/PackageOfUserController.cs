using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyHouse.MVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Sample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    [Description("Quản lý danh sách gói cho thuê của người dùng")]
    public class PackageOfUserController : BaseController
    {
        public PackageOfUserController(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<IActionResult> PackageOfUserList()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> PackageOfUserPartial(int PageIndex, int PageSize, int OrderBy, int TenantId, int UserId=-1)
        {
            string domain = GetCurrentDomain();
            CoreModel coreModel = await GetCurrentSessionAsync();
            string token = HttpContext.Session.GetString("token");
            RestClient client = new RestClient(domain + "api/PackageOfUser?PageIndex=" + PageIndex + "&PageSize=" + PageSize + "&OrderBy=" + OrderBy + "&TenantId=" + TenantId + "&UserId=" + UserId);
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = await client.ExecuteAsync(request);
            var obj = JObject.Parse(response.Content);
            var data = obj["Data"];
            var items = data["Items"];
            ViewData["pageindex"] = data["PageIndex"];
            ViewData["pagesize"] = data["PageSize"];
            ViewData["totalpage"] = data["TotalPage"];
            ViewData["totalitem"] = data["TotalItem"];
            List<PackageOfUserModel> model = JsonConvert.DeserializeObject<List<PackageOfUserModel>>(items.ToString());
            coreModel.MyProperty = model;
            return PartialView(coreModel);
        }
        public async Task<IActionResult> PackageOfUserDetailPartial(int PackageOfUserId)
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            RestClient client = new RestClient(coreModel.Domain + "api/PackageOfUser/" + PackageOfUserId);
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + coreModel.Token);
            IRestResponse response = await client.ExecuteAsync(request);
            var obj = JObject.Parse(response.Content);
            var data = obj["Data"];
            PackageOfUserModel model = JsonConvert.DeserializeObject<PackageOfUserModel>(data.ToString());
            coreModel.MyProperty = model;
            return PartialView(coreModel);
        }
    }
}
