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
using CourtCasesManagement.Filters;
using Core.Enum;

namespace CourtCasesManagement.Controllers
{
    public class CaseHearingReportController : BaseController
    {
        private readonly ILegalmanagementsystemservice _legalmanagementsystemService;

        public CaseHearingReportController(ILegalmanagementsystemservice legalmanagementsystemService)
        {
            _legalmanagementsystemService = legalmanagementsystemService;
        }
        //public async Task<IActionResult> Index()
        //{
        //    Legalmanagementsystem model = new Legalmanagementsystem();

        //    model.legalmanagementsytemlist = await _legalmanagementsystemService.GetLegalmanagementsystemList();
        //    return View(model);
        //}

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] HearingReportSearchDto hearingReportSearchDto)
        {
            var result = await _legalmanagementsystemService.GetLegalmanagementsystemReportData(hearingReportSearchDto);
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
