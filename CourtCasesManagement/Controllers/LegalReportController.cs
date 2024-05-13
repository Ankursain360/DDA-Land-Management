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
using Microsoft.AspNetCore.Http;

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

        [AuthorizeContext(ViewAction.View)]
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

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> LegalReportList([FromBody] LegalReportSearchDto report)
        {

            var result = await _legalmanagementsystemservice.GetAllLegalReportList(report);
            List<LegalManagementSystemListDto> data = new List<LegalManagementSystemListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new LegalManagementSystemListDto()
                    {
                        Id = result.Select(x => x.Id).FirstOrDefault(),
                        LegalfileNo = result[i].FileNo,
                        LMFileNo = result[i].LMFileNO,
                        courtCaseNo = result[i].CourtCaseNo,
                        courtCaseTitle = result[i].CourtCaseTitle,
                        Subject = result[i].Subject,
                        HearingDate = result[i].HearingDate.HasValue ? Convert.ToDateTime(result[i].HearingDate).ToString("dd-MMM-yyyyy") : "",
                        NextHearingDate = result[i].NextHearingDate.HasValue ? Convert.ToDateTime(result[i].NextHearingDate).ToString("dd-MMM-yyyy") : "",
                        ContemptOfCourt = result[i].ContemptOfCourt == null ? "" : result[i].ContemptOfCourt.ToString() == "1" ? "Yes" : "No",
                        Courttype = result[i].CourtType == null ? "" : result[i].CourtType.CourtType,
                        Casestatus = result[i].CaseStatus == null ? "" : result[i].CaseStatus.CaseStatus,
                        LastDecision = result[i].LastDecision,
                        Zone = result[i].Zone == null ? "" : result[i].Zone.Name,
                        Locality = result[i].Locality == null ? " " : result[i].Locality.Name,
                        CaseType = result[i].CaseType,
                        InFavour = result[i].InFavour,
                        PanelLawyer = result[i].PanelLawyer,
                        StayInterimGranted = result[i].StayInterimGranted == null ? "" : result[i].StayInterimGranted.ToString() == "1" ? "Yes" : "No",
                        Judgement = result[i].Judgement.ToString() == "1" ? "Yes" : "No",
                        Remarks = result[i].Remarks,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",

                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            HttpContext.Session.Set("file", memory);
            return Ok();

        }

        [HttpGet]
        [AuthorizeContext(ViewAction.Download)]
        public virtual ActionResult download()
        {
            byte[] data = HttpContext.Session.Get("file") as byte[];
            HttpContext.Session.Remove("file");
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LegalReport.xlsx");
        }


        //public async Task<IActionResult> LegalReportList()
        //{
        //    var result = await _legalmanagementsystemservice.GetAllLegalmanagementsystem();
        //    List<LegalManagementSystemListDto> data = new List<LegalManagementSystemListDto>();
        //    if (result != null)
        //    {
        //        for (int i = 0; i < result.Count; i++)
        //        {
        //            data.Add(new LegalManagementSystemListDto()
        //            {
        //                Id = result[i].Id,
        //                fileNo = result[i].FileNo,
        //                courtCaseNo = result[i].CourtCaseNo,
        //                courtCaseTitle = result[i].CourtCaseTitle,
        //                Subject = result[i].Subject,
        //                HearingDate = Convert.ToDateTime(result[i].HearingDate).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].HearingDate).ToString("dd-MMM-yyyyy"),
        //                NextHearingDate = Convert.ToDateTime(result[i].NextHearingDate).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].NextHearingDate).ToString("dd-MMM-yyyy"),
        //                ContemptOfCourt = result[i].ContemptOfCourt.ToString() == "1" ? "Yes" : "No",
        //                Courttype = result[i].CourtType.CourtType == null ? "" : result[i].CourtType.CourtType,
        //                Casestatus = result[i].CaseStatus.CaseStatus == null ? "" : result[i].CaseStatus.CaseStatus,
        //                LastDecision = result[i].LastDecision,
        //                Zone = result[i].Zone == null ? "" : result[i].Zone.Name,
        //                Locality = result[i].Locality == null ? " " : result[i].Locality.Name,
        //                CaseType = result[i].CaseType,
        //                InFavour = result[i].InFavour,
        //                PanelLawyer = result[i].PanelLawyer,
        //                StayInterimGranted = result[i].StayInterimGranted.ToString() == "1" ? "Yes" : "No",
        //                Judgement = result[i].Judgement.ToString() == "1" ? "Yes" : "No",
        //                Remarks = result[i].Remarks,
        //                Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
        //            });
        //        }
        //    }

        //    var memory = ExcelHelper.CreateExcel(data);
        //    return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //}


    }
}
