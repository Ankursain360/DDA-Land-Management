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
        public async Task<JsonResult> GetVillageDetails(int VillageId, int ZoneId)
        {
            return Json(await _GISService.GetVillageDetails(VillageId,ZoneId));
        }
        public async Task<JsonResult> GetZoneList()
        {
            return Json(await _GISService.GetZoneList());
        }
        public async Task<JsonResult> GetAbadiDetails(int VillageId)
        {
            return Json(await _GISService.GetAbadiDetails(VillageId));
        }
        public async Task<JsonResult> GetBurjiDetails(int VillageId)
        {
            return Json(await _GISService.GetBurjiDetails(VillageId));
        }
        public async Task<JsonResult> GetCleanDetails(int VillageId)
        {
            return Json(await _GISService.GetCleanDetails(VillageId));
        }
        public async Task<JsonResult> GetCleantextDetails(int VillageId)
        {
            return Json(await _GISService.GetCleantextDetails(VillageId));
        }
        public async Task<JsonResult> GetDimDetails(int VillageId)
        {
            return Json(await _GISService.GetDimDetails(VillageId));
        }
        public async Task<JsonResult> GetEncroachmentDetails(int VillageId)
        {
            return Json(await _GISService.GetEncroachmentDetails(VillageId));
        }
        public async Task<JsonResult> GetGoshaDetails(int VillageId)
        {
            return Json(await _GISService.GetGoshaDetails(VillageId));
        }
        public async Task<JsonResult> GetGridDetails(int VillageId)
        {
            return Json(await _GISService.GetGridDetails(VillageId));
        }
        public async Task<JsonResult> GetNalaDetails(int VillageId)
        {
            return Json(await _GISService.GetNalaDetails(VillageId));
        }
        public async Task<JsonResult> GetTextDetails(int VillageId)
        {
            return Json(await _GISService.GetTextDetails(VillageId));
        }
        public async Task<JsonResult> GetTriJunctionDetails(int VillageId)
        {
            return Json(await _GISService.GetTriJunctionDetails(VillageId));
        }
    }
}
