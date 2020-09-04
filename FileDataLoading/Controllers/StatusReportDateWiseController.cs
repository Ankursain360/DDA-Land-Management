using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FileDataLoading.Controllers
{
    public class StatusReportDateWiseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int id)
        {
            ViewBag.IsShowData = "Yes";
            return View();
        }
    }
}