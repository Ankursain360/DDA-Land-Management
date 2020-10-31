using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace LandInventory.Controllers
{
    public class ArchivedPropertyController : Controller
    {
        private readonly IPropertyRegistrationService _propertyregistrationService;
        public ArchivedPropertyController(IPropertyRegistrationService propertyregistrationService)
        {
            _propertyregistrationService = propertyregistrationService;
        }
        public async Task<IActionResult> Restore(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int id, Propertyregistration propertyregistration)  //Added by ishu
        {
            Restoreproperty model = new Restoreproperty();
            if (id == 0)
            {
                return NotFound();
            }
            model.RestoreReason = propertyregistration.RestoreReason;
            var form = await _propertyregistrationService.Restore(id);
            var result = await _propertyregistrationService.InsertInRestoreProperty(id, model);
            if (form == false)
            {
                return NotFound();
            }

            ViewBag.Message = Alert.Show(Messages.RestoreSuccess, "", AlertType.Success);
            return RedirectToAction("Create");
        }
        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
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
        public async Task<JsonResult> GetLocalityList(int? divisionId)
        {
            try
            {
                divisionId = divisionId ?? 0;
                return Json(await _propertyregistrationService.GetLocalityDropDownList2(Convert.ToInt32(divisionId)));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();

            await BindDropDownView(propertyregistration);
            return View(propertyregistration);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody]PropertyRegisterationSearchDto model)
        {
            var result = await _propertyregistrationService.GetRestoreLandReportData(model);
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


        async Task BindDropDownView(Propertyregistration propertyregistration)
        {
            propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownListReport();
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList(); propertyregistration.TakenOverDepartmentList = await _propertyregistrationService.GetTakenDepartmentDropDownList();
            propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetHandedDepartmentDropDownList();
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            ViewBag.LayoutDocView = Data.LayoutFilePath;
            ViewBag.GeoDocView = Data.GeoFilePath;
            ViewBag.TakenOverDocView = Data.TakenOverFilePath;
            ViewBag.HandedOverDocView = Data.HandedOverFilePath;
            ViewBag.DisposalTypeDocView = Data.DisposalTypeFilePath;
            await BindDropDownView(Data);

            Data.ZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(Data.ZoneId);
            Data.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            Data.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            Data.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}