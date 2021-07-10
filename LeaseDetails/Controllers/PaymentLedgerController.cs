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
using LeaseDetails.Filters;
using Core.Enum;


namespace LeaseDetails.Controllers
{
    public class PaymentLedgerController : BaseController
    {
        private readonly IPaymentTransactionService _paymentTransactionService;

        public PaymentLedgerController(IPaymentTransactionService paymentTransactionService)
        {
            _paymentTransactionService = paymentTransactionService;
        }
        public async Task<IActionResult> Index()
        {
            Payment payment = new Payment();
            payment.ApplicationNoList = await _paymentTransactionService.GetAllAllotmententry();
            payment.FromDate = DateTime.Now.AddDays(-30);
            payment.ToDate = DateTime.Now;
            return View(payment);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PaymentLedgerSearchDto model)
        {

            if (model != null)
            {
                var result = await _paymentTransactionService.GetPagedPaymentLedgerReport(model);
                
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show("No Data Found", "", AlertType.Warning);
                return PartialView("_List", null);
            }
        }

    }
}
