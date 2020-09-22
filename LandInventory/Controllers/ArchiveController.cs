using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace LandInventory.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly IPropertyRegistrationService _propertyregistrationService;
        int UserId = 2;
        // private readonly IHostingEnvironment _hostingEnvironment;
        public ArchiveController(IPropertyRegistrationService propertyregistrationService)
        {
            _propertyregistrationService = propertyregistrationService;
        }


        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            //propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownList();
            propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList();
            propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList();
          //  propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
           // propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList();
        }

        public async Task<IActionResult> Index()
        {
            //int UserId = 2;
            var result = await _propertyregistrationService.GetAllPropertyregistration(UserId);
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();
            propertyregistration.IsActive = 1;
            await BindDropDown(propertyregistration);
            return View(propertyregistration);
        }
    }
}
