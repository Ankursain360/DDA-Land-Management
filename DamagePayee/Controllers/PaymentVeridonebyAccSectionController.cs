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
    public class PaymentVeridonebyAccSectionController : Controller
    {
        private readonly IPaymentverificationService _paymentverificationService;

        public PaymentVeridonebyAccSectionController(IPaymentverificationService paymentverificationService)
        {
            _paymentverificationService = paymentverificationService;
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {

            Paymentverification payment = new Paymentverification();
            ViewBag.FileNoList = await _paymentverificationService.BindFileNoList();
            ViewBag.LocalityList = await _paymentverificationService.BindLoclityList();
            payment.FromDateMsg = DateTime.Now.AddDays(-30);
            payment.ToDateMsg = DateTime.Now;
            return View(payment);
        }

        [HttpPost]
        public async Task<PartialViewResult> ListPayemntVerification([FromBody] PaymentVerificationAccountSection model)
        {

            var result = await _paymentverificationService.GetPagedPaymentVerificationDoneByAcc(model);
            ViewBag.verificationststus = model.IsVerified;
            return PartialView("_ListallData", result);

        }



        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            ViewBag.IsShowData = "Yes";
            return View();
        }
    }
}
