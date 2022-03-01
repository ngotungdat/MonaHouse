using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    [Description("Quản lý danh sách gói cho thuê")]
    public class PackageController : Controller
    {
        public IActionResult PackageList()
        {
            string token = HttpContext.Session.GetString("token");
            if(string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Login");
            ViewBag.Token = token;
            return View();
        }
    }
}
