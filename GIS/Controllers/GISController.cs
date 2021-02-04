using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GIS.Controllers
{
    public class GISController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
