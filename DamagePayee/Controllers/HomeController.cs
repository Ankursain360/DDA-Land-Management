using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using DamagePayee.Models;
using System.Diagnostics;
using DamagePayee.Helper;
using System.Threading.Tasks;
using Dto.Master;
using Microsoft.AspNetCore.Http;

namespace DamagePayee.Controllers
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
    }
}
