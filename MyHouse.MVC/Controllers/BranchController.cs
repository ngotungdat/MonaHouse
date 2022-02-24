using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class BranchController : Controller
    {
        public IActionResult BranchList()
        {
            return View();
        }
    }
}
