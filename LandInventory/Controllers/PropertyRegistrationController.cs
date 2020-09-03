using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LandInventory.Controllers
{
    public class PropertyRegistrationController : Controller
    {
        //private readonly propertyregistrationContext _context;
        //public PropertyRegistrationController(propertyregistrationContext context)
        //{
        //    _context = context;
        //}
        public IActionResult Index()
        {
            return View();
            //return View(_context.Tblpropertyregistration.ToList());
        }
       
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }



    }
}

