using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Danh sách chủ nhà trọ
        /// </summary>
        /// <returns></returns>
        public IActionResult UserOwner()
        {
            return View();
        }
        public IActionResult UserOwnerDetailPartial(int userId)
        {
            ViewData["userId"] = userId;
            return PartialView();
        }
    }
}
