﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using DamagePayee.Filters;
using Core.Enum;
using Utility.Helper;
namespace DamagePayee.Controllers
{
    public class PaymentverificationController : BaseController
    {

        private readonly IPaymentverificationService _paymentverificationService;
      
        public PaymentverificationController(IPaymentverificationService paymentverificationService)
        {

            _paymentverificationService = paymentverificationService;
           
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> ListUnverified([FromBody] PaymentverificationSearchDto model)
        {
            
                var result = await _paymentverificationService.GetPagedPaymentListUnverified(model);
                return PartialView("_ListUnverified", result);
           
        }
        [HttpPost]
        public async Task<PartialViewResult> ListVerified([FromBody] PaymentverificationSearchDto model)
        {
          
                var result = await _paymentverificationService.GetPagedPaymentListVerified(model);
                return PartialView("_ListVerified", result);
          
        }
       
        public async Task<IActionResult> Verify(int id)
        {
            var result = await _paymentverificationService.Verify(id, SiteContext.UserId);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.Verifiedsuccesfuly, "", AlertType.Success);
             
                return View("Index");
            }
            else
               return View("Index");
           
        }
        public async Task<IActionResult> Unverify(int id)
        {
            var result = await _paymentverificationService.Unverify(id, SiteContext.UserId);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.UnVerifysuccesfuly, "", AlertType.Success);

                return View("Index");
            }
            else
                return View("Index");

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
