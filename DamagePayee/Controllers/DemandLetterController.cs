using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DamagePayee.Controllers
{
    public class DemandLetterController : Controller
    {
        public IActionResult GenerateDemandLetter()
        {
            return PartialView();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
