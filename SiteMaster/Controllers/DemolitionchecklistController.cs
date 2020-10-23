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
    public class DemolitionchecklistController : BaseController
    {
        private readonly IDemolitionchecklistService _demolitionchecklistService;
        public DemolitionchecklistController(IDemolitionchecklistService demolitionchecklistService)
        {
            _demolitionchecklistService = demolitionchecklistService;
        }


        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemolitionchecklistSearchDto model)
        {
            var result = await _demolitionchecklistService.GetPagedDemolitionchecklist(model);

            return PartialView("_List", result);
        }




        public async Task<IActionResult> Create()
        {
            Demolitionchecklist demolitionchecklist = new Demolitionchecklist();
            demolitionchecklist.IsActive = 1;
             return View(demolitionchecklist);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Demolitionchecklist demolitionchecklist)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    var result = await _demolitionchecklistService.Create(demolitionchecklist);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _demolitionchecklistService.GetDemolitionchecklist();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demolitionchecklist);
                    }
                }
                else
                {
                    return View(demolitionchecklist);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(demolitionchecklist);
            }
        }




        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _demolitionchecklistService.FetchSingleResult(id);
          
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Demolitionchecklist demolitionchecklist)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _demolitionchecklistService.Update(id, demolitionchecklist);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _demolitionchecklistService.GetDemolitionchecklist();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demolitionchecklist);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(demolitionchecklist);
                }
            }
            else
            {
                return View(demolitionchecklist);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _demolitionchecklistService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _demolitionchecklistService.GetDemolitionchecklist();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _demolitionchecklistService.FetchSingleResult(id);
          

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}
