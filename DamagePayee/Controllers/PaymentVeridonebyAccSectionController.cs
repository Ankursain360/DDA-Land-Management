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
using Utility.Helper;


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



        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> PaymentVerificationdetailsList()
        {
            var result = await _paymentverificationService.GetAllPaymentList();
            List<PaymentVerificationListDto> data = new List<PaymentVerificationListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new PaymentVerificationListDto()
                    {
                        Id = result[i].Id,

                        FileNo = result[i].FileNo,
                        PayeeName = result[i].PayeeName,
                        PropertyNo = result[i].PropertyNo,
                        AmountPaid = result[i].AmountPaid,

                        InterestPaid = result[i].InterestPaid,
                        TotalAmount = result[i].TotalAmount,
                        TransactionId = result[i].TransactionId,


                        BankTransactionId = result[i].BankTransactionId,
                        PaymentMode = result[i].PaymentMode,
                        BankName = result[i].BankName,

                        FromDateMsg = Convert.ToDateTime(result[i].FromDateMsg).ToString("dd-MMM-yyyy"),

                        ToDateMsg = Convert.ToDateTime(result[i].ToDateMsg).ToString("dd-MMM-yyyy"),

                        verified = result[i].IsVerified.ToString() == "1" ? "Verified" : "Unverified"
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
