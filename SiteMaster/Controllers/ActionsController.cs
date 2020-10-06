using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SiteMaster.Controllers
{
    public class ActionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
