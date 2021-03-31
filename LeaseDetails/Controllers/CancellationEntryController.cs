using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseDetails.Controllers
{
    public class CancellationEntryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
