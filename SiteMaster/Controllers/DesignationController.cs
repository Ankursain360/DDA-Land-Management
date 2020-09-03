using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SiteMaster.Controllers
{
    public class DesignationController : Controller
    {
      
       
        public IActionResult Index()
        {
            return View();
            //return View(_context.TblMasterDesignation.Where(x => x.IsActive == 1).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }



    
    }

}
