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

namespace LeaseDetails.Controllers
{
    public class PaymentDescriptionController : BaseController
    {
        private readonly ILeaseApplicationFormService _paymentdescriptionservice;
        private readonly IAllotmentEntryService _paymentService;


        public PaymentDescriptionController(ILeaseApplicationFormService paymentdescriptionservice, IAllotmentEntryService paymentService)
        {
            _paymentdescriptionservice = paymentdescriptionservice;
            _paymentService = paymentService;


        }



        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            Leaseapplication leaseapplication = new Leaseapplication();
            leaseapplication.RefNoList = await _paymentdescriptionservice.GetRefNoListforAllotmentLetter();
            return View(leaseapplication);
        }



        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] PaymentdetailssearchDto model)
        {
            var result = await _paymentService.GetPagedPaymentReport(model);
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
