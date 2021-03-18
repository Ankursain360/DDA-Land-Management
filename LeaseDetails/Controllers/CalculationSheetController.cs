using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LeaseDetails.Filters;
using Utility.Helper;

namespace LeaseDetails.Controllers
{
   
   public class CalculationSheetController : BaseController
    {
        private readonly ICalculationSheetService _calculationSheetService;

        public CalculationSheetController(ICalculationSheetService calculationSheetService)
        {
            _calculationSheetService = calculationSheetService;
        }
     
        public async Task<IActionResult> Index()
        {
            Allotmententry entry = new Allotmententry();
          
            entry.ApplicationList = await _calculationSheetService.GetAllApplications();
            return View(entry);
        }

        //[HttpGet]
        //public async Task<JsonResult> GetApplicationAreaDetails(int? ApplicationId)
        //{
        //    ApplicationId = ApplicationId ?? 0;

        //    return Json(await _calculationSheetService.FetchSingleAppAreaDetails(Convert.ToInt32(ApplicationId)));
        //}



        public async Task<IActionResult> Receipt(int? ApplicationId)
        {
            Allotmententry entry = await _calculationSheetService.FetchSingleAppAreaDetails(ApplicationId??0);
            entry.Date = DateTime.Now;
            return PartialView("Receipt", entry);
        }
    }
}
