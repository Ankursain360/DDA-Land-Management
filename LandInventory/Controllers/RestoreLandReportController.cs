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
    public class RestoreLandReportController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();    
        //}
        //public IActionResult Create()
        //{
        //    return View();
        //}
      
   
            private readonly IPropertyRegistrationService _propertyregistrationService;

            public RestoreLandReportController(IPropertyRegistrationService propertyregistrationService)
            {
                _propertyregistrationService = propertyregistrationService;
            }

        // public async Task<IActionResult> Restore(int id)  //Added by ishu
        //{
        //    if (id == 0)
        //    {
        //        return NotFound();
        //    }

        //    var form = await _propertyregistrationService.Restore(id);
        //    if (form == false)
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.Message = Alert.Show(Messages.RestoreSuccess, "", AlertType.Success);
        //    return View(form);
        //}
            async Task BindDropDown(Propertyregistration propertyregistration)
            {
          //     propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList();
               propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
          //     propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList();
            }
            public async Task<IActionResult> Create()
            {
                Propertyregistration propertyregistration = new Propertyregistration();
                //propertyregistration.LitigationStatus = 2;
                //propertyregistration.Encroached = 2;
                await BindDropDown(propertyregistration);
                return View(propertyregistration);
            }


            //public async Task<PartialViewResult> GetDetails( int department, int zone, int division)
            //{
            ////var result = await _propertyregistrationService.GetRestoreLandReportData(department, zone, division);

            ////if (result != null)
            ////{
            ////    return PartialView("Index", result);
            ////}
            ////else
            ////{
            ////    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            ////    return PartialView();
            ////}

            //return PartialView();
        //}
        }
    }

