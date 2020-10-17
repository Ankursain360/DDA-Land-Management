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
namespace EncroachmentDemolition.Controllers
{
    public class EncroachmentReportController : Controller
    {
        private readonly IEncroachmentRegisterationService _encroachmentregistrationService;

        public EncroachmentReportController(IEncroachmentRegisterationService encroachmentregistrationService)
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
        public async Task<PartialViewResult> GetDetails([FromBody]EnchroachmentSearchDto dto)
        {
            var result = await _encroachmentregistrationService.GetEncroachmentReportData(dto);
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



