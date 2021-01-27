using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Enum;
using SiteMaster.Filters;
namespace SiteMaster.Controllers
{
    public class CountryController : BaseController
    {
        public CountryController()
        {

        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
