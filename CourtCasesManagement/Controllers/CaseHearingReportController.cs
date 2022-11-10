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
using Dto.Master;
using Utility.Helper;

namespace CourtCasesManagement.Controllers
{
    public class CaseHearingReportController : BaseController
    {
        private readonly ILegalmanagementsystemservice _legalmanagementsystemService;

        public CaseHearingReportController(ILegalmanagementsystemservice legalmanagementsystemService)
        {
            _legalmanagementsystemService = legalmanagementsystemService;
        }
        public async Task<IActionResult> Index()
        {
            Legalmanagementsystem model = new Legalmanagementsystem();

            model.legalmanagementsytemlist = await _legalmanagementsystemService.GetLegalmanagementsystemList();
            return View(model);
        }

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
        public async Task<IActionResult> CaseHearningReportList([FromBody] HearingReportSearchDto hearingReportSearchDto)
        {
            var result = await _legalmanagementsystemService.GetAllLegalReportDataList(hearingReportSearchDto);
            List<HearingReportListDto> data = new List<HearingReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new HearingReportListDto()
                    {
                        //Id = result[i].Id,
                        fileNo = result[i].FileNo,
                        courtCaseNo = result[i].CourtCaseNo,
                        courtCaseTitle = result[i].CourtCaseTitle,
                        Subject = result[i].Subject,
                        HearingDate = Convert.ToDateTime(result[i].HearingDate).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].HearingDate).ToString("dd-MMM-yyyyy"),
                        NextHearingDate = Convert.ToDateTime(result[i].NextHearingDate).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].NextHearingDate).ToString("dd-MMM-yyyy"),
                        CaseType = result[i].CaseType,
                        InFavour = result[i].InFavour,
                        PanelLawyer = result[i].PanelLawyer,
                        
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }

        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
