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
using Dto.Master;
using DamagePayee.Filters;
using Core.Enum;
using Utility.Helper;

namespace DamagePayee.Controllers
{
    public class DemandsletterReport : BaseController
    {

        private readonly IDemandLetterService _demandLetterService;

        public DemandsletterReport(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            DemandletterReportDto demandletter = new DemandletterReportDto();
            ViewBag.PropertyNoList = await _demandLetterService.BindPropertyNoList();
            ViewBag.LocalityList = await _demandLetterService.BindLoclityList();
            demandletter.FromDate = DateTime.Now.AddDays(-30);
            demandletter.ToDate = DateTime.Now;
            return View(demandletter);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] DemandletterreportSearchDto report)
        {
            var result = await _demandLetterService.GetPagedDemandletterReport(report);
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
        // [AuthorizeContext(ViewAction.Download)]
        //[HttpPost]
        public async Task<IActionResult> DemandLetterReportList([FromBody] DownloadDemandLetterReportDto report)
        {
            var result = await _demandLetterService.GetDemandLetterReportList(report);
            List<DemandLetterReportListDto> data = new List<DemandLetterReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DemandLetterReportListDto()
                    {

                        Id = i + 1,
                        Loaclity = result[i].Locality.Name == null ? "" : result[i].Locality.Name,
                        FileNo = result[i].FileNo == null ? "" : result[i].FileNo,
                        PayeeName = result[i].Name == null ? "" : result[i].Name,
                        PropertyNumber = result[i].PropertyNo == null ? "NA" : result[i].PropertyNo,
                        DemandNo = result[i].DemandNo,
                        DemandDate = Convert.ToDateTime(result[i].CreatedDate).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].CreatedDate).ToString("dd-MMM-yyyy"),
                        DemandPeriodFromDate = Convert.ToDateTime(result[i].DemandPeriodFromDate).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].DemandPeriodFromDate).ToString("dd-MMM-yyyy"),
                        DemandPeriodToDate = Convert.ToDateTime(result[i].DemandPeriodToDate).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].DemandPeriodToDate).ToString("dd-MMM-yyyy"),
                        DemandAmount = result[i].DepositDue,

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
