using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LeaseForPublic.Filters;

namespace LeaseForPublic.Controllers
{
    public class DashboardController : Controller
    {
        [ServiceFilter(typeof(AuditFilterAttribute))]
        public IActionResult Index()
        {
            return View();
        }
    }
}
