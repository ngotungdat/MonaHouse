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

            // load chart
            StringBuilder ap = new StringBuilder();
            // lấy danh sách tất cả gói cước được đăng ký
            // giả dl
            List<PackageOfUserModel> PackageOfUsers = new List<PackageOfUserModel>();
            //chart
            double total = 0;
            ap.Append("[");
            // PackageOfUsers.Count
            for (int j = 0; j < 6; j++)
            {
                //PackageOfUserModel packageOfUser = PackageOfUsers[j];
                ap.Append("{");
                ap.Append(" label: \"" + "ahihi"+j /*packageOfUser.Title*/ + "\",");
                string data = "[";
                for (int i = 1; i <= 12; i++)
                {
                    //List<tbl_CollectMoneyMotelRoom> cl = CollectMoneyMotelRoomTable.GetColletByRoom(h.ID, i, GetDateTime.Now.Year).Where(n => n.RefundDeposit != true).ToList();
                    //total += cl.Sum(n => n.Paid.Value);
                    //if (i != 12)
                    //    data += cl.Sum(n => n.Paid.Value).ToString() + ",";
                    //else
                    //    data += cl.Sum(n => n.Paid.Value).ToString();
                }
                data += "]";
                ap.Append(" data: " + data + ",");
                ap.Append(" backgroundColor: \"" + /*AssetCRM.RandomColor()*/"#7E060C" + "\",");
                ap.Append(" borderColor: \"transparent\",");
                ap.Append(" borderWidth: 0");
                if (j != (20 - 1))
                    ap.Append("},");
                else
                    ap.Append("}");
            }
            ap.Append("]");
            ViewBag.Data = ap.ToString();
            //chart
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
