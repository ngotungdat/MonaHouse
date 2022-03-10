using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using Sample.Extensions;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Màn hình đăng nhập
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Kiểm tra đăng nhập và gán session
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckLogin(IFormCollection f)
        {
            HttpContext.Session.Clear();
            string domain = GetCurrentDomain();
            HttpContext.Session.SetString("domain", domain);
            string username = f["txt-username"].ToString().Trim();
            string password = f["txt-password"].ToString();
            var client = new RestClient(domain + "api/authenticate/login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            var response = await client.ExecuteAsync<AppDomainResult>(request);
            if (response.Data.Success)
            {
                string token = response.Data.Data.ToString();
                HttpContext.Session.SetString("token", token);
                return RedirectToAction("Dashboard", "Home");
            }
            ModelState.AddModelError("", response.Data.ResultMessage);
            return View("Login");
        }
        /// <summary>
        /// Quên mật khẩu, gửi mail hoặc OTP
        /// </summary>
        /// <returns></returns>
        public IActionResult ForgotPassword()
        {
            return View();
        }
        /// <summary>
        /// Đăng xuất và xóa tất cả session
        /// </summary>
        /// <returns></returns>
        public IActionResult Signout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}
