using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using FileDataLoading.Models;

namespace FileDataLoading.Controllers
{
    public class Document_Paper_IssueController : Controller
    {
        //private readonly lmsContext _context;
        //public Document_Paper_IssueController(lmsContext context)
        //{
        //    _context = context;
        //}
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            ViewBag.IsShowData = "Yes";
            return View();
        }
    }
}
