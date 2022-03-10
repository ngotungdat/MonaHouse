using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyHouse.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    [Description("Quản lý danh sách gói cho thuê")]
    public class PackageController : BaseController
    {
        public PackageController(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<IActionResult> PackageList()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
    }
}
