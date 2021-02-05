using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GIS.Controllers
{
    public class GISController : BaseController
    {
        private readonly IGISService _GISService;

        public GISController(IGISService GISService)
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
        public async Task<JsonResult> GetZoneDetails(int ZoneId)
        {
            return Json(await _GISService.GetZoneDetails(ZoneId));
        }
        public async Task<JsonResult> GetZoneList()
        {
            return Json(await _GISService.GetZoneList());
        }

    }
}
