using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteMaster.Filters;
using Core.Enum;
namespace SiteMaster.Controllers
{
    public class EmployeeController : BaseController
    {

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }
    }
}
