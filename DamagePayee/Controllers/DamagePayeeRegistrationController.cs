using System;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System.Web;
using DamagePayee.Models;

using System.Net;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Utility.Helper;
using AutoMapper.Configuration;
using System.Text.Encodings.Web;
using CCA.Util;
using System.Linq;
using Unidecode.NET;
using Microsoft.AspNetCore.Http;
using Dto.Master;
using Service.IApplicationService;

namespace DamagePayee.Controllers
{
    [AllowAnonymous]
    public class DamagePayeeRegistrationController : BaseController
    {
       
        static string result = string.Empty;
        private readonly IHostingEnvironment _hostingEnvironment;
        public Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IDamagePayeeRegistrationService _damagePayeeRegistrationService;
        private readonly IUserProfileService _userProfileService;
        public DamagePayeeRegistrationController(Microsoft.Extensions.Configuration.IConfiguration configuration,
            IDamagePayeeRegistrationService damagePayeeRegistrationService,
            IHostingEnvironment en,
            IUserProfileService userProfileService)
        {
            _damagePayeeRegistrationService = damagePayeeRegistrationService;
            _configuration = configuration;
            _hostingEnvironment = en;
            _userProfileService = userProfileService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagePayeeRegistrationSearchDto model)
        {
            var result = await _damagePayeeRegistrationService.GetPagedDamagePayeeRegistration(model);
            return PartialView("_List", result);
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            var Msg = TempData["Message"] as string;
            if (Msg != null)
                ViewBag.Message = Msg;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
               
        public async Task<IActionResult> Create(Payeeregistration payeeregistration)
        {
            try
            {
                if (!Captcha.ValidateCaptchaCode(payeeregistration.CaptchaCode, HttpContext))
                {
                    ModelState.AddModelError("captcha", "Invalid Captcha.");
                }
                if (ModelState.IsValid)
                {
                    //    // Validate Captcha Code
                    //if (!Captcha.ValidateCaptchaCode(payeeregistration.CaptchaCode, HttpContext))
                    //{
                    //    ViewBag.Message = Alert.Show("Invalid Catacha.", "", AlertType.Error);
                    //    return View(payeeregistration);
                    //}
                    var AesKey = _configuration.GetSection("EncryptionKey").Value.ToString();
                    payeeregistration.IsVerified = "F";
                    var result = await _damagePayeeRegistrationService.Create(payeeregistration);

                    if (result == true)
                    {
                        EncryptionHelper encryptionHelper = new EncryptionHelper();
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //At successfull completion send mail and sms
                        string DisplayName = payeeregistration.Name.ToString();
                        string EmailID = payeeregistration.EmailId.ToString();
                        string Id = payeeregistration.Id.ToString().Unidecode();
                        string LoginName = payeeregistration.Name.ToString();
                        string contactno = payeeregistration.MobileNumber.ToString();

                        Uri uri = new Uri("http://localhost:1011/DamagePayeeRegistration");
                        string Action = "Dear " + LoginName + ",  You are succesfully registered with DDA Portal. For verify your email click  below link :-  " + uri;
                        var aburl = Request.GetDisplayUrl().Replace("Create", "RegistrationConfirmed");
                        string url = "https://www.google.com/search?q=yahoo+mail&rlz=1C1CHBF_enIN923IN923&oq=&aqs=chrome.1.69i59i450l5.24765349j0j15&sourceid=chrome&ie=UTF-8#=" + contactno + "&message= " + Action + " Thank you .&priority=11";

                        string link = "<a target=\"_blank\" href=\"" + aburl + "?" + encryptionHelper.EncryptString(AesKey, "EmailID") + "=" + encryptionHelper.EncryptString(AesKey, EmailID) + "&" + encryptionHelper.EncryptString(AesKey, "Id") + "=" + encryptionHelper.EncryptString(AesKey, Id) + "\">Click Here</a>";
                        string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "MailDetails.html");
                        string linkhrefregistration = aburl + "?" + encryptionHelper.EncryptString(AesKey, "EmailID") + "=" + encryptionHelper.EncryptString(AesKey, EmailID) + "&" + encryptionHelper.EncryptString(AesKey, "Id") + "=" + encryptionHelper.EncryptString(AesKey, Id);
                        #region Mail Generation Added By Renu

                        MailSMSHelper mailG = new MailSMSHelper();

                        #region HTML Body Generation
                        RegisterationBodyDTO bodyDTO = new RegisterationBodyDTO();
                        bodyDTO.displayName = DisplayName;
                        bodyDTO.loginName = LoginName;
                        bodyDTO.password = "";
                        bodyDTO.link = linkhrefregistration;
                        bodyDTO.action = Action;
                        bodyDTO.path = path;
                        string strBodyMsg = mailG.PopulateBody(bodyDTO);
                        #endregion
                                               
                       // var sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, EmailID, strMailCC, strMailBCC, strAttachPath);
                        #region Common Mail Genration
                        SentMailGenerationDto maildto = new SentMailGenerationDto();
                        maildto.strMailSubject = "Email Verification";
                        maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
                        maildto.strBodyMsg = strBodyMsg;
                        maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
                        maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
                        maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
                        maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
                        maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

                        maildto.strMailTo = EmailID;
                        var sendMailResult = mailG.SendMailWithAttachment(maildto);
                        #endregion
                        #endregion

                        if (sendMailResult)
                            TempData["Message"] = Alert.Show("Dear User,<br/>" + LoginName + " Login Details send on your Registered email and Mobile No, Please check and Login with details", "", AlertType.Success);
                        else
                            TempData["Message"] = Alert.Show("Dear User,<br/>" + LoginName + " Enable to send mail or sms ", "", AlertType.Info);

                        // return RedirectToAction("Create", "DamagePayeeRegistration");
                        return RedirectToAction("MailSentMsg", "DamagePayeeRegistration", new { username = payeeregistration.Name });

                        
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(payeeregistration);

                    }
                }
                else
                {
                   // ViewBag.Message = Alert.Show("Invalid Captcha.", "", AlertType.Warning);
                    return View(payeeregistration);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(payeeregistration);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrationConfirmed()
        {
            try
            {
                EncryptionHelper encryptionHelper = new EncryptionHelper();
                var AesKey = _configuration.GetSection("EncryptionKey").Value.ToString();
                Payeeregistration payeeregistration = new Payeeregistration();
                var EncryptedEmailId = HttpContext.Request.Query[encryptionHelper.EncryptString(AesKey, "EmailID")];
                var EncryptedId = HttpContext.Request.Query[encryptionHelper.EncryptString(AesKey, "Id")];
                var UniqueId = Convert.ToInt32(encryptionHelper.DecryptString(AesKey, Convert.ToString(EncryptedId)).Replace(" ", "+"));
                var EmailId = encryptionHelper.DecryptString(AesKey, EncryptedEmailId).Replace(" ", "+");
                var id = Request.QueryString.Value;

                if (id == null)
                {
                    id = "0";
                }
                if (ModelState.IsValid)
                {
                    payeeregistration.IsVerified = "T";
                    var result = await _damagePayeeRegistrationService.FetchSingleResult(UniqueId);
                    if (result != null)
                    {
                        payeeregistration.Id = UniqueId;
                        var result1 = await _damagePayeeRegistrationService.UpdateVerification(payeeregistration);
                        // aspnetuser table entry 
                        if (result1)
                        {

                            AddUserDto model = new AddUserDto()
                            {
                                Name = result.Name,
                                UserName = result.Name,
                                Email = result.EmailId,
                                PhoneNumber = result.MobileNumber,
                                Password = "Payee@123",
                                ConfirmPassword = "Payee@123",
                                RoleId = 20

                            };

                            var result2 = await _userProfileService.CreateUser(model);
                            if (result2)//send login credientials mail
                            {
                                // EncryptionHelper encryptionHelper = new EncryptionHelper();
                                //  ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                                //At successfull completion send mail and sms
                                string DisplayName = result.Name.ToString();
                                string EmailID = result.EmailId.ToString();
                                string Id = result.Id.ToString().Unidecode();
                                string LoginName = result.Name.ToString();
                                string contactno = result.MobileNumber.ToString();

                                Uri uri = new Uri("http://localhost:1011/DamagePayeeRegistration");
                                string Action = "Dear " + LoginName + ",  You are succesfully registered with DDA Portal. For verify your email click  below link :-  " + uri;
                                var aburl = Request.GetDisplayUrl().Replace("Create", "RegistrationConfirmed");
                                string url = "https://www.google.com/search?q=yahoo+mail&rlz=1C1CHBF_enIN923IN923&oq=&aqs=chrome.1.69i59i450l5.24765349j0j15&sourceid=chrome&ie=UTF-8#=" + contactno + "&message= " + Action + " Thank you .&priority=11";
                                //string link = "Username="+ result.Name + "and Password="+ model.Password;
                                string link = "https://damagepayee.managemybusinessess.com/";
                                // string link = "<a target=\"_blank\" href=\"" + aburl + "?" + encryptionHelper.EncryptString(AesKey, "EmailID") + "=" + encryptionHelper.EncryptString(AesKey, EmailID) + "&" + encryptionHelper.EncryptString(AesKey, "Id") + "=" + encryptionHelper.EncryptString(AesKey, Id) + "\">Click Here</a>";
                                string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "LoginInfoMailDetails.html");
                                // string linkhrefregistration = aburl + "?" + encryptionHelper.EncryptString(AesKey, "EmailID") + "=" + encryptionHelper.EncryptString(AesKey, EmailID) + "&" + encryptionHelper.EncryptString(AesKey, "Id") + "=" + encryptionHelper.EncryptString(AesKey, Id);
                                #region Mail Generation Added By Renu

                                MailSMSHelper mailG = new MailSMSHelper();

                                #region HTML Body Generation
                                RegisterationBodyDTO bodyDTO = new RegisterationBodyDTO();
                                bodyDTO.displayName = DisplayName;
                                bodyDTO.loginName = LoginName;
                                bodyDTO.password = "Payee@123";
                                bodyDTO.link = link;
                                // bodyDTO.link = "";
                                bodyDTO.action = Action;
                                bodyDTO.path = path;
                                string strBodyMsg = mailG.PopulateBody(bodyDTO);
                                #endregion

                                // var sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, EmailID, strMailCC, strMailBCC, strAttachPath);
                                #region Common Mail Genration
                                SentMailGenerationDto maildto = new SentMailGenerationDto();
                                maildto.strMailSubject = "User Login Details ";
                                maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
                                maildto.strBodyMsg = strBodyMsg;
                                maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
                                maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
                                maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
                                maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
                                maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

                                maildto.strMailTo = EmailID;
                                var sendMailResult = mailG.SendMailWithAttachment(maildto);
                                #endregion
                                #endregion

                                return RedirectToAction("logincredientialsMailSentMsg", "DamagePayeeRegistration", new { username = result.Name });

                            }
                            else
                            {
                                var result3 = await _damagePayeeRegistrationService.Delete1(UniqueId);
                                var Message1 = "Some problem occurred while registering,kindly register again";
                                return RedirectToAction("ExceptionView", "DamagePayeeRegistration", new { Message = Message1 });
                            }
                        }
                        //return View("RegistrationConfirmed");
                        //return RedirectToAction("logincredientialsMailSentMsg", "DamagePayeeRegistration", new { username = result.Name });
                        else
                        {
                           var Message2 = "Some problem occurred while verifying email, kindly try to register again.";
                            return RedirectToAction("ExceptionView", "DamagePayeeRegistration", new { Message = Message2 });
                           // return View("Create", "DamagePayeeRegistration");
                        }

                    }
                    else
                    {
                        var Message3 = "Some problem occurred while verifying email,Kindly register again";
                         return RedirectToAction("ExceptionView", "DamagePayeeRegistration", new { Message = Message3 });
                        
                    }


                }
                else
                {
                    var Message4 = "Some problem occurred while verifying email, kindly try to register again.";
                    return RedirectToAction("ExceptionView", "DamagePayeeRegistration", new { Message = Message4 });
                }
            }
            catch (Exception ex)
            {
                EncryptionHelper encryptionHelper = new EncryptionHelper();
                var AesKey = _configuration.GetSection("EncryptionKey").Value.ToString();
                var EncryptedId = HttpContext.Request.Query[encryptionHelper.EncryptString(AesKey, "Id")];
                var UniqueId = Convert.ToInt32(encryptionHelper.DecryptString(AesKey, Convert.ToString(EncryptedId)).Replace(" ", "+"));

                var result = await _damagePayeeRegistrationService.Delete1(UniqueId);
                var Message5 = "Some problem occurred while verifying email, kindly try to register again.";
                return RedirectToAction("ExceptionView", "DamagePayeeRegistration", new { Message = Message5 });
            }
        }


        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage(Payeeregistration payeeregistration)
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");

        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _damagePayeeRegistrationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Payeeregistration payeeregistration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _damagePayeeRegistrationService.Update(id, payeeregistration);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(payeeregistration);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(payeeregistration);

                }
            }
            return View(payeeregistration);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _damagePayeeRegistrationService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                return View("Index", result1);
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistName(int Id, string Name)
        {
            var result = await _damagePayeeRegistrationService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                
                var result1 = await _userProfileService.ValidateUniqueUserName1(Name);
                if (result1 == false)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"This Username is already Linked with Another Account. Kindly use different Username");
                }
            }
            else
            {
                return Json($"Name: {Name} already exist");
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Existemail(int Id, string EmailId)
        {
            var result = await _damagePayeeRegistrationService.CheckUniqueemail(Id, EmailId);
            if (result == false)
            {
                var result1 = await _userProfileService.ValidateUniqueEmail1(EmailId);
                if (result1 == false)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"This Email is already Linked with Another Account. Kindly use different Email");
                }
                //return Json(true);
            }
            else
            {
                return Json($"Email Id : {EmailId} already exist");
            }

           
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistPhoneNumber(int Id, string MobileNumber)
        {
            var result = await _damagePayeeRegistrationService.CheckUniquephone(Id, MobileNumber);
            if (result == false)
            {
                var result1 = await _userProfileService.ValidateUniquePhone1(MobileNumber);
                if (result1 == false)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"This Mobile Number is already Linked with Another Account. Kindly use different Mobile Number");
                }
                
            }
            else
            {
                return Json($"This Mobile No already exist");
            }


        }
      

        public async Task<IActionResult> View(int id)
        {
            var Data = await _damagePayeeRegistrationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public IActionResult MailSentMsg(string username)
        {
            ViewBag.name = username;
            return View();
        }
        public IActionResult logincredientialsMailSentMsg(string username)
        {
            ViewBag.name = username;
            return View();
        }
        public IActionResult ExceptionView(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
    }
}
