using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyHouse.MVC.Models;
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
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }

    }
}
