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
using LandInventory.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace LandInventory.Controllers
{
    public class PropertyInventoryReport : Controller
    {
        private readonly IPropertyRegistrationService _propertyregistrationService;

        public PropertyInventoryReport(IPropertyRegistrationService propertyregistrationService)
        {
            _propertyregistrationService = propertyregistrationService;
        }

        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
        }
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();
            propertyregistration.LitigationStatus = 2;
            propertyregistration.Encroached = 2;
            await BindDropDown(propertyregistration);
            return View(propertyregistration);
        }



        [HttpPost]
        public async Task<IActionResult> Create(Propertyregistration propertyregistration)
        {
            await BindDropDown(propertyregistration);
            if (ModelState.IsValid)
            {
                return (RedirectToAction("Index", "PropertyInventoryReport", propertyregistration));

            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(propertyregistration);
            }

        }

        public async Task<PartialViewResult> Index(int department, int landUse, int litigation, int encroached)
        {
            var result = await _propertyregistrationService.GetPropertyRegisterationReportData(department, landUse, litigation, encroached);

            if (result != null)
            {
                return PartialView(result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }
    }
}
