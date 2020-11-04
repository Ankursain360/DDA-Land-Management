using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Service.IApplicationService;
using Utility.Helper;

namespace LandTransfer.Controllers
{
    public class PlanningController : Controller
    {

        public IConfiguration _configuration;
        public readonly IPropertyRegistrationService _propertyregistrationService;




        public PlanningController(IPropertyRegistrationService propertyregistrationService, IConfiguration configuration)
        {
            _propertyregistrationService = propertyregistrationService;

            _configuration = configuration;
              }






        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> ListPage(Propertyregistration propertyregistration)
        {
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();

            propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList(propertyregistration.DepartmentId);
           // propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(propertyregistration.ZoneId);
            propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(propertyregistration.ZoneId);

            return View(propertyregistration);
        }


    }
}
