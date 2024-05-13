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

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Leaseapplication leaseapplication = new Leaseapplication();
            leaseapplication.RefNoList = await _demandletterService.GetRefNoListforAllotmentLetter();
            return View(leaseapplication);
        }




      
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
