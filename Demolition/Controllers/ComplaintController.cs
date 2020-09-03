using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DDAPropertyREG.Controllers
{
    public class ComplaintController : Controller
    {
        static string Result = string.Empty;
        //private readonly IAuthService _AuthService;
        //public OlComplaintController(IAuthService AuthService)
        //{
        //    _AuthService = AuthService;
        //}
        //private readonly lmsContext _context;

        //public async Task<IActionResult> CmplntCreate()
        //{
        //    var a = _context.TblCmplntType.Select(x => new { x.CmplntType, x.Id }).ToList();
        //    ViewBag.ListCmplnType = _context.TblCmplntType.Select(x => new { x.CmplntType, x.Id }).ToList();
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CmplntCreate(TblOlComplaint model)
        //{
        //    _context.TblOlComplaint.Add(model);
        //    var result = await _context.SaveChangesAsync();

        //    if (result == 1)
        //    {
        //        Result = "Saved";
        //        ViewData["Msg"] = "Dear User,<br/>Details Successfully Saved!!.";
        //    }
        //    else
        //    {
        //        ViewData["Msg"] = "Dear User,<br/><b>Some Error Ocurred Please try Later. if the problem persists contact your system administrator..";
        //    }
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            //var CmplnDetails = await _context.TblOlComplaint.ToListAsync();

            //return View(CmplnDetails.ToList());
            return View();
        }

        //public async Task<IActionResult> CmplntDetails()
        //{
        //    if (!_context.TblOlComplaint.Any())
        //    {
        //        //return View(CmplntCreate);
        //        return RedirectToAction(nameof(CmplntCreate));
        //    }
        //    else
        //    {
        //        var CmplnDetails = await _context.TblOlComplaint.ToListAsync();
        //        return View(CmplnDetails.ToList());

        //    }
        //    return NotFound();
        //}


        public IActionResult Report()
        {
            return View();
        }


    }
}