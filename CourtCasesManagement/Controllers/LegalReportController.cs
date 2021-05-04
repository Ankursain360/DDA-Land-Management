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

        //public async Task<IActionResult> LegalReportList([FromBody] LegalReportSearchDto report)
        //{
           
        //    var result = await_legalmanagementsystemservice.GetPagedLegalReportForDownload(report);
        //    List<BranchListDto> data = new List<BranchListDto>();
        //    if (result != null)
        //    {
        //        for (int i = 0; i < result.Count; i++)
        //        {
        //            data.Add(new BranchListDto()
        //            {
        //                Id = result[i].Id,
        //                BranchCode = result[i].Code,
        //                BranchName = result[i].Name,
        //                Department = result[i].Department == null ? "" : result[i].Department.Name,
        //                IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
        //            });
        //        }
        //    }

        //    var memory = ExcelHelper.CreateExcel(data);
        //    return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //}


    }
}
