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

namespace LIMSPublicInterface.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IApplicationModificationDetailsService _modificationDetails;
        public HomeController(IApplicationModificationDetailsService modificationDetails)
        {
            _modificationDetails = modificationDetails;
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
    }
}
