using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AcquiredLandInformationManagement.Controllers
{
    public class MutationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
