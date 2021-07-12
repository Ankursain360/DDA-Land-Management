using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Enum;
using LeaseForPublic.Filters;
namespace LeaseForPublic.Controllers
{
    public class KYCformController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       // [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }
    }
}
