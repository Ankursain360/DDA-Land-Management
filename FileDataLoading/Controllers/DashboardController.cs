using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FileDataLoading.Filters;

namespace FileDataLoading.Controllers
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
