using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

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

      
    }
}
