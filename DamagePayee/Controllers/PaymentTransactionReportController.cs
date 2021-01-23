//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;

//namespace DamagePayee.Controllers
//{
//    public class PaymentTransactionReportController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Index(int id)
//        {
//            ViewBag.IsShowData = "Yes";
//            return View();
//        }
//    }
//}

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
namespace DamagePayee.Controllers
{
    public class PaymentTransactionReportController : Controller
    {
        private readonly IPaymentverificationService _paymentverificationService;

        public PaymentTransactionReportController(IPaymentverificationService paymentverificationService)
        {
            _paymentverificationService = paymentverificationService;
        }
        public async Task<IActionResult> Index()
        {
            Paymentverification model = new Paymentverification();


            //model.LocalityList = await paymentverificationService.GetAllLocalityList();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] PaymentTransactionReportSearchDto paymentTransactionReportSearchDto)
        {
            var result = await _paymentverificationService.GetPaymentTransactionReportData(paymentTransactionReportSearchDto);
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

