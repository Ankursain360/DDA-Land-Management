using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EncroachmentDemolition.Controllers
{
    public class DemolitionReport2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
