using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SiteMaster.Controllers
{
    public class VillageMasterController : Controller
    {
        
        //public IActionResult Index()
        //{
        //    return View(_context.Tblvillagemaster.Where(x => x.IsActive == 1).ToList());
        //}

       



      

        public IActionResult Create()
        {

           


            return View();
        }


        public IActionResult Index()
        {




            return View();
        }

    }
}