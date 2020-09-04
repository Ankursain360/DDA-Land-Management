using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using FileDataLoading.Models;

namespace FileDataLoading.Controllers
{
    public class IssueReturnFileController : Controller
    {
        //private readonly lmsContext _context;
        //public IssueReturnFileController(lmsContext context)
        //{
        //    _context = context;
        //}
        public IActionResult Index()
        {
            //ViewBag.localdata = _context.TblMasterDesignation.ToList();
            return View();
        }
        //[HttpPost]
        //public JsonResult GetAutocmplete(string Prefix)
        //{

        //    //var Countries = (from c in _context.TblMasterDesignation
        //    //                 where c.DesignationName.StartsWith(Prefix)
        //    //                 select new { c.DesignationName, c.DesignationId });
        //    //return Json(Countries);
        //}
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

        public async Task<IActionResult>  IssueFile()
        {
            return View();
        }

        //public IActionResult IssueFileData()
        //{
        //    return View();
        //}

        public async Task<IActionResult> IssueFileData(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
               
                return View();
            }
        }
        public IActionResult IssueReceipt()
        {
            return PartialView("IssueReceipt");
        }
        public IActionResult ReturnReceipt()
        {
            return PartialView("ReturnReceipt");
        }

    }
}
