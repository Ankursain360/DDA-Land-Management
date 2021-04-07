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
    public class DemandLetterController : BaseController
    {
        private readonly ILeaseApplicationFormService _demandletterService;
        private readonly IAllotmentEntryService _demandlettersService;


        public DemandLetterController(ILeaseApplicationFormService demandletterService, IAllotmentEntryService demandlettersService)
        {
            _demandletterService = demandletterService;
            _demandlettersService = demandlettersService;
        }


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Create()
        {
            Leaseapplication leaseapplication = new Leaseapplication();
            leaseapplication.RefNoList = await _demandletterService.GetRefNoListforAllotmentLetter();
            return View(leaseapplication);
        }




        //public async Task<IActionResult> Create()
        //{
        //    Allotmententry entry = new Allotmententry();

        //    entry.ApplicationList = await _DemandletterService.GetAllApplications();
        //    return View(entry);
        //}


        //public async Task<IActionResult> Receipt(int? ApplicationId)
        //{
        //    Allotmententry entry = await _DemandletterService.FetchSingleAppAreaDetails(ApplicationId ?? 0);
        //  //  entry.Date = DateTime.Now;
        //    return PartialView("_List", entry);
        //}

        //public async Task<IActionResult> List(int? ApplicationId)
        //{
        //    var Data = await _demandletterService.FetchLeaseApplicationDetailsforAllotmentLetter(ApplicationId ?? 0);
        //    return PartialView("_List", Data);
        //}

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemandletterDateSearchDto report)
        {
            
            var result = await _demandlettersService.Getdemandletteralldata(report);
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
