﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LeaseForPublic.Filters;

namespace LeaseForPublic.Controllers
{
    public class DashboardController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }
    }
}
