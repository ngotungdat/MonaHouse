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
    [Description("Quản lý nhóm user")]
    public class UserGroupController : BaseController
    {
        public UserGroupController(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Danh sách nhóm user do supper admin quản lý
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> UserGroup()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
        public async Task<IActionResult> UserInGroup()
        {
            CoreModel coreModel = await GetCurrentSessionAsync();
            if (coreModel == null)
                return RedirectToAction("Login", "Login");
            return View(coreModel);
        }
    }
}
