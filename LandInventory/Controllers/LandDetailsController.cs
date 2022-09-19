using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandInventory.Controllers
{
    public class LandDetailsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
