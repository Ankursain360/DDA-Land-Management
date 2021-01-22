using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DamagePayee.Filters;
using Core.Enum;
namespace DamagePayee.Controllers
{
    public class DemolationController : Controller
    {

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {  
            
            return View();  
        }
        public IActionResult Report()
        {
            return View();
        }
    }
}