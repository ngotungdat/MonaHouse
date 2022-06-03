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
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class RoomController : BaseController
    {
        public RoomController(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<IActionResult> RoomList()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> RoomDetail()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> RoomDetailForRenter()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            //
            string domain = GetCurrentDomain();
            string token = HttpContext.Session.GetString("token");
            RestClient client = new RestClient(domain + "api/userinroom?UserId="+coreModel.Users.Id+"&PageIndex=1&PageSize=20&OrderBy=0&TenantId="+coreModel.Users.TenantId);
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = await client.ExecuteAsync(request);
            var obj = JObject.Parse(response.Content);
            var data = obj["Data"];
            var items = data["Items"];
            List<UserInRoomModel> model = JsonConvert.DeserializeObject<List<UserInRoomModel>>(items.ToString());
            if (model.Count == 0)
                return RedirectToAction("Login", "Login");
            coreModel.MyProperty = model[0].RoomId;
            //
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> Test()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> AddCustomer()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> ProfileTenantDetail(int id)
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            //
            string domain = GetCurrentDomain();
            string token = HttpContext.Session.GetString("token");
            RestClient client = new RestClient(domain + "api/user/"+id);
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = await client.ExecuteAsync(request);
            var obj = JObject.Parse(response.Content);
            var data = obj["Data"];
            //var items = data["Items"];
            UserModel model = JsonConvert.DeserializeObject<UserModel>(data.ToString());
            coreModel.MyProperty = model;
            //
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> HistoryRoomReceiptPartial()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            
            return PartialView(coreModel);
        }
    }
}

