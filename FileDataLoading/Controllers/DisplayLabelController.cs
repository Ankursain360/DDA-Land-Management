using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using FileDataLoading.Models;

namespace FileDataLoading.Controllers
{
    public class DisplayLabelController : Controller
    {
        //private readonly lmsContext _context;
        //public DisplayLabelController(lmsContext context)
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

        //public async Task<IActionResult> AutocompleteParameter(string term)
        //{
        //    return Json(_context.TblMasterDesignation.Where(x => x.DesignationName.Equals(term)).ToList());
        //}

        public async Task<IActionResult> IssueFile()
        {
            return View();
        }

        public IActionResult IssueFileData()
        {
            return View();
        }
        
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult PrintLabel()
        {
            return PartialView("PrintLabel");
        }
    }
}
