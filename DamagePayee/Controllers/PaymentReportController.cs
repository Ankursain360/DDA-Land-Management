using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using Dto.Search;
using Dto.Master;
using DamagePayee.Filters;
using Core.Enum;

namespace DamagePayee.Controllers
{
    public class PaymentReportController : BaseController
    {
        private readonly IPaymentverificationService _paymentverificationService;
        public PaymentReportController(IPaymentverificationService paymentverificationService)
        {
            _paymentverificationService = paymentverificationService;
        }
        //public async Task<IActionResult> Create()
        //{
        //    Paymentverification paymentverification = new Paymentverification();
        //    ViewBag.LocalityList = await _paymentverificationService.BindLocalityList();
        //    return View(paymentverification);
        //}
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] PaymentverificationSearchDto model)
        {
            var result = await _paymentverificationService.GetPagedPaymentListVerified(model);
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
