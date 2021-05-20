using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMaster.Controllers
{
    public class DamageRateListController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
