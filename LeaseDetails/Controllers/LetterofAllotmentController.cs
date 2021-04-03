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
    public class LetterofAllotmentController : Controller
    {
        
        private readonly ILeaseApplicationFormService _leaseApplicationFormService;
        public IActionResult Index()
        {
            return View();
        }
        public LetterofAllotmentController(ILeaseApplicationFormService leaseApplicationFormService)
        {
            _leaseApplicationFormService = leaseApplicationFormService;
        }

        public async Task<IActionResult> Create()
        {
            Leaseapplication leaseapplication = new Leaseapplication();
            leaseapplication.RefNoList = await _leaseApplicationFormService.GetRefNoListforAllotmentLetter();
            return View(leaseapplication);
        }

        public async Task<IActionResult> Receipt(int? ApplicationId)
        {
            var Data = await _leaseApplicationFormService.FetchLeaseApplicationDetailsforAllotmentLetter(ApplicationId ?? 0);
            return PartialView("Receipt", Data);
        }
    }
}
