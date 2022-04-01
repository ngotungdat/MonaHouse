using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyHouse.MVC.Models;
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
    public class NotificationController : BaseController
    {
        public NotificationController(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<IActionResult> NotificationUsers()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> NotificationCenter()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
    }
}
