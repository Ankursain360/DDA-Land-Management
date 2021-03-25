using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using LeaseDetails.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Microsoft.AspNetCore.Http;
using System.IO;
using LeaseDetails.Filters;
using Core.Enum;
using Microsoft.AspNetCore.Routing;

namespace LeaseDetails.Controllers
{
    public class LeasePaymentDetailsController : BaseController
    {
        private readonly ILeasepaymentdetailsService _LeasepaymentdetailsService;
        public IConfiguration _configuration;
        int pass=2;
        public LeasePaymentDetailsController(ILeasepaymentdetailsService LeasepaymentdetailsService, IConfiguration configuration)
        {
            _configuration = configuration;
            _LeasepaymentdetailsService = LeasepaymentdetailsService;
        }
        //  [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> BindLeaseApplicationDetails(int? appId)
        {
            appId = appId ?? 0;
            var leaseappId = 0;
            var result = await _LeasepaymentdetailsService.BindAllotmentDetails(Convert.ToInt32(appId));
            if (result != null)
            {
                leaseappId = result[0].ApplicationId;

            }
            return Json(await _LeasepaymentdetailsService.BindLeaseApplicationDetails(Convert.ToInt32(leaseappId)));

        }


        //   [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {


            Leasepaymentdetails rate = new Leasepaymentdetails();
            rate.IsActive = 1;
            rate.AllotmententryList = await _LeasepaymentdetailsService.GetAllAllotmententry();
            rate.PaymenttypeList = await _LeasepaymentdetailsService.BindAllPaymentType();
            ViewBag.ActionName = "Create";

            return View(rate);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Leasepaymentdetails rate)
        {
            rate.AllotmententryList = await _LeasepaymentdetailsService.GetAllAllotmententry();
            rate.PaymenttypeList = await _LeasepaymentdetailsService.BindAllPaymentType();
            try
            {
                var result = false;

                if (ModelState.IsValid)
                {
                    rate.CreatedBy = SiteContext.UserId;
                    result = await _LeasepaymentdetailsService.Create(rate);
                    ViewBag.pass = 0;
                    ModelState.Clear();
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return RedirectToAction("Create", "Test");
                    }

                    else
                    {

                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rate);

                    }




                }
                else
                {
                    return View(rate);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(rate);
            }
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LeasepaymentdetailsSearchDto model)
        {

            var result = await _LeasepaymentdetailsService.GetPagedLeasepaymentdetails(model);
            return PartialView("_List", result);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _LeasepaymentdetailsService.FetchSingleResult(id);
            Data.AllotmententryList = await _LeasepaymentdetailsService.GetAllAllotmententry();
            Data.PaymenttypeList = await _LeasepaymentdetailsService.BindAllPaymentType();
            Data.IsActive = 1;
            Data.ModifiedBy = SiteContext.UserId;

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Leasepaymentdetails leasepaymentdetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    leasepaymentdetails.ModifiedBy = SiteContext.UserId;
                    var result = await _LeasepaymentdetailsService.Update(id, leasepaymentdetails);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        //var list = await _groundRentService.GetAllGroundRent();
                        //return View("Index", list);
                        return RedirectToAction("Create", "Test");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(leasepaymentdetails);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(leasepaymentdetails);
                }
            }
            else
            {
                return View(leasepaymentdetails);
            }
        }

    }
}
