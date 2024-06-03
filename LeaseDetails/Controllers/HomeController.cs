using Dto.Master;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using LeaseDetails.Helper;
using LeaseDetails.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Libraries.Service.IApplicationService;
using System;

namespace LeaseDetails.Controllers
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

        [HttpGet]
        public async Task<JsonResult> KycApplicationDetails(int? Id)
        {
            Id = Id ?? 0;
            var data = await _userProfileService.KycApplicationDetails(_siteContext.UserId);
            return Json(data.Select(x => new
            {
                x.KycApplicaionPending,
                x.KycApplicaionApprove,               
                x.KycApplicationDeficiency,
                x.KycApplicationInRejected,
                x.KycApplicationInTotal,

            }));
        }

        [HttpGet]
        public async Task<JsonResult> KycDemandPaymentDetails(int? Id)
        {
            Id = Id ?? 0;
            var data = await _userProfileService.KycDemandPaymentDetails(_siteContext.UserId);
            return Json(data.Select(x => new
            {
                x.KycDemandPaymentPending,
                x.KycDemandPaymentApprove,              
                x.KycDemandPaymentInDeficiency,
                x.KycDemandPaymentRejected,
                x.KycDemandPaymenTotal
            }));
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
