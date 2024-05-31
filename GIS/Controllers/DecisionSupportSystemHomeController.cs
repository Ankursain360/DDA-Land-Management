using Dto.Master;
using GIS.Helper;
using GIS.Models;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using System.Diagnostics;
using System.Threading.Tasks;
using Core.Enum;
using GIS.Filters;
using System;

namespace GIS.Controllers
{
    public class DecisionSupportSystemHomeController : Controller
    {
        private readonly IGISService _GISService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IApplicationModificationDetailsService _modificationDetails;
        public DecisionSupportSystemHomeController(IGISService GISService, IHttpContextAccessor httpContextAccessor, IUserProfileService userProfileService, ISiteContext siteContext, IApplicationModificationDetailsService modificationDetails)
        {
            _GISService = GISService;
            _httpContextAccessor = httpContextAccessor;
            _siteContext = siteContext;
            _userProfileService = userProfileService;
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
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            UserProfileDto user = await _userProfileService.GetUserById(_siteContext.UserId);
            updateDateFun();
            return View(user);
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
    }
}
