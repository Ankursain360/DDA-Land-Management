using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using SiteMaster.Models;

namespace SiteMaster.Controllers
{
    public class DesignationController : Controller
    {

        private readonly IDesignationService _designationService;

        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _designationService.GetAllDesignation();
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Designation designation)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    if (!Exist(0, designation))
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Unique Name Required for Designation Name", Status = "S", BackPageAction = "Create", BackPageController = "Designation" };
                        return View(designation);
                        
                    }

                    var result = await _designationService.Create(designation);

                    if (result == true)
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Details Successfully Saved!!.", Status = "S", BackPageAction = "Create", BackPageController = "Designation" };
                        return View();
                    }
                    else
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Please try again later", Status = "S", BackPageAction = "Create", BackPageController = "Designation" };
                        return View(designation);

                    }
                }
                else
                {
                    return View(designation);
                }
            }
            catch (Exception ex)
            {
                ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Something went wrong!!.", Status = "S", BackPageAction = "Create", BackPageController = "Designation" };
                return View(designation);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _designationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Designation designation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (Exist(id, designation))
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Unique Name Required for Designation Name", Status = "S", BackPageAction = "Create", BackPageController = "Designation" };
                        return View(designation);

                    }

                    var result = await _designationService.Update( id, designation);
                    if (result == true)
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Details Successfully Updated!!.", Status = "S", BackPageAction = "Index", BackPageController = "Designation" };
                        return View();
                    }
                    else
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Please try again later", Status = "S", BackPageAction = "Index", BackPageController = "Designation" };
                        return View(designation);

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exist(designation.Id, designation))
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Unique Name Required for Designation Name", Status = "S", BackPageAction = "Create", BackPageController = "Designation" };
                        return View(designation);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(designation);
        }

        private bool Exist(int id, Designation designation)
        {
            var result = _designationService.CheckUniqueName(id, designation);
            return result;
        }


        public async Task<IActionResult> Delete(int id)  //Not in use
        {
            if (id == null)
            {
                return NotFound();
            }

            var form =  await _designationService.Delete(id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            //try
            //{

            var result= await _designationService.Delete(id);
            if (result == true)
            {
                ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Details Successfully Deleted!!.", Status = "S", BackPageAction = "Index", BackPageController = "Designation" };
            }
            else
            {
                ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Please try again later", Status = "S", BackPageAction = "Index", BackPageController = "Designation" };
            }
            return RedirectToAction("Index", "Designation");
            //}
            //catch(Exception ex)
            //{
            //    ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Something went wrong", Status = "S", BackPageAction = "Index", BackPageController = "Designation" };
            //    return View();
            //}

        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _designationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }

}
