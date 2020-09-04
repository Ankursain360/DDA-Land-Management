using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SiteMaster.Controllers
{
    public class ZoneMasterController : Controller
    {

       
        //public IActionResult Index()
        //{
        //    return View(_context.Tblzonemaster.Where(x => x.IsActive == 1).ToList());
        //}

        public IActionResult Index()
        {
            return View();


        }


     

    
        public IActionResult Create()
        {

           
         
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Tblzonemaster tblzonemaster)
        //{

        //    BindDropDown();
         
        //    tblzonemaster.CreatedBy = "Admin";
        //    tblzonemaster.CreatedDate = DateTime.Now;
        //    _context.Add(tblzonemaster);
        //    var result = await _context.SaveChangesAsync();

        //    if (result == 1)
        //    {
        //        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Details Successfully Saved!!.", Status = "S", BackPageAction = "Index", BackPageController = "ZoneMaster" };

        //    }
        //    else
        //    {
        //        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Please try again later", Status = "S", BackPageAction = "Index", BackPageController = "ZoneMaster" };
        //        return View();

        //    }
        //    return View();
        //}
        //public async Task<IActionResult> Edit(int id)
        //{
        //    BindDropDown();
        //    var Data = await _context.Tblzonemaster.FindAsync(id);
        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Tblzonemaster tblzonemaster)
        //{
        //    BindDropDown();
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            tblzonemaster.ModifiedBy = "Admin";
        //            tblzonemaster.ModifiedDate = DateTime.Now;

        //            _context.Tblzonemaster.Update(tblzonemaster);
        //            var result = await _context.SaveChangesAsync();
        //            if (result == 1)
        //            {
        //                ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Details Successfully Updated!!.", Status = "S", BackPageAction = "Index", BackPageController = "ZoneMaster" };
        //            }
        //            else
        //            {
        //                ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Please try again later", Status = "S", BackPageAction = "Index", BackPageController = "ZoneMaster" };
        //                return View();
        //            }
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!Exist(tblzonemaster.ZoneId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    return View(tblzonemaster);
        //}

        //private bool Exist(int id)
        //{
        //    return _context.Tblzonemaster.Any(e => e.ZoneId == id);
        //}
        //public async Task<IActionResult> Delete(int? id)
        //{

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var form = await _context.Tblzonemaster
        //        .FirstOrDefaultAsync(m => m.ZoneId == id);
        //    if (form == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(form);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var form = await _context.Tblzonemaster.FindAsync(id);
        //    form.IsActive = 0;
        //    _context.Tblzonemaster.Update(form);
        //    var result = _context.SaveChanges();
        //    if (result > 0)
        //    {
        //        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Details Successfully Deleted!!.", Status = "S", BackPageAction = "Index", BackPageController = "ZoneMaster" };
        //    }
        //    else
        //    {
        //        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Please try again later", Status = "S", BackPageAction = "Index", BackPageController = "ZoneMaster" };
        //    }
        //    return View();
        //}
   
    
    
    }
}