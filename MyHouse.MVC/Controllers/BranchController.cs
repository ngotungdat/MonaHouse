using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class BranchController : BaseController
    {
        public BranchController(IConfiguration configuration) : base(configuration)
        {
        }
        public IActionResult BranchList()
        {
            return View();
        }
        public async Task<IActionResult> BranchListPartial(int PageIndex, int PageSize, int OrderBy, string SearchContent)
        {
            string domain = GetCurrentDomain();
            string token = HttpContext.Session.GetString("token");
            RestClient client = new RestClient(domain + "api/branch?PageIndex=" + PageIndex + "&PageSize=" + PageSize + "&OrderBy=" + OrderBy);
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
            List<BranchModel> model = JsonConvert.DeserializeObject<List<BranchModel>>(items.ToString());
            return PartialView(model);
        }
    }
}
