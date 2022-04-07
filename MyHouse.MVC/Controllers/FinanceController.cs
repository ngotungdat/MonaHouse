using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyHouse.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class FinanceController : BaseController
    {
        public FinanceController(IConfiguration configuration) : base(configuration)
        {
        }
        // phiếu thu
        public async Task<IActionResult> Receipts()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        // phiếu chi
        public async Task<IActionResult> Payment(int id)
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            ViewBag.cityId = id;
            return View(coreModel);
        }
        // in sửa phiếu thu
        public async Task<IActionResult> Invoice()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        // in sửa phiếu chi
        public async Task<IActionResult> Vouchers()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }

    }
}
