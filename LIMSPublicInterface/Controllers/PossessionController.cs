using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LIMSPublicInterface.Controllers
{
    public class PossessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
