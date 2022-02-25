using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Controllers
{
    public class PaymentHistoryController : Controller
    {
        public IActionResult PaymentHistoryList()
        {
            return View();
        }
    }
}
