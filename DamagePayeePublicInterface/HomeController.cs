using DamagePayeePublicInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GIS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<JsonResult> VacantPlot(int? location)
        {
            return null;
        }
        public async Task<JsonResult> GetVillageList(int? location)
        {
            return null;
        }
        public async Task<JsonResult> GetAllVillageList(int? location)
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
        public async Task<JsonResult> GetPlotList(int? location)
        {
            return null;
        }
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
