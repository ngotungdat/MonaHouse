using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            HttpContext.Session.SetString("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiVXNlcklkXCI6MSxcIlVzZXJOYW1lXCI6XCJhZG1pblwiLFwiRnVsbE5hbWVcIjpcImFkbWluXCIsXCJQaG9uZVwiOlwiMDEyMzQ1Njc4XCIsXCJFbWFpbFwiOlwiYWRtaW5AZ21haWwuY29tXCIsXCJJc0NoZWNrT1RQXCI6ZmFsc2UsXCJSb2xlc1wiOlt7XCJSb2xlTmFtZVwiOlwiQXV0aFwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIlBlcm1pdE9iamVjdFwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIlVzZXJHcm91cFwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIkNhdGFsb2d1ZVwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIlVzZXJcIixcIklzVmlld1wiOnRydWV9LHtcIlJvbGVOYW1lXCI6XCJQZXJtaXNzaW9uXCIsXCJJc1ZpZXdcIjp0cnVlfSx7XCJSb2xlTmFtZVwiOlwiRmlsZVwiLFwiSXNWaWV3XCI6dHJ1ZX1dLFwiSXNDb25maXJtT1RQXCI6ZmFsc2V9IiwibmJmIjoxNjQ1NDk2MTAyLCJleHAiOjE2NDU2MDc3MDIsImlhdCI6MTY0NTQ5NjEwMn0.tsWemdyzJbeHgFzYlX4Easu-NPGZ2YFVKfmfp3wC140");
            return View();
        }
        [HttpPost]
        public async Task<AppDomainResult> CheckLogin([FromBody] LoginModel data)
        {
            string username = data.username;
            //var client = new RestClient("https://localhost:44340/api/user/1");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiVXNlcklkXCI6MSxcIlVzZXJOYW1lXCI6XCJhZG1pblwiLFwiRnVsbE5hbWVcIjpcImFkbWluXCIsXCJQaG9uZVwiOlwiMDEyMzQ1Njc4XCIsXCJFbWFpbFwiOlwiYWRtaW5AZ21haWwuY29tXCIsXCJJc0NoZWNrT1RQXCI6ZmFsc2UsXCJSb2xlc1wiOlt7XCJSb2xlTmFtZVwiOlwiQXV0aFwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIlBlcm1pdE9iamVjdFwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIlVzZXJHcm91cFwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIkNhdGFsb2d1ZVwiLFwiSXNWaWV3XCI6dHJ1ZX0se1wiUm9sZU5hbWVcIjpcIlVzZXJcIixcIklzVmlld1wiOnRydWV9LHtcIlJvbGVOYW1lXCI6XCJQZXJtaXNzaW9uXCIsXCJJc1ZpZXdcIjp0cnVlfSx7XCJSb2xlTmFtZVwiOlwiRmlsZVwiLFwiSXNWaWV3XCI6dHJ1ZX1dLFwiSXNDb25maXJtT1RQXCI6ZmFsc2V9IiwibmJmIjoxNjQ1NDk5OTQ5LCJleHAiOjE2NDU2MTE1NDksImlhdCI6MTY0NTQ5OTk0OX0.2t9DXJsmxL_Nf5gSrmk2Hnn74XankRx0j6DkkBlD1X0");
            //IRestResponse response = await client.ExecuteAsync(request);
            return new AppDomainResult()
            {

            };
        }
        public class LoginModel
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
