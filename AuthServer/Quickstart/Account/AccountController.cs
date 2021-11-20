// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AuthServer.Models;
using Dto.Master;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utility.Helper;

using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;

namespace IdentityServerHost.Quickstart.UI
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityServerAspNetIdentity.Models.ApplicationUser> _userManager;
        private readonly SignInManager<IdentityServerAspNetIdentity.Models.ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        public IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPasswordhistoryService _passwordhistoryService;
        public AccountController(
            UserManager<IdentityServerAspNetIdentity.Models.ApplicationUser> userManager,
            SignInManager<IdentityServerAspNetIdentity.Models.ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            IConfiguration configuration,
             IPasswordhistoryService passwordhistoryService,
            IHttpContextAccessor httpContextAccessor, IHostingEnvironment en)
           
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = en;
            _passwordhistoryService = passwordhistoryService;
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        
        {
            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync(returnUrl);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return RedirectToAction("Challenge", "External", new { scheme = vm.ExternalLoginScheme, returnUrl });
            }
            vm.Data = SetEncriptionKey();
            return View(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            // the user clicked the "cancel" button
            if (button != "login")
            {
                if (context != null)
                {
                    // if the user cancels, send a result back into IdentityServer as if they 
                    // denied the consent (even if this client does not require consent).
                    // this will send back an access denied OIDC error response to the client.
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    if (context.IsNativeClient())
                    {
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.LoadingPage("Redirect", model.ReturnUrl);
                    }

                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // since we don't have a valid context, then we just go back to the home page
                    return Redirect("~/");
                }
            }
            if (!Captcha.ValidateCaptchaCode(model.CaptchaCode, HttpContext))
            {
                ModelState.AddModelError("captcha", "Invalid Captcha.");
            }

            if (model.Data != "")
            {
                string key = model.Data;
                model.Password = DecryptStringAES(model.Password, key);
                model.Username = DecryptStringAES(model.Username, key);
            }
            else
            {
                ModelState.AddModelError("Decryption", "Decryption program failed.Kindly Referesh the page and try again.");
            }

            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);

                    if (user.ChangePasswordStatus == "T") 
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var callback = Url.Action(nameof(ResetPassword), "Account", new { token, username = user.UserName }, Request.Scheme);

                        //At successfull completion send mail and sms
                        string DisplayName = model.Username.ToString();
                        string EmailID = user.Email.ToString();
                       // string Id = "";
                        string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "MailDetails1.html");

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
                        {
                            return RedirectToAction("MailSentMsg", "Account", new { username = user.UserName });

                        }

                        
                    }

                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName, clientId: context?.Client.ClientId));
                    #region cookie based session fixation added by sachin bhatt

                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration.GetSection("CookiesSettings:CookiesTimeout").Value));
                    options.HttpOnly = true;
                    //options.Secure = true;
                     options.Domain = _configuration.GetSection("CookiesSettings:CookiesDomain").Value.ToString();                   
                    // options.Path = new PathString(_configuration.GetSection("CookiesSettings:CookiesPath").Value.ToString());

                    string _browserInfo = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString() + "~" + _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    string _sessionValue = user.Id.ToString() + "^" + DateTime.Now.Ticks + "^" + _browserInfo + "^" + System.Guid.NewGuid();
                    byte[] _encodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_sessionValue);
                    string _encryptedString = System.Convert.ToBase64String(_encodeAsBytes);
                    _httpContextAccessor.HttpContext.Response.Cookies.Append("AuthToken", _encryptedString, options);


                    #endregion
                    if (context != null)
                    {
                        if (context.IsNativeClient())
                        {
                            // The client is native, so this change in how to
                            // return the response is for better UX for the end user.
                            return this.LoadingPage("Redirect", model.ReturnUrl);
                        }

                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                        return Redirect(model.ReturnUrl);
                    }

                    // request for a local page
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        // user might have clicked on a malicious link - should be logged
                        throw new Exception("invalid return URL");
                    }
                }
                else if (result.IsLockedOut)
                {
                    //var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account", new { }, Request.Scheme);
                    //var content = string.Format("Your account is locked out, to reset your password, please click this link: {0}", forgotPassLink);
                    //var message = new Message(new string[] { userModel.Email }, "Locked out account information", content, null);
                    //await _emailSender.SendEmailAsync(message);
                    ModelState.AddModelError("", "The account is locked out");
                    var vms = await BuildLoginViewModelAsync(model);
                    return View(vms);
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }


        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await _signInManager.SignOutAsync();
                await _httpContextAccessor.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                await _httpContextAccessor.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                //await _httpContextAccessor.HttpContext.SignOutAsync("Cookies");
                //await _httpContextAccessor.HttpContext.SignOutAsync("oidc");
                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }
            // delete local authentication cookie
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("idserv.external");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("idserv.session");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("AuthToken");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("ASP.NET_SessionId");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("encryptedSession");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".AspNetCore.Session");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
           // _httpContextAccessor.HttpContext.Response.Cookies.Delete("Auth-cookie");
            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);

            }

            return View("LoggedOut", vm);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = string.Empty; //model.Username;
            vm.CaptchaCode = string.Empty;
            vm.RememberLogin = model.RememberLogin;
            vm.Data = SetEncriptionKey();
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }

        #region Security Audit Points added by sachin
        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 150;
            int height = 50;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");

        }
        private string SetEncriptionKey()
        {
            Random random = new Random();
            string combination = "0123456789ABCDEFGHIJKMNOPQRSTUVWXYZabcdefghijkmnopqrstuvxyz";
            StringBuilder captcha = new StringBuilder();
            for (int i = 0; i < 16; i++)
                captcha.Append(combination[random.Next(combination.Length)]);
            string key = captcha.ToString();
            HttpContext.Session.SetString("haskey", captcha.ToString());
            return key;
        }

        public static string DecryptStringAES(string cipherText, string keys)
        {
            var keybytes = Encoding.UTF8.GetBytes(keys);
            var iv = Encoding.UTF8.GetBytes(keys);

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }
        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }

        private string GenerateHashKey()
        {
            StringBuilder myStr = new StringBuilder();
            myStr.Append(Request.Headers["User-Agent"].ToString());
            myStr.Append(Guid.NewGuid().ToString());
            //myStr.Append(Request.Browser.MajorVersion);
            //myStr.Append(Request.Browser.MinorVersion);
            //myStr.Append(Request.LogonUserIdentity.User.Value);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashdata = sha.ComputeHash(Encoding.UTF8.GetBytes(myStr.ToString()));
            return Convert.ToBase64String(hashdata);
        }
        #endregion

        #region Reset password on first login ,code added by ishu 20/9/2021
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
                        //ViewBag.Message = Alert.Show("Invalid Catacha.", "", AlertType.Error);
                        return View(resetPasswordDto);
                    }
                    var user = await _userManager.FindByNameAsync(resetPasswordDto.Username);
                    if (user == null)
                    {
                        ModelState.AddModelError("Username", "No Authenticated User with username - " + resetPasswordDto.Username + ".");
                       // ViewBag.Message = Alert.Show("No Authenticated User", "", AlertType.Error);
                        return View(resetPasswordDto);
                    }
                    //chk previous  passwords 

                    var encodedpassword = (new EncryptionHelper()).Base64Encode(resetPasswordDto.Password);
                    var result1 = await _passwordhistoryService.IsPreviousPassword(user.Id, encodedpassword);
                    if (result1)
                    {
                        ModelState.AddModelError("Password", "You Cannot Reuse Previous 5 Passwords");
                        return View(resetPasswordDto);
                    }
                    var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                    if (!resetPassResult.Succeeded)
                    {
                        foreach (var error in resetPassResult.Errors)
                        {
                            ModelState.TryAddModelError(error.Code, error.Description);
                            //ViewBag.Message = Alert.Show(error.Description, "", AlertType.Error);
                            ModelState.AddModelError("", error.Description);
                        }
                         return View(resetPasswordDto);
                    }
                    user.ChangePasswordStatus = "F";
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    var res= result.Succeeded ? true : false;
                    #region
                    //insert into password history table

                    Passwordhistory history = new Passwordhistory();

                    history.UserId = user.Id;
                    history.UserName = user.UserName;

                    // history.Password = _userManager.PasswordHasher.HashPassword(user, resetPasswordDto.Password);
                    // history.Password = resetPasswordDto.Password;
                    history.Password = encodedpassword;
                    history.CreatedBy = user.Id;
                    history.IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                    var result2 = await _passwordhistoryService.Create(history);
                    #endregion
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

        public IActionResult MailSentMsg(string username)
        {
            ViewBag.name = username;
            return View();
        }
        #endregion
    }
}