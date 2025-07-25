using Dto.Master;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using LIMSPublicInterface.Helper;
using LIMSPublicInterface.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Libraries.Service.IApplicationService;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Libraries.Model.Entity;
using Libraries.Service.ApplicationService;
using Notification.Constants;
using Notification.OptionEnums;
using Notification;

namespace LIMSPublicInterface.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IApplicationModificationDetailsService _modificationDetails;
        private readonly IConfiguration _configuration;
        private readonly IFeedbackService _feedbackService;

        public HomeController(IApplicationModificationDetailsService modificationDetails,IConfiguration configuration,IFeedbackService feedbackService)
        {
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
                // TempData["updatedDate"] = dt;
                HttpContext.Session.SetString("LastUpdatedDate", dt);
            }
            else
            {
                TempData["updatedDate"] = "No Data Available";

            }

        }
        public async Task<IActionResult> Index()
        {
            updateDateFun();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

        public IActionResult ExceptionLog()
        {
            return View();
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
        //[AuthorizeContext(ViewAction.Download)]
        //public IActionResult Usermanual()
        //{
        //    FileHelper file = new FileHelper();
        //    string FilePath = _configuration.GetSection("FilePaths:Docs:UsermanualPath").Value.ToString();
        //    string path = FilePath;
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(path);
        //    return File(fileBytes, file.GetContentType(path));
        //}
        public IActionResult ContentArchivalCAPPolicy()
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
                    TempData["Message"] = AlertMessage.ShowMessage("Your Feedback has been successfully submitted. The System Administrator will revert with a response within 2 working day. Thank you for reaching out.", "Sucess", AlertType.Success);
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
        public IActionResult CMAP()
        {
            updateDateFun();
            return View();
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
}
