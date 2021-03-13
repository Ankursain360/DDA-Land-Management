using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Enum;
using Dto.Search;
using LandInventory.Filters;
namespace LandInventory.Controllers
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
