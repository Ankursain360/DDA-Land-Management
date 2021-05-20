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
using Microsoft.AspNetCore.Mvc.Rendering;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;

namespace SiteMaster.Controllers
{
    public class DamageRateListController : BaseController
    {

        private readonly IDamageRateListService _damageRateListService;

        public DamageRateListController(IDamageRateListService damageRateListService)
        {
            _damageRateListService = damageRateListService;
        }
      
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            DamageRateListCreateDto damageRateListCreatedto = new DamageRateListCreateDto();
            ViewBag.PropertyTypeList = await _damageRateListService.GetPropertyTypes();
            ViewBag.LocalityList = await _damageRateListService.GetLocalities();
            return View(damageRateListCreatedto);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamageRateListSearchDto model)
        {
            List<DamageRateListDataDto> damageRateListDataDtos = new List<DamageRateListDataDto>();
            var result = await _damageRateListService.GetSearchResultPagedData(model, damageRateListDataDtos);
            return PartialView("_List", result);
        }

        //[HttpPost]
        //[AuthorizeContext(ViewAction.Add)]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Zone zone)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            await BindDropDown(zone);
        //            var result = await _zoneService.Create(zone);

        //            if (result == true)
        //            {
        //                ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
        //                var result1 = await _zoneService.GetAllDetails();
        //                return View("Index", result1);
        //            }
        //            else
        //            {
        //                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //                return View(zone);

        //            }
        //        }
        //        else
        //        {
        //            return View(zone);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        return View(zone);
        //    }
        //}
        //[AuthorizeContext(ViewAction.Edit)]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var Data = await _zoneService.FetchSingleResult(id);
        //    await BindDropDown(Data);
        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}

        //[HttpPost]
        //[AuthorizeContext(ViewAction.Edit)]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Zone zone)
        //{
        //    await BindDropDown(zone);
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            var result = await _zoneService.Update(id, zone);
        //            if (result == true)
        //            {
        //                ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
        //                var result1 = await _zoneService.GetAllDetails();
        //                return View("Index", result1);
        //            }
        //            else
        //            {
        //                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //                return View(zone);

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //            return View(zone);
        //        }
        //    }
        //    return View(zone);
        //}


        //public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        //{
        //    var result = await _zoneService.Delete(id);
        //    if (result == true)
        //    {
        //        ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
        //    }
        //    else
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //    }
        //    return RedirectToAction("Index", "Zone");

        //}
        //[AuthorizeContext(ViewAction.View)]
        //public async Task<IActionResult> View(int id)
        //{
        //    var Data = await _zoneService.FetchSingleResult(id);
        //    await BindDropDown(Data);
        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}

        [HttpGet]
        public async Task<JsonResult> GetDateRangeList(int? Id)
        {
            Id = Id ?? 0;
            if (Id == 1)
            {
                var data = await _damageRateListService.GetDateRangeDropdownListResidential();
                return Json(data);
            }
            else
            {
                var data = await _damageRateListService.GetDateRangeDropdownListCommercial();
                return Json(data);
            }
        }





    }
}