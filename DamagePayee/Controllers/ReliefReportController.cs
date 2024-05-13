using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using Dto.Search;
using Dto.Master;
using DamagePayee.Filters;
using Core.Enum;
using System.Collections.Generic;
using Utility.Helper;
using Microsoft.AspNetCore.Http;

namespace DamagePayee.Controllers
{
    public class ReliefReportController : BaseController
    {
        private readonly IDemandLetterService _demandLetterService;

        public ReliefReportController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ReliefReportDtoProfile demandletter = new ReliefReportDtoProfile();
            ViewBag.FileNoList = await _demandLetterService.BindFileNoList();
            ViewBag.LocalityList = await _demandLetterService.BindLoclityList();
            demandletter.FromDate = DateTime.Now.AddDays(-30);
            demandletter.ToDate = DateTime.Now;
            return View(demandletter);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] ReliefReportSearchDto model)
        {
            var result = await _demandLetterService.GetPagedReliefReport(model);

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

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> GetReliefReportList([FromBody] ReliefReportSearchDto model)
        {
            var result = await _demandLetterService.GetAllReliefReportList(model);
            List<ReliefReportListDto> data = new List<ReliefReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new ReliefReportListDto()
                    {
                        Locality = result[i].Locality == null ? "" : result[i].Locality.Name,
                        FileNo = result[i].FileNo,
                        PropertyNo = result[i].PropertyNo,
                        DemandNumber = result[i].DemandNo,
                        DemandAmount = result[i].DepositDue,
                        Rebate_ReliefAmount = result[i].ReliefAmount.ToString()
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReliefReport.xlsx");
        }
    
    }
}