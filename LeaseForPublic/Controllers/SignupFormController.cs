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
using Microsoft.AspNetCore.Hosting;

using Dto.Master;
using Dto.Common;






using Microsoft.AspNetCore.Http;







namespace LeaseForPublic.Controllers
{
    public class SignupFormController : Controller
    {
        private readonly ILeasesignupService _leasesignupService;
        public IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        public SignupFormController(IConfiguration configuration, ILeasesignupService leasesignupService, IHostingEnvironment hostingEnvironment)

        {
            _configuration = configuration;
            _leasesignupService = leasesignupService;
            _hostingEnvironment = hostingEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateLogin()
        {
            Leasesignup leasesignup = new Leasesignup();


            return View(leasesignup);
        }

        public async Task<IActionResult> Create()
        {
            Leasesignup leasesignup = new Leasesignup();


            return View(leasesignup);

        }



        //[HttpPost]
        //public async Task<PartialViewResult> sendotp([FromBody] DamagePayeeRegistrationSearchDto model)
        //{
        //    var result = await _damagePayeeRegistrationService.GetPagedDamagePayeeRegistration(model);
        //    return PartialView("_List", result);
        //}


        [HttpPost]
        public async Task<IActionResult> sendotp([FromBody] Leasesignup model)
        {

            List<string> JsonMsg = new List<string>();
            var IsEmailExist = await _leasesignupService.ValidateMobileEmail(model.MobileNo, model.EmailId);
            if (IsEmailExist)
            {
                JsonMsg.Add("false");
                JsonMsg.Add("This Email Id and Mobile is already Linked with Another Account. Kindly use different Email Id");
                return Json(JsonMsg);
            }

            string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "OTPMailDesign.html");



            var sendMailResult = false;
            Random random = new Random();
            var otp = random.Next(111111, 999999);


            string Action = "Otp is " + otp;
            String Mobile = model.MobileNo;
            String EmailID = model.EmailId;


            MailSMSHelper mail = new MailSMSHelper();

            #region HTML Body Generation
            LeaseSignupMailDto bodyDTO = new LeaseSignupMailDto();

            bodyDTO.Otp = otp.ToString();

            bodyDTO.path = path;
            string strBodyMsg = mail.GenerateMailFormatForLeaseSignUp(bodyDTO);
            #endregion




            #region Common Mail Genration
            SentMailGenerationDto maildto = new SentMailGenerationDto();
            maildto.strMailSubject = "OTP Request";
            maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
            maildto.strBodyMsg = strBodyMsg;
            maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
            maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
            maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
            maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
            maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

            maildto.strMailTo = model.EmailId;
            sendMailResult = mail.SendMailWithAttachment(maildto);
            #endregion


            SendSMSDto SMS = new SendSMSDto();
            SMS.GenerateSendSMS(Action, Mobile);

            JsonMsg.Add("true");
            JsonMsg.Add("Otp send successfully!");
            JsonMsg.Add(otp.ToString());
            return Json(JsonMsg);

        }


        [HttpPost]

        public async Task<IActionResult> Create(Leasesignup leasesignup)
        {
            try
            {

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





        [HttpPost]

        public async Task<IActionResult> CreateLogin(Leasesignup leasesignup)
        {
            try
            {

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










        [HttpPost]
        public async Task<IActionResult> sendotp1([FromBody] Leasesignup model)
        {

            List<string> JsonMsg = new List<string>();
            var IsEmailExist = await _leasesignupService.ValidateMobileEmail(model.MobileNo, model.EmailId);
            if (!IsEmailExist)
            {
                JsonMsg.Add("false");
                JsonMsg.Add("This Mobile Number is not Registered. Please Signup");
                return Json(JsonMsg);
            }

            string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "OTPMailDesign.html");

            var sendMailResult = false;
            Random random = new Random();
            var otp = random.Next(111111, 999999);


            string Action = "Otp is " + otp;
            String Mobile = model.MobileNo;
            String EmailID = model.EmailId;


            MailSMSHelper mail = new MailSMSHelper();

            #region HTML Body Generation
            LeaseSignupMailDto bodyDTO = new LeaseSignupMailDto();

            bodyDTO.Otp = otp.ToString();

            bodyDTO.path = path;
            string strBodyMsg = mail.GenerateMailFormatForLeaseSignUp(bodyDTO);
            #endregion




            #region Common Mail Genration
            SentMailGenerationDto maildto = new SentMailGenerationDto();
            maildto.strMailSubject = "OTP Request";
            maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
            maildto.strBodyMsg = strBodyMsg;
            maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
            maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
            maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
            maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
            maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

            maildto.strMailTo = model.EmailId;
            sendMailResult = mail.SendMailWithAttachment(maildto);
            #endregion


            SendSMSDto SMS = new SendSMSDto();
            SMS.GenerateSendSMS(Action, Mobile);

            JsonMsg.Add("true");
            JsonMsg.Add("Otp send successfully!");
            JsonMsg.Add(otp.ToString());
            return Json(JsonMsg);

        }





    }
}
