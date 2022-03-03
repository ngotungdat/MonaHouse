using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class ConfigController : Controller
    {
        public IActionResult Cities()
        {
            ViewBag.domain = "https://localhost:44340/";
            return View();
        }
    }
}
