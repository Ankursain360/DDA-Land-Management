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
    public class RestorePropertyReportController : Controller
    {
            private readonly IPropertyRegistrationService _propertyregistrationService;

            public RestorePropertyReportController(IPropertyRegistrationService propertyregistrationService)
            {
                _propertyregistrationService = propertyregistrationService;
            }
        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            //  propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            //propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList();
        }
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();

            await BindDropDown(propertyregistration);
            return View(propertyregistration);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody]PropertyRegisterationSearchDto model)
        {
            var result = await _propertyregistrationService.GetRestorePropertyReportData(model);

            if (result != null)
            {
                return PartialView("_Index", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }

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
    }
}
