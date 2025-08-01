﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using SiteMaster.Filters;
using Utility.Helper;

namespace SiteMaster.Controllers
{
    public class SchemefileloadingController : BaseController
    {
        public readonly ISchemeFileLoadingService _schemeFileLoadingService;
        public SchemefileloadingController(ISchemeFileLoadingService schemeFileLoadingService)
        {
            _schemeFileLoadingService = schemeFileLoadingService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> List([FromBody] SchemeFileLoadingSearchDto model)
        {
            var result = await _schemeFileLoadingService.GetPagedSchemeFileLoading(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Schemefileloading schemefileloading)
        {
            try
            {

                if (ModelState.IsValid)
                {


                    var result = await _schemeFileLoadingService.Create(schemefileloading);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                        var list = await _schemeFileLoadingService.GetAllSchemeFileLoading();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(schemefileloading);

                    }
                }
                else
                {
                    return View(schemefileloading);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(schemefileloading);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _schemeFileLoadingService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Schemefileloading schemefileloading)
        {
            if (ModelState.IsValid)
            {
                try
                {



                    var result = await _schemeFileLoadingService.Update(id, schemefileloading);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _schemeFileLoadingService.GetAllSchemeFileLoading();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(schemefileloading);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(schemefileloading);

                }
            }
            return View(schemefileloading);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string SchemeName)
        {
            var result = await _schemeFileLoadingService.CheckUniqueName(Id, SchemeName);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Scheme: {SchemeName} already exist");
            }
        }



        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
           

            var result = await _schemeFileLoadingService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "Schemefileloading");
           

        }


        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _schemeFileLoadingService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        //[AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Praveen
        {
            try
            {

                var result = await _schemeFileLoadingService.Delete(id);
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
            var list = await _schemeFileLoadingService.GetAllSchemeFileLoading();
            return View("Index", list);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Schemefileloading> result = await _schemeFileLoadingService.GetAllSchemeFileLoading();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Schemefileloading.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> SchemeFileLoadingList()
        {
            var result = await _schemeFileLoadingService.GetAllSchemeFileLoading();
            List<SchemeFileLoadingListDto> data = new List<SchemeFileLoadingListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new SchemeFileLoadingListDto()
                    {
                        Id = result[i].Id,
                        SchemeName = result[i].SchemeName,
                        SchemeCode = result[i].SchemeCode,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }

}
