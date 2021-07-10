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

    }
}
