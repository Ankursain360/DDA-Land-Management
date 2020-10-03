using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LIMSPublicInterface.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult VillageReport()
        {
            return View();
        }
        public IActionResult NazulVillageReport()
        {
            return View();
        }
        public IActionResult AcquiredVillageReport()
        {
            return View();
        }
        public IActionResult VillageDetailsKhasraWiseReport()
        {
            return View();
        }
       
    }
}
