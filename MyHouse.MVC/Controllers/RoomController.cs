using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult RoomList()
        {
            return View();
        }
    }
}
