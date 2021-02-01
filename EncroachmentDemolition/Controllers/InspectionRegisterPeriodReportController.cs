//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Libraries.Model.Entity;
//using Libraries.Service.IApplicationService;
//using Microsoft.AspNetCore.Mvc;
//using Notification;
//using Notification.Constants;
//using Notification.OptionEnums;

//namespace EncroachmentDemolition.Controllers
//{
//    public class InspectionRegisterPeriodReportController : Controller
//    {
//        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;

//        public InspectionRegisterPeriodReportController(IEncroachmentRegisterationService encroachmentRegisterationService)
//        {
//            _encroachmentRegisterationService = encroachmentRegisterationService;

//        }





//        [HttpGet]
//        public async Task<JsonResult> GetZoneList(int? DepartmentId)
//        {
//            DepartmentId = DepartmentId ?? 0;
//            return Json(await _encroachmentRegisterationService.GetAllZone(Convert.ToInt32(DepartmentId)));
//        }
//        [HttpGet]
//        public async Task<JsonResult> GetDivisionList(int? ZoneId)
//        {
//            ZoneId = ZoneId ?? 0;
//            return Json(await _encroachmentRegisterationService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
//        }
//        [HttpGet]
//        public async Task<JsonResult> GetLocalityList(int? DivisionId)
//        {
//            DivisionId = DivisionId ?? 0;
//            return Json(await _encroachmentRegisterationService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
//        }


//        public async Task<IActionResult> Create()
//        {
//            EncroachmentRegisteration model = new EncroachmentRegisteration();
//            model.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
//            model.ZoneList = await _encroachmentRegisterationService.GetAllZone(model.DepartmentId);
//            model.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(model.ZoneId);
//            model.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(model.DivisionId);

//          //  model.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(model.DivisionId);
//            return View(model);
//        }






//        public async Task<PartialViewResult> GetDetails(int department, int zone, int division, int locality, DateTime fromdate, DateTime todate)
//        {
//            var result = await _encroachmentRegisterationService.GetEncroachmentRegisterationReportData(department, zone, division, locality, fromdate, todate);

//            if (result != null)
//            {
//                return PartialView("Index", result);
//            }
//            else
//            {
//                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
//                return PartialView();
//            }

//        }


//
//    }
//}
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
using EncroachmentDemolition.Filters;
using Core.Enum;

namespace EncroachmentDemolition.Controllers
{
    public class InspectionRegisterPeriodReportController : Controller
    {
        private readonly IEncroachmentRegisterationService _encroachmentregistrationService;

        public InspectionRegisterPeriodReportController(IEncroachmentRegisterationService encroachmentregistrationService)
        {
            _encroachmentregistrationService = encroachmentregistrationService;
        }
        // Dropdown Dependency  calls below
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _encroachmentregistrationService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _encroachmentregistrationService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _encroachmentregistrationService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            EncroachmentRegisteration model = new EncroachmentRegisteration();
            model.DepartmentList = await _encroachmentregistrationService.GetAllDepartment();
            model.ZoneList = await _encroachmentregistrationService.GetAllZone(model.DepartmentId);
            model.DivisionList = await _encroachmentregistrationService.GetAllDivisionList(model.ZoneId);
            model.LocalityList = await _encroachmentregistrationService.GetAllLocalityList(model.DivisionId);
            return View(model);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] InspectionEncroachmentregistrationSearchDto dto)
        {
            var result = await _encroachmentregistrationService.GetEncroachmentRegisterationReportData(dto);
            if (result != null)
            {
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }
    }
}



