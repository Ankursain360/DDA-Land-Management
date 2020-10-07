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

namespace LandTransfer.Controllers
{
    public class ReportofLandTransferDepartmentWiseController : Controller
    {
        private readonly ILandTransferService _landTransferService;

        public ReportofLandTransferDepartmentWiseController(ILandTransferService landTransferService)
        {
            _landTransferService = landTransferService;
        }

        //async Task GetAllDepartment(Landtransfer landtransfer)
        //{
        //    //  propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList();
        //    landtransfer.departmentList = await _propertyregistrationService.GetDepartmentDropDownList();
        //    //propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList();
        //}

        public IActionResult Index()
        {
            return View();
        }
    }
}