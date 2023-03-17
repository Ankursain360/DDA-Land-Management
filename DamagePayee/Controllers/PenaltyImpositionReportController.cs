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
using DamagePayee.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using Microsoft.AspNetCore.Http;

namespace DamagePayee.Controllers
{
    public class PenaltyImpositionReportController : BaseController
    {
        private readonly IDemandLetterService _demandLetterService;
        public PenaltyImpositionReportController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }

        async Task BindDropDownView(Demandletters demandletters)
        {
            demandletters.LocalityList = await _demandLetterService.GetLocalityList();
            demandletters.FileNoList = await _demandLetterService.GetFileNoList();
        }
       

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Demandletters model = new Demandletters();

            await BindDropDownView(model);
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] PenaltyImpositionReportSearchDto report)
        {
            var result = await _demandLetterService.GetPagedPenaltyImpositionReport(report);
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




        public async Task<IActionResult> PenaltyImpositionReportList([FromBody] PenaltyImpositionReportSearchDto report)
        {
            var result = await _demandLetterService.GetPenaltyImpositionReportList(report);
            List<PenalityImpositionReportListDto> data = new List<PenalityImpositionReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new PenalityImpositionReportListDto()
                    {
                        Id = result[i].Id,
                        Locality = result[i].Locality == null ? "" : result[i].Locality.Name.ToString(),
                        FileNo = result[i].FileNo,
                        PropertyNumber = result[i].PropertyNo,
                        PayeeName = result[i].Name,
                        DemandNo = result[i].DemandNo,
                        PenaltyInterestCharges = result[i].Penalty.ToString()
                       
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            HttpContext.Session.Set("file", memory);
            return Ok();

        }
        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = HttpContext.Session.Get("file") as byte[];
            HttpContext.Session.Remove("file");
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PenaltyImpositionReport.xlsx");
        }

    }
}
