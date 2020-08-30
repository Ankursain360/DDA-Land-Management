using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Encrochment_Management.Controllers
{
    public class WatchWardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult WatchWardApproval()
        {
            //Added by renu 29 aug 2020
            return View();
        }

        public IActionResult WatchWardApprovalCreate()
        {
            return View();
        }
    }
}
