using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyHouse.MVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Sample.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Dashboard()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            // kiểm tra role của người đăng nhập
            // khách trọ không có trang thống kê
            if (coreModel.Users.RoleNumber == 5) {
                return RedirectToAction("RoomDetailForRenter", "Room");
            }
            // Chủ trọ có riêng 1 tản thống kê
            if (coreModel.Users.RoleNumber == 3)
            {
                return RedirectToAction("DashboardCustomer", "Home");
            }
            return View(coreModel);
        }
        public async Task<IActionResult> DashboardCustomer()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }

        public async Task<IActionResult> Reportew()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(coreModel);
        }
        public async Task<IActionResult> ReportEmpty()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
            {
                return RedirectToAction("Login", "Login");
            }
            RestClient client = new RestClient(coreModel.Domain + "api/Room/GetAllRoomByTenantId?userId="+coreModel.Users.TenantId);
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + coreModel.Token);
            IRestResponse response = await client.ExecuteAsync(request);
            var obj = JObject.Parse(response.Content);
            var data = obj["Data"];
            List<ReportEmptyModel> model = JsonConvert.DeserializeObject<List<ReportEmptyModel>>(data.ToString());
            coreModel.MyProperty = model;
            return View(coreModel);
        }
        public async Task<IActionResult> ReportDebt()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(coreModel);
        }
        public async Task<IActionResult> ReportType()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(coreModel);
        }
        public async Task<IActionResult> ReportHouse()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(coreModel);
        }
    }
}
