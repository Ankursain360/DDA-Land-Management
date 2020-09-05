using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using DDAPropertyREG.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourtCasesManagement.Controllers
{
    public class StayInterimController : Controller
    {
        //private readonly propertyregistrationContext _context;
        //public StayInterimController(propertyregistrationContext context)
        //{
        //    _context = context;
        //}
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