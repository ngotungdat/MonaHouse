using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Sample.Models;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Danh sách chủ nhà trọ
        /// </summary>
        /// <returns></returns>
        public IActionResult UserOwner()
        {
            return View();
        }
        public async Task<IActionResult> UserOwnerPartial(int PageIndex, int PageSize, int OrderBy, string SearchContent)
        {
            string token = HttpContext.Session.GetString("token");
            var client = new RestClient("https://localhost:44340/api/user?PageIndex=" + PageIndex + "&PageSize=" + PageSize + "&OrderBy=" + OrderBy);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await client.ExecuteAsync(request);
            var obj = JObject.Parse(response.Content);
            var data = obj["Data"];
            var items = data["Items"];
            ViewData["pageindex"] = data["PageIndex"];
            ViewData["pagesize"] = data["PageSize"];
            ViewData["totalpage"] = data["TotalPage"];
            ViewData["totalitem"] = data["TotalItem"];
            var model = JsonConvert.DeserializeObject<List<UserModel>>(items.ToString());
            return PartialView(model);
        }
        public IActionResult UserOwnerDetailPartial(int userId)
        {
            ViewData["userId"] = userId;
            return PartialView();
        }
    }
}
