using System;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
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
using Service.IApplicationService;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace SiteMaster.Controllers
{
    public class ForgetPasswordController : BaseController
    {
        public string Email { get; set; }
        private readonly IHostingEnvironment _hostingEnvironment;
        public readonly IConfiguration _configuration;
        private readonly IUserProfileService _userProfileService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ForgetPasswordController(IConfiguration configuration, IHostingEnvironment en
            , IUserProfileService userProfileService, UserManager<ApplicationUser> userManager)
        {
            _userProfileService = userProfileService;
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
                        ViewBag.Message = Alert.Show("Invalid Catacha.", "", AlertType.Error);
                        return View(model);
                    }
                    var user = await _userManager.FindByNameAsync(model.Username);
                    if (user == null)
                    {
                        ViewBag.Message = Alert.Show("Unable to load user " + model.Username + ".", "", AlertType.Warning);
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

                        string strMailSubject = "Reset Password";
                        string strMailCC = "", strMailBCC = "", strAttachPath = "";
                        var sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, EmailID, strMailCC, strMailBCC, strAttachPath);
                        #endregion

                        if (sendMailResult)
                            TempData["Message"] = Alert.Show("Dear User,<br/>" + DisplayName + " a link for password reset sent on your registered email and mobile no., Please check!", "", AlertType.Success);
                        else
                            TempData["Message"] = Alert.Show("Dear User,<br/>" + DisplayName + " Enable to send mail or sms ", "", AlertType.Info);

                        return RedirectToAction("Create", "ForgetPassword");
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
                        ViewBag.Message = Alert.Show("Invalid Catacha.", "", AlertType.Error);
                        return View(resetPasswordDto);
                    }
                    var user = await _userManager.FindByNameAsync(resetPasswordDto.Username);
                    if (user == null)
                    {
                        ViewBag.Message = Alert.Show("No Authenticated User", "", AlertType.Error);
                        return View(resetPasswordDto);
                    }
                    var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                    if (!resetPassResult.Succeeded)
                    {
                        foreach (var error in resetPassResult.Errors)
                        {
                            ModelState.TryAddModelError(error.Code, error.Description);
                            ViewBag.Message = Alert.Show(error.Description, "", AlertType.Error);
                        }
                        return View(resetPasswordDto);
                    }
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

        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage(Payeeregistration payeeregistration)
        {
            int width = 170;
            int height = 75;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");

        }

    }
}

