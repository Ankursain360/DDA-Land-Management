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
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;

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

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody]DesignationSearchDto model)
        {
            var x = model.name;
            var result = await _designationService.GetAllDesignation();
            return PartialView("_List", result);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Designation designation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _designationService.Create(designation);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
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
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
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
        public async Task<IActionResult> Edit(int id, Designation designation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _designationService.Update(id, designation);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _designationService.GetAllDesignation();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(designation);

                    }
                }
                catch (Exception ex)
                {
                   
                }
            }
            return View(designation);
        }

        [AcceptVerbs ("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult>  Exist(int Id, string Name)
        {
            var result = await _designationService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Designation: {Name} already exist");
            }
        }


        public async Task<IActionResult> Delete(int id)  //Not in use
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _designationService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }

            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View(form);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
           
            var result = await _designationService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _designationService.GetAllDesignation();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _designationService.GetAllDesignation();
                return View("Index", result1);
            }

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
