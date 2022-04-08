using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyHouse.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class ConfigController : BaseController
    {
        public ConfigController(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<IActionResult> Cities()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> Districts(int id)
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            ViewBag.cityId = id;
            return View(coreModel);
        }
        public async Task<IActionResult> Wards(int id,int ctityId)
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            ViewBag.districtId = id;
            ViewBag.cityId = ctityId;
            return View(coreModel);
        }
        public async Task<IActionResult> VehicleFeeConfig()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> UtilitiesConfig()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }

        //
        public async Task<IActionResult> CustomerResource()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }

        public async Task<IActionResult> FeedBackType()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> Contract()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> NewContractSample()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> EditContractSample()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> NewContract()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> ListAllContract()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
    }
}
