using System;
using System.Threading.Tasks;
using Dto.Search;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authorization;
using Utility.Helper;
using Microsoft.Extensions.Configuration;
using System.Text.Encodings.Web;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Dto.Master;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using IdentityServerAspNetIdentity.Models;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using System.Text.RegularExpressions;

namespace IdentityServerHost.Quickstart
{
    public class ForgetPasswordController : Controller
    {
        public string Email { get; set; }
        private readonly IHostingEnvironment _hostingEnvironment;
        public readonly IConfiguration _configuration;
        private readonly IPasswordhistoryService _passwordhistoryService;
        private readonly UserManager<IdentityServerAspNetIdentity.Models.ApplicationUser> _userManager;

        public ForgetPasswordController(IConfiguration configuration, IHostingEnvironment en,
             IPasswordhistoryService passwordhistoryService,
            UserManager<IdentityServerAspNetIdentity.Models.ApplicationUser> userManager)
        {
            _passwordhistoryService = passwordhistoryService;
            _userManager = userManager;
            _configuration = configuration;
            _hostingEnvironment = en;
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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ForgetPasswordMailDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //    // Validate Captcha Code
                    if (!Captcha.ValidateCaptchaCode(model.CaptchaCode, HttpContext))
                    {
                        //ViewBag.Message = Alert.Show("Invalid Catacha.", "", AlertType.Error);
                        ModelState.AddModelError("CaptchaCode", "Invalid Captcha.");
                        return View(model);
                    }


                    var user = await _userManager.FindByNameAsync(model.Username);
                    if (user == null)
                    {
                        // ViewBag.Message = Alert.Show("Unable to load user " + model.Username + ".", "", AlertType.Warning);
                        ModelState.AddModelError("Username", "Unable to load user " + model.Username + ".");
                        return View(model);
                    }

                    Email = model.Username;
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callback = Url.Action(nameof(ResetPassword), "ForgetPassword", new { token, username = user.UserName }, Request.Scheme);

                    if (user != null)
                    {
                        //At successfull completion send mail and sms
                        string DisplayName = model.Username.ToString();
                        string EmailID = user.Email.ToString();
                        string maskedemail = MaskEmail(EmailID);
                        string Id = "";
                        string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "MailDetails.html");

                        #region Mail Generation Added By Renu

                        MailSMSHelper mailG = new MailSMSHelper();

                        #region HTML Body Generation
                        RegisterationBodyDTO bodyDTO = new RegisterationBodyDTO();
                        bodyDTO.displayName = DisplayName;
                        bodyDTO.loginName = DisplayName;
                        bodyDTO.password = "";
                        bodyDTO.link = callback;
                        bodyDTO.action = "";
                        bodyDTO.path = path;
                        string strBodyMsg = mailG.PopulateBody(bodyDTO);
                        #endregion

                        #region Common Mail Genration
                        SentMailGenerationDto maildto = new SentMailGenerationDto();
                        maildto.strMailSubject = "Reset Password";
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
                            TempData["Message"] = Alert.Show("Dear User,<br/>" + DisplayName + " a link for password reset sent on your registered email and mobile no., Please check!", "", AlertType.Success);
                        else
                            TempData["Message"] = Alert.Show("Dear User,<br/>" + DisplayName + " Enable to send mail or sms ", "", AlertType.Info);

                        // return RedirectToAction("Create", "ForgetPassword");
                        HttpContext.Session.SetString("username", DisplayName);
                        HttpContext.Session.SetString("emailId", maskedemail);
                        return RedirectToAction("ForgetPasswordMail", "ForgetPassword");

                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(model);

                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string username)
        {
            var model = new ResetPasswordDto { Token = token, Username = username };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //    // Validate Captcha Code
                    if (!Captcha.ValidateCaptchaCode(resetPasswordDto.CaptchaCode, HttpContext))
                    {
                        ModelState.AddModelError("CaptchaCode", "Invalid Captcha.");
                        return View(resetPasswordDto);
                    }
                    var user = await _userManager.FindByNameAsync(resetPasswordDto.Username);
                    if (user == null)
                    {
                        ModelState.AddModelError("Username", "No user found with username" + resetPasswordDto.Username + ".");
                        return View(resetPasswordDto);
                    }
                    //chk previous  passwords 

                    var encodedpassword = (new EncryptionHelper()).Base64Encode(resetPasswordDto.Password);
                    var result1 = await _passwordhistoryService.IsPreviousPassword(user.Id, encodedpassword);
                    if (result1)
                    {
                        ModelState.AddModelError("Password", "You Cannot Reuse Previous 5 Password");
                        return View(resetPasswordDto);
                    }

                    var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                    if (!resetPassResult.Succeeded)
                    {
                        foreach (var error in resetPassResult.Errors)
                        {
                            ModelState.TryAddModelError(error.Code, error.Description);
                        }
                        return View(resetPasswordDto);
                    }
                    //insert into password history table

                    Passwordhistory history = new Passwordhistory();

                    history.UserId = user.Id;
                    history.UserName = user.UserName;
                    history.Password = encodedpassword;
                    history.CreatedBy = user.Id;
                    history.IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                    var result = await _passwordhistoryService.Create(history);

                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }
                else
                {
                    ViewBag.Message = Alert.Show("Please Enter fields correctly", "", AlertType.Error);
                    return View(resetPasswordDto);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(resetPasswordDto);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation(string token, string username)
        {
            var model = new ResetPasswordDto { Token = token, Username = username };
            return View(model);
        }

        public IActionResult ForgetPasswordMail()
        {
            return View();
        }

        public string MaskEmail(string s)
        {
            string _PATTERN = @"(?<=[\w]{1})[\w-\._\+%\\]*(?=[\w]{1}@)|(?<=@[\w]{1})[\w-_\+%]*(?=\.)";
            if (!s.Contains("@"))
                return new String('*', s.Length);
            if (s.Split('@')[0].Length < 4)
                return @"*@*.*";
            return Regex.Replace(s, _PATTERN, m => new string('*', m.Length));
        }
    }
}

