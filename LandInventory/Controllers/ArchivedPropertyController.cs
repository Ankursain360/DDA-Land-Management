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
    public class ArchivedPropertyController : Controller
    {
       
      
   
            private readonly IPropertyRegistrationService _propertyregistrationService;

            public ArchivedPropertyController(IPropertyRegistrationService propertyregistrationService)
            {
                _propertyregistrationService = propertyregistrationService;
            }

        public async Task<IActionResult> Restore(int id)  //Added by ishu
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _propertyregistrationService.Restore(id);
            if (form == false)
            {
                return NotFound();
            }

            ViewBag.Message = Alert.Show(Messages.RestoreSuccess, "", AlertType.Success);
            return View(form);
        }
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
        public async Task<IActionResult> Create()
            {
                Propertyregistration propertyregistration = new Propertyregistration();
               
                await BindDropDown(propertyregistration);
                return View(propertyregistration);
            }


        public async Task<PartialViewResult> GetDetails(int department, int zone, int division)
        {
            var result = await _propertyregistrationService.GetRestoreLandReportData(department, zone, division);

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

