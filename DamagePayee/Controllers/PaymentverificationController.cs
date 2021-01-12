using System;
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

namespace DamagePayee.Controllers
{
    public class PaymentverificationController : BaseController
    {

        private readonly IPaymentverificationService _paymentverificationService;
      
        public PaymentverificationController(IPaymentverificationService paymentverificationService)
        {

            _paymentverificationService = paymentverificationService;
           
        }
        public IActionResult Index()
        
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PaymentverificationSearchDto model)
        {
            
                var result = await _paymentverificationService.GetPagedPaymentList(model);
                return PartialView("_List", result);
           
        }
        [HttpPost]
        public async Task<PartialViewResult> List2([FromBody] PaymentverificationSearchDto model)
        {
          
                var result = await _paymentverificationService.GetPagedPaymentList2(model);
                return PartialView("_List2", result);
          
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


    }
}
