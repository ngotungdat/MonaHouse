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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckLogin(IFormCollection f)
        {
            string username = f["txt-username"].ToString().Trim();
            string password = f["txt-password"].ToString();
            var client = new RestClient("https://monahouse-api.monamedia.net/api/authenticate/login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            var response = await client.ExecuteAsync<AppDomainResult>(request);
            if (response.Data.Success)
            {
                HttpContext.Session.SetString("token", response.Data.Data.ToString());
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", response.Data.ResultMessage);
            return View("Login");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
