using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
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
            return null;
        }
        public async Task<JsonResult> GetAllSectorList(int? location)
        {
            return null;
        }
        public async Task<JsonResult> GetSectorList(int? location)
        {
            return null;
        }
        public async Task<JsonResult> GetAllPlotList(int? location)
        {
            return null;
        }
        //public async Task<JsonResult> GetAllPlotList(int? location)
        //{
        //    return null;
        //}
        public async Task<JsonResult> GetZoneList(int? location)
        {
            return null;
        }
        public async Task<JsonResult> GetVillageCompanywise(int? location)
        {
            return null;
        }
        public async Task<JsonResult> GetAllZoneList(int? location)
        {
            return null;
        }
       // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
