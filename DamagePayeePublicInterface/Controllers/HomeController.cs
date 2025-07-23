using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using DamagePayeePublicInterface.Models;
using System.Diagnostics;
using DamagePayeePublicInterface.Helper;
using System.Threading.Tasks;
using Dto.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using Libraries.Service.IApplicationService;
using Core.Enum;
using DamagePayeePublicInterface.Filters;
using Utility.Helper;
using Microsoft.Extensions.Configuration;
using Notification.OptionEnums;
using Libraries.Model.Entity;
using Notification;
using Notification.Constants;

namespace DamagePayeePublicInterface.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationModificationDetailsService _modificationDetails;
        private readonly IConfiguration _configuration;
        private readonly IFeedbackService _feedbackService;

        public HomeController(ISiteContext siteContext,
           IUserProfileService userProfileService, IHttpContextAccessor httpContextAccessor, IApplicationModificationDetailsService modificationDetails,IConfiguration configuration,IFeedbackService feedbackService)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _httpContextAccessor = httpContextAccessor;
            _modificationDetails = modificationDetails;
            _configuration = configuration;
            _feedbackService = feedbackService;
        }
        public void updateDateFun()
        {
            var updatedDate = _modificationDetails.GetApplicationModificationDetails();
            var dt = Convert.ToDateTime(updatedDate).ToString("dd/MMM/yyyy HH:MM:ss tt");
            if (updatedDate != null)
            {
                //TempData["updatedDate"] = dt;
                HttpContext.Session.SetString("LastUpdatedDate", dt);
            }
            else
            {
                TempData["updatedDate"] = "No Data Available";

            }

        }
        [ActionName("Index1")]
        public async Task<IActionResult> Index()
        {
            UserProfileDto user = await _userProfileService.GetUserById(_siteContext.UserId);
            updateDateFun();
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Response.Clear();
            //Clear cookies
            var cookies = _httpContextAccessor.HttpContext.Request.Cookies;
            foreach (var cookie in cookies)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie.Key);
            }
            return SignOut("Cookies", "oidc");
        }

        public IActionResult ErrorLog()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult copyRight()
        {
            updateDateFun();
            return View();
        }
        public IActionResult PrivacyPolicy()
        {
            updateDateFun();
            return View();
        }
        public IActionResult HyperlinkPolicy()
        {
            updateDateFun();
            return View();
        }
        public IActionResult WebInformationManager()
        {
            updateDateFun();
            return View();
        }
        public IActionResult TermsandConditions()
        {
            updateDateFun();
            return View();
        }
        public IActionResult Help()
        {
            updateDateFun();
            return View();
        }
        public IActionResult Contactus()
        {
            updateDateFun();
            return View();
        }
        public IActionResult AboutUs()
        {
            updateDateFun();
            return View();
        }
        public IActionResult Disclaimer()
        {
            updateDateFun();
            return View();
        }
        public IActionResult ContingencyManagementPlan()
        {
            updateDateFun();
            return View();
        }

        [AuthorizeContext(ViewAction.Download)]
        public IActionResult Usermanual()
        {
            FileHelper file = new FileHelper();
            string FilePath = _configuration.GetSection("FilePaths:Docs:UsermanualPath").Value.ToString();
            string path = FilePath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }
        public IActionResult ContentArchivalCAPPolicy()
        {
            updateDateFun();
            return View();
        } 
        public IActionResult ContentReviewCRPPolicy()
        {
            updateDateFun();
            return View();
        }
        public IActionResult ContentReviewPolicy()
        {
            updateDateFun();
            return View();
        }
        public IActionResult WebsiteMonitoringPlan()
        {
            updateDateFun();
            return View();
        }
        public IActionResult SecurityPolicy()
        {
            updateDateFun();
            return View();
        }
        public IActionResult Sitemap()
        {
            updateDateFun();
            return View();
        }
        public IActionResult Feedback()
        {
            updateDateFun();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Feedback(tblfeedback tblfeedback)
        {

            if (ModelState.IsValid)
            {
                var result = await _feedbackService.Create(tblfeedback);

                if (result == true)
                {
                    TempData["Message"] = AlertMessage.ShowMessage("Feedback Send successfully", "Sucess", AlertType.Success);
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    updateDateFun();
                    return View(tblfeedback);

                }
            }
            else
            {
                updateDateFun();
                return View(tblfeedback);
            }

        }
        public static class AlertMessage
        {
            public static string ShowMessage(string message, string title, AlertType type)
            {
                return $"<div class='alert alert-{type.ToString().ToLower()} alert-dismissible fade show' role='alert'>" +
                       $"{message}<button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
            }
        }
        public IActionResult CMAP()
        {
            updateDateFun();
            return View();
        }
    }
}
