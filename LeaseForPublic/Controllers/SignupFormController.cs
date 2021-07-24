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



using Service.IApplicationService;
using System.Text;



using Microsoft.AspNetCore.Http;

using Dto.Search;





namespace LeaseForPublic.Controllers
{
    public class SignupFormController : BaseController
    {
        private readonly ILeasesignupService _leasesignupService;
        public IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IKycformService _kycformService;
        private readonly IUserProfileService _userProfileService;
        private readonly IApprovalProccessService _approvalproccessService;
        string AadharDoc = "";
        string LetterDoc = "";
        string ApplicantDoc = "";


        public SignupFormController(IConfiguration configuration, ILeasesignupService leasesignupService, IHostingEnvironment hostingEnvironment,
            IKycformService KycformService,
            IUserProfileService userProfileService,
             IApprovalProccessService approvalproccessService)

        {
            _configuration = configuration;
            _leasesignupService = leasesignupService;
            _hostingEnvironment = hostingEnvironment;
            _kycformService = KycformService;
            AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
            LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
            ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();
            _userProfileService = userProfileService;
            _approvalproccessService = approvalproccessService;

        }
        public IActionResult Index()
        {

           // string str;
           // str = TempData["data1"].ToString();
          //  ViewBag.Message = TempData["data1"] as string;
           // ViewBag.Message = TempData["Message"] as string;
            //var Id = HttpContext.Session.GetString("Id");
            var mobile = HttpContext.Session.GetString("Mobile");
            return View();
        }

        //[HttpPost]
        //public async Task<PartialViewResult> KycList(string Mobileno)
        //{


        //    var mobile = HttpContext.Session.GetString("Mobile");
        //    Mobileno = mobile;
        //    var result = await _leasesignupService.GetAllKycformList(Mobileno);
        //    return PartialView("_List", result);
        //}



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Leasesignuplist model)
        {


            var mobile = HttpContext.Session.GetString("Mobile");
            model.Mobileno = mobile;
            var result = await _leasesignupService.AllKycformList(model);
            return PartialView("_List", result);
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
            HttpContext.Session.SetString("Mobile", model.MobileNo);
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
                var mobile = HttpContext.Session.GetString("Mobile");

                if (ModelState.IsValid)
                {
                    var result = await _leasesignupService.Create(leasesignup);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.RegistrationConfirm, "", AlertType.Success);
                       
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

            //TempData["data1"] = model.MobileNo;
          //  TempData["Message"] = Alert.Show(Messages.AddRecordSuccess + " Your Reference No is  " + model.MobileNo, "", AlertType.Success);


            // return str;
            // return RedirectToAction("Read");

            HttpContext.Session.SetString("Mobile", model.MobileNo);
            //  var mobile = HttpContext.Session.GetString("Mobile");
            JsonMsg.Add("true");
            JsonMsg.Add("Otp send successfully!");
            JsonMsg.Add(otp.ToString());
            return Json(JsonMsg);

        }





    }
}
