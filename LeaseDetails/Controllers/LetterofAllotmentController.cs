using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Hosting;
using LeaseDetails.Filters;
using Core.Enum;
using System.Data;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


namespace LeaseDetails.Controllers
{
    public class LetterofAllotmentController : BaseController
    {

        private readonly ILeaseApplicationFormService _leaseApplicationFormService;
        private readonly IAllotmentLetterService _allotmentLetterService;
        public IActionResult Index()
        {
            return View();
        }
         [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AllotmentLetterSeearchDto model)
        {
            var result = await _allotmentLetterService.GetPagedAllotmentLetter(model);
            return PartialView("_List", result);
        }

        public LetterofAllotmentController(ILeaseApplicationFormService leaseApplicationFormService,IAllotmentLetterService allotmentLetterService)
        {
            _leaseApplicationFormService = leaseApplicationFormService;
            _allotmentLetterService = allotmentLetterService;
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
        public async Task<IActionResult> Save(int? ApplicationId)
        {
            var Data = await _leaseApplicationFormService.FetchLeaseApplicationDetailsforAllotmentLetter(ApplicationId ?? 0);

            Allotmentletter model= new Allotmentletter();


            if (ModelState.IsValid)
            {
                model.CreatedBy = SiteContext.UserId;
                model.IsActive = 1;
                model.AllotmentId = Convert.ToInt32(ApplicationId);
                model.ReferenceNumber = "txt/001";
                 DateTime theDate = DateTime.Now;
                model.DemandPeriodStart = theDate;
                 DateTime yearInTheFuture = theDate.AddYears(1);
                model.DemandPeriodEnd = yearInTheFuture;
                var result = await _allotmentLetterService.Create(model);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);


                }
            }
                return RedirectToAction("Create", "LetterofAllotment");

            
        }

        public async Task<IActionResult> Edit(int? ApplicationId)
        {
            Allotmentletter allotmentletter = new Allotmentletter();
            
            var Data = await _leaseApplicationFormService.FetchLeaseApplicationDetailsforAllotmentLetter(ApplicationId ?? 0);

            return View(allotmentletter);
        }
    }
}
