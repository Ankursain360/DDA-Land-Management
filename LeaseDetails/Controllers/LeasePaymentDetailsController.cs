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
        public async Task<IActionResult> Create(int id,string btnsubmit)
        {
            
            if (id == 0)
            {
                Leasepaymentdetails rate = new Leasepaymentdetails();
                rate.IsActive = 1;
                rate.AllotmententryList = await _LeasepaymentdetailsService.GetAllAllotmententry();
                rate.PaymenttypeList = await _LeasepaymentdetailsService.BindAllPaymentType();
                ViewBag.ActionName = "Create";
                
                return View(rate);
            }
            else
            {
                if (ViewBag.pass == 1 && id !=0)
                {
                    Leasepaymentdetails rate = new Leasepaymentdetails();
                    rate.IsActive = 1;
                    rate.AllotmententryList = await _LeasepaymentdetailsService.GetAllAllotmententry();
                    rate.PaymenttypeList = await _LeasepaymentdetailsService.BindAllPaymentType();
                     ViewBag.ActionName = "Submit Details";
                    return View(rate);
                }
                else
                {
                    var Data = await _LeasepaymentdetailsService.FetchSingleResult(id);
                    Data.AllotmententryList = await _LeasepaymentdetailsService.GetAllAllotmententry();
                    Data.PaymenttypeList = await _LeasepaymentdetailsService.BindAllPaymentType();
                    Data.IsActive = 1;
                    if (Data == null)
                    {
                        return NotFound();
                    }
                    ViewBag.ActionName = "Update";
                    return View(Data);
                }
                // return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Leasepaymentdetails rate, int id)
        {
            rate.AllotmententryList = await _LeasepaymentdetailsService.GetAllAllotmententry();
            rate.PaymenttypeList = await _LeasepaymentdetailsService.BindAllPaymentType();
            try
            {
                var result= false;

                if (ModelState.IsValid)
                {

                    if (id == 0)
                    //if (((pass == 2) || (pass == 0)) && (id == 0))
                    {
                        rate.CreatedBy = SiteContext.UserId;
                         result = await _LeasepaymentdetailsService.Create(rate);
                        ViewBag.pass = 0;
                        ModelState.Clear();
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            return RedirectToAction("Create", "LeasePaymentDetails");
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(rate);

                        }

                    }
                    else
                    {
                        rate.ModifiedBy = SiteContext.UserId;
                         result = await _LeasepaymentdetailsService.Update(id, rate);
                        ModelState.Clear();
                        ViewBag.pass = 1;
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            ViewBag.ActionName = "Create";
                            return View(rate);
                           // return RedirectToAction("Create", "LeasePaymentDetails");
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(rate);

                        }
                    }

                    //if (result == true)
                    //{
                    //    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    //    // RouteValueDictionary RouteInfo = new RouteValueDictionary();
                    //    //RouteInfo.Add("0", id);
                    //    //rate.Id = 0;
                    //    //return ViewBag.JavaScriptFunction = string.Format("ClearFields();");
                    //    //// ViewBag["pass"] = 2;
                    //    // ViewBag.pass = 1;
                    //    return View(rate);
                    //    //ViewBag.JavaScriptFunction = string.Format("ClearFields()");
                    //    //return RedirectToAction("Create", "LeasePaymentDetails");
                    //    ////  return RedirectToAction("Create", "LeasePaymentDetails", RouteInfo);

                    //}
                    
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



    }
}

