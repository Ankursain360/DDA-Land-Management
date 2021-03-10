using GIS.Models;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GIS.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGISService _GISService;

        public HomeController(IGISService GISService)
        {
            _GISService = GISService;
        }
        public IActionResult Index()
        {
            return View();
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
    }
}
