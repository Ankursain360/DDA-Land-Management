using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
namespace LandInventory.Controllers
{
    public class DeletedPropertiesController : Controller
    {
        private readonly IPropertyRegistrationService _propertyregistrationService;

        public DeletedPropertiesController(IPropertyRegistrationService propertyregistrationService)
        {
            _propertyregistrationService = propertyregistrationService;
        }

        //public async Task<IActionResult> Restore(int id)
        //{
        //    var Data = await _propertyregistrationService.FetchSingleResult(id);
        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Restore(int id, Propertyregistration propertyregistration)  //Added by ishu
        //{
        //    Restoreproperty model = new Restoreproperty();
        //    if (id == 0)
        //    {
        //        return NotFound();
        //    }
        //    model.RestoreReason = propertyregistration.RestoreReason;
        //    var form = await _propertyregistrationService.Restore(id);
        //    var result = await _propertyregistrationService.InsertInRestoreProperty(id, model);
        //    if (form == false)
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.Message = Alert.Show(Messages.RestoreSuccess, "", AlertType.Success);
        //    return RedirectToAction("Create");
        //}
        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            //  propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            //propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList();
        }

        // Dropdown Dependency  calls below
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? departmentId)
        {
            departmentId = departmentId ?? 0;
            return Json(await _propertyregistrationService.GetZoneDropDownList(Convert.ToInt32(departmentId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _propertyregistrationService.GetDivisionDropDownList(Convert.ToInt32(zoneId)));
        }


        [HttpGet]
        public async Task<JsonResult> GetPrimaryNoList(int? divisionId)
        {
            divisionId = divisionId ?? 0;
            return Json(await _propertyregistrationService.GetPrimaryListNoList(Convert.ToInt32(divisionId)));
        }



        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();

            await BindDropDown(propertyregistration);
            return View(propertyregistration);
        }


        public async Task<PartialViewResult> GetDetails(int department, int zone, int division, int primaryListNo)
        {
            var result = await _propertyregistrationService.GetRestoreLandReportData(department, zone, division, primaryListNo);

            if (result != null)
            {
                return PartialView("Index", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }

        }
    }
}

