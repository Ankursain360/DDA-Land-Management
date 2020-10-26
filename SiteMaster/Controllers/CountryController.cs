using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SiteMaster.Controllers
{
    public class CountryController : BaseController
    {
        public CountryController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
