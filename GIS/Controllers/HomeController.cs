using GIS.Models;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GIS.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGISService _GISService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IGISService GISService, IHttpContextAccessor httpContextAccessor)
        {
            _GISService = GISService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "GIS");
            //return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<JsonResult> GetVillageList(int? ZoneId)
        {
            return Json(await _GISService.GetVillageList(ZoneId ?? 0));
        }
        public async Task<JsonResult> GetPlotList(int? VillageId)
        {
            return Json(await _GISService.GetPlotList(VillageId ?? 0));
        }
        public async Task<JsonResult> GetZoneList()
        {
            return Json(await _GISService.GetZoneList());
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
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Response.Clear();
            //Clear cookies
            var cookies = _httpContextAccessor.HttpContext.Request.Cookies;
            foreach (var cookie in cookies)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie.Key);
            }
            //
            return SignOut("Cookies", "oidc");
        }
    }
}
