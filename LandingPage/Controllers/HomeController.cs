using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LandingPage.Models;
using Libraries.Service.IApplicationService;
using LandingPage.Helper;
using Service.IApplicationService;
using Dto.Master;

namespace LandingPage.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;

        //private readonly ILogger<HomeController> _logger;
        public HomeController(ISiteContext siteContext,
          IUserProfileService userProfileService, IModuleService moduleService)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _moduleService = moduleService;
        }
        //public HomeController(ILogger<HomeController> logger, IModuleService moduleService)
        //{
        //    _logger = logger;
        //    _moduleService = moduleService;
        //}


        private readonly IModuleService _moduleService;


        public async Task<IActionResult> Index()
        {
           UserProfileDto user = await _userProfileService.GetUserById(_siteContext.UserId);

            var list = await _moduleService.GetAllModule();
            return View(list);
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
    }
}
