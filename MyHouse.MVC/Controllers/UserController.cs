using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyHouse.MVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Sample.Entities;
using Sample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Danh sách chủ nhà trọ
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> UserOwner()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }/// <summary>
         /// Danh sách nhân viên admin
         /// </summary>
         /// <returns></returns>
        public async Task<IActionResult> UserCSKH()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        /// <summary>
        /// Trang cá nhân
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ProfileUser()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        /// <summary>
        /// Danh sách chủ nhà trọ - Partial View
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="SearchContent"></param>
        /// <returns></returns>
        public async Task<IActionResult> UserOwnerPartial(int PageIndex, int PageSize, int OrderBy, string SearchContent,string roleNumber)
        {
            string domain = GetCurrentDomain();
            string token = HttpContext.Session.GetString("token");
            RestClient client = new RestClient(domain + "api/user?PageIndex=" + PageIndex + "&PageSize=" + PageSize + "&OrderBy=" + OrderBy + "&RoleNumber="+roleNumber);
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
            List<UserModel> model = JsonConvert.DeserializeObject<List<UserModel>>(items.ToString());
            return PartialView(model);
        }
        /// <summary>
        /// Chi tiết chủ nhà trọ - Partial View
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IActionResult> UserOwnerDetailPartial(int userId)
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            RestClient client = new RestClient(coreModel.Domain + "api/user/"+userId);
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + coreModel.Token);
            IRestResponse response = await client.ExecuteAsync(request);
            var obj = JObject.Parse(response.Content);
            var data = obj["Data"];
            UserModel model = JsonConvert.DeserializeObject<UserModel>(data.ToString());
            coreModel.MyProperty = model;
            return PartialView(coreModel);
        }


    }
}
