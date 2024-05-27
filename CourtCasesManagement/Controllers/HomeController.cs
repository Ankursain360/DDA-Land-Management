
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using CourtCasesManagement.Models;
using System.Diagnostics;
using CourtCasesManagement.Helper;
using System.Threading.Tasks;
using Dto.Master;
using CourtCasesManagement.Controllers;
using Microsoft.AspNetCore.Http;
using System;
using Libraries.Service.IApplicationService;

namespace CourtCasesManagement.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationModificationDetailsService _modificationDetails;
        public HomeController(ISiteContext siteContext,
           IUserProfileService userProfileService, IHttpContextAccessor httpContextAccessor, IApplicationModificationDetailsService modificationDetails)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _httpContextAccessor = httpContextAccessor;
            _modificationDetails = modificationDetails;
        }
        public void updateDateFun()
        {
            var updatedDate = _modificationDetails.GetApplicationModificationDetails();
            var dt = Convert.ToDateTime(updatedDate).ToString("dd/MMM/yyyy HH:MM:ss tt");
            if (updatedDate != null)
            {
                TempData["updatedDate"] = dt;

            }
            else
            {
                TempData["updatedDate"] = "No Data Available";

            }

        }
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

        public IActionResult UnAuthorized()
        {
            return View();
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
    }
}
