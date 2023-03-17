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
using System.Collections.Generic;
using Utility.Helper;
using Microsoft.AspNetCore.Http;

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
            paymentverification.FromDate = DateTime.Now.AddDays(-30);
            paymentverification.ToDate = DateTime.Now;
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
        public async Task<IActionResult> GetDetailsList([FromBody] PaymentTransactionReportSearchDto model)
        {
            var result = await _paymentverificationService.GetPagedPaymentTransactionReportData(model);
            List<PaymentTransactionReportListDto> data = new List<PaymentTransactionReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new PaymentTransactionReportListDto()
                    {
                        FileNo = result[i].FileNo,
                        Locality = result[i].LocalityName,
                        PayeeName = result[i].PayeeName,
                        PropertyNo = result[i].PropertyNo,
                        Amountpaid = result[i].AmountPaid.ToString(),
                        TransactionId =result[i].TransactionId,
                        BankReference =result[i].BankReference,
                        BankName = result[i].BankName,
                        PaymentDate = result[i].CreatedDate,
                        PaymentMode = result[i].PaymentMode
                    });
                }
            }


            var memory = ExcelHelper.CreateExcel(data);
            HttpContext.Session.Set("file", memory);
            return Ok();
        }

        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = HttpContext.Session.Get("file") as byte[];
            HttpContext.Session.Remove("file");
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PaymentTransactionReport");
        }
    }
}

