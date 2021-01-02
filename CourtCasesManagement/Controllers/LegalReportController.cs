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

namespace CourtCasesManagement.Controllers
{
    public class LegalReportController : BaseController
    {
        private readonly ILegalmanagementsystemservice _legalmanagementsystemservice;
        public LegalReportController(ILegalmanagementsystemservice legalmanagementsystemservice)
        {
            _legalmanagementsystemservice = legalmanagementsystemservice;
        }

        async Task BindDropDownView(Legalmanagementsystem legalmanagementsystem)
        {
            legalmanagementsystem.CasestatusList = await _legalmanagementsystemservice.GetCasestatusList();
            legalmanagementsystem.CourttypeList = await _legalmanagementsystemservice.GetCourttypeList();
            legalmanagementsystem.ZoneList = await _legalmanagementsystemservice.GetZoneList();
            legalmanagementsystem.FileNoList = await _legalmanagementsystemservice.GetFileNoList();
        }
       
      
        public async Task<IActionResult> Index()
        {
            Legalmanagementsystem model = new Legalmanagementsystem();

            await BindDropDownView(model);
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] LegalReportSearchDto report)
        {
            var result = await _legalmanagementsystemservice.GetPagedLegalReport(report);
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

        // Dropdown Dependency  calls below
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _legalmanagementsystemservice.GetLocalityList(Convert.ToInt32(zoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetCourtCaseNoList(int? filenoId)
        {
            filenoId = filenoId ?? 0;
            return Json(await _legalmanagementsystemservice.GetCourtCaseNoList(Convert.ToInt32(filenoId)));
        }
    }
}
