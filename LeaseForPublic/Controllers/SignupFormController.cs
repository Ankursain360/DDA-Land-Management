using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Enum;
using LeaseForPublic.Filters;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using System.IO;



using Dto.Common;


namespace LeaseForPublic.Controllers
{
    public class SignupFormController : Controller
    {
        private readonly ILeasesignupService _leasesignupService;
        public IConfiguration _configuration;

        public SignupFormController(IConfiguration configuration, ILeasesignupService leasesignupService)

        {
            _configuration = configuration;
            _leasesignupService = leasesignupService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            Leasesignup leasesignup = new Leasesignup();

           
            return View(leasesignup);

        }
        [HttpPost]
      
        public async Task<IActionResult> Create(Leasesignup leasesignup)
        {
            try
            {
                Random random = new Random();
                var otp = random.Next(111111, 999999);


                string Action = "Otp is "+otp;
                String Mobile = leasesignup.MobileNo;
                String EmailID = leasesignup.EmailId;
                SendMailDto mail = new SendMailDto();
                SendSMSDto SMS = new SendSMSDto();
                SMS.GenerateSendSMS(Action, Mobile);
                mail.GenerateMailFormatForComplaint1("Nikita", EmailID, Action);

                if (ModelState.IsValid)
                {
                    var result = await _leasesignupService.Create(leasesignup);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                     
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(leasesignup);
                    }
                }
                else
                {
                    return View(leasesignup);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(leasesignup);
            }
        }



    }
}
