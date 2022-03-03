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
            return View();
        }
        public IActionResult Districts(int id)
        {
            ViewBag.cityId = id;
            return View();
        }
        public IActionResult Wards(int id)
        {
            ViewBag.DistrictId = id;
            return View();
        }
    }
}
