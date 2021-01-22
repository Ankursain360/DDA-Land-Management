using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LandTransfer.Filters;
using Core.Enum;
namespace LandTransfer.Controllers
{
    public class DashboardController : Controller
    {

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
