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
    public class PaymentTransactionReportController : Controller
{
        private readonly IPaymentverificationService _paymentverificationService;

        public PaymentTransactionReportController(IPaymentverificationService paymentverificationService)
       {
            _paymentverificationService = paymentverificationService;
        }




        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            PaymentTransactionReportDtoProfile paymentverification = new PaymentTransactionReportDtoProfile();
            ViewBag.FileNoList = await _paymentverificationService.BindFileNoList();
            ViewBag.LocalityList = await _paymentverificationService.BindLoclityList();
            
            return View(paymentverification);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] PaymentTransactionReportSearchDto model)
        {
            var result = await _paymentverificationService.GetPagedPaymentTransactionReportData(model);

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

