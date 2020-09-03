using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using DDAPropertyREG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SiteMaster.Controllers
{
    public class DistrictController : Controller
    {


      //  private readonly lmsContext _context;
       // public DistrictController(lmsContext context)
       // {
           // _context = context;
       // }
        public IActionResult Index()
        {
            return View();

            //(_context.Tblmasterdistrict.Where(x => x.IsActive == 1).ToList());
        }

      //  public async Task<IActionResult> ViewDetails(int id)
      //  {
       //     var Data = await _context.Tblmasterdistrict.FindAsync(id);
        //    if (Data == null)
         //   {
           //     return NotFound();
           // }
           // return View(Data);
      //  }

       
        public IActionResult Create()
        {
            return View();
        }
       // [HttpPost]
       // [ValidateAntiForgeryToken]
       // public async Task<IActionResult> Create(Tblmasterdistrict tblmasterdistrict)
       // {
        //    tblmasterdistrict.CreatedBy = "Admin";
          //  tblmasterdistrict.CreatedDate = DateTime.Now;
           // _context.Add(tblmasterdistrict);
          //  var result = await _context.SaveChangesAsync();

          //  if (result == 1)
           // {
             //   ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Details Successfully Saved!!.", Status = "S", BackPageAction = "Index", BackPageController = "District" };

           // }
           // else
          //  {
             //   ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Please try again later", Status = "S", BackPageAction = "Index", BackPageController = "District" };
              //  return View();

          //  }
          //  return View();
      //  }
      //  public async Task<IActionResult> Edit(int id)
      //  {
         //   var Data = await _context.Tblmasterdistrict.FindAsync(id);
          //  if (Data == null)
          //  {
               // return NotFound();
          //  }
          //  return View(Data);
      //  }

    }
}