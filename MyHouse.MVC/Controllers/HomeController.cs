using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHouse.MVC.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var data = getdata();
            return View();
        }
        public static object getdata()
        {
            var client = new RestClient("https://localhost:44340/api/user/1");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiVXNlcklkXCI6MSxcIlVzZXJOYW1lXCI6XCJhZG1pblwiLFwiRnVsbE5hbWVcIjpcImFkbWluXCIsXCJQaG9uZVwiOlwiMDEyMzQ1Njc4XCIsXCJFbWFpbFwiOlwiYWRtaW5AZ21haWwuY29tXCIsXCJJc0NoZWNrT1RQXCI6ZmFsc2UsXCJSb2xlc1wiOlt7XCJSb2xlTmFtZVwiOlwiQXV0aFwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIlBlcm1pdE9iamVjdFwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIlVzZXJHcm91cFwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIkNhdGFsb2d1ZVwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIlVzZXJcIixcIklzVmlld1wiOnRydWV9LHtcIlJvbGVOYW1lXCI6XCJQZXJtaXNzaW9uXCIsXCJJc1ZpZXdcIjp0cnVlfSx7XCJSb2xlTmFtZVwiOlwiRmlsZVwiLFwiSXNWaWV3XCI6dHJ1ZX1dLFwiSXNDb25maXJtT1RQXCI6ZmFsc2V9IiwibmJmIjoxNjQ1NDk5OTQ5LCJleHAiOjE2NDU2MTE1NDksImlhdCI6MTY0NTQ5OTk0OX0.2t9DXJsmxL_Nf5gSrmk2Hnn74XankRx0j6DkkBlD1X0");
            IRestResponse response = client.Execute(request);
            return response.Content;
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

        public IActionResult Dashboard()
        {
            return View();
        }

    }
}
