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
    public class PaymentTransactionController :BaseController
    {
        private readonly IPaymentTransactionService _paymentTransactionService;

        public PaymentTransactionController(IPaymentTransactionService paymentTransactionService)
        {
            _paymentTransactionService = paymentTransactionService;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] PaymentTranscationReportDto model)
        {
            try
            {
               
                var result = await _paymentTransactionService.GetPagedPaymentTransactionReport(model);

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
            catch (Exception Ex)
            {

                return PartialView("_List", Ex);
            }
        }


    }
}
