using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DamagePayee.Controllers
{
    public class DemolationController : Controller
    {
        public IActionResult Index()
        {   return View();  }
        public IActionResult Report()
        {
            return View();
        }
    }
}