using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SiteMaster.Controllers
{
    public class LocalityController : Controller
    {



        public IActionResult Index()
        {




            return View();
        }



        public IActionResult Create()
        {

           


            return View();
        }
      

    }
}
