using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace DamagePayeePublicInterface.Controllers
{
    public class PaymentController  : BaseController
    {

        private readonly IDPPublicPaymentService _dPPublicPaymentService;
        public IConfiguration _configuration;
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;
        public PaymentController(IDPPublicPaymentService dPPublicPaymentService, IDamagepayeeregisterService damagepayeeregisterService)
        {
           
            _dPPublicPaymentService = dPPublicPaymentService;
            _damagepayeeregisterService = damagepayeeregisterService;

        }
        public async Task<IActionResult> Index()
        {
            string FileNo = _damagepayeeregisterService.GetFileNo(SiteContext.UserId);

            //var Data = await _dPPublicPaymentService.FetchDamagePayeeRegisterDetails(SiteContext.UserId);
            // Get Payment Details of Payment Details By Pankaj
            var Data = await _dPPublicPaymentService.FatchDemandPaymentDetails(FileNo);
            if ( Data == null)
            {
                return View();
            }
            else 
            {
                var data1 = await _dPPublicPaymentService.GetDemandDetails(Data.FileNo);
                return View(data1);
            }
            
        }
    }
}
