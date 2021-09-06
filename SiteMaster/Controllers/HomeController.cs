using Dto.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using SiteMaster.Helper;
using SiteMaster.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SiteMaster.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ISiteContext siteContext,
           IUserProfileService userProfileService, IHttpContextAccessor httpContextAccessor)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            UserProfileDto user = await _userProfileService.GetUserById(_siteContext.UserId);
            return View(user);
        }


        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear(); 
            //Clear cookies
            var cookies = _httpContextAccessor.HttpContext.Request.Cookies;
            foreach (var cookie in cookies)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie.Key);
            }
            var myCookies = Request.Cookies.Keys;
            foreach (string cookie in myCookies)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie, new CookieOptions()
                {
                    Domain = "auth.managemybusinessess.com"
                });
            }
            //
            return SignOut("Cookies", "oidc");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UnAuthorized() {
            return View();
        }

        public IActionResult ExceptionLog()
        {
            return View();
        }
    }
}
