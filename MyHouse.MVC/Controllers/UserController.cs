using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
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
        public IActionResult UserOwner()
        {
            return View();
        }
        public async Task<IActionResult> UserOwnerPartial(int PageIndex, int PageSize, int OrderBy, string SearchContent)
        {
            string domain = GetCurrentDomain();
            string token = HttpContext.Session.GetString("token");
            RestClient client = new RestClient(domain + "api/user?PageIndex=" + PageIndex + "&PageSize=" + PageSize + "&OrderBy=" + OrderBy);
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
        public IActionResult UserOwnerDetailPartial(int userId)
        {
            ViewData["userId"] = userId;
            return PartialView();
        }
    }
}
