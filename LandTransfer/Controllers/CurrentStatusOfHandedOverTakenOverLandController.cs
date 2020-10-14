using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LandTransfer.Controllers
{
    public class CurrentStatusOfHandedOverTakenOverLandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult ViewHistory()
        {
            return View();
        }
    }
}
