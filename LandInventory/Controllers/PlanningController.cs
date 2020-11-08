using System;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LandInventory.Controllers
{
    public class PlanningController : Controller
    {

        public IConfiguration _configuration;
        public readonly IPlanningService _planningService;
        public PlanningController(IPlanningService planningService, IConfiguration configuration)
        {
            _planningService = planningService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create(Planning planning)
        {
            planning.DepartmentList = await _planningService.GetAllDepartment();
            planning.ZoneList = await _planningService.GetAllZone(planning.DepartmentId);
            planning.DivisionList = await _planningService.GetAllDivision(planning.ZoneId);
            return View(planning);
        }
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            var zoneList = await _planningService.GetAllZone(Convert.ToInt32(DepartmentId));
            return Json(zoneList);
        }
        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _planningService.GetAllDivision(Convert.ToInt32(ZoneId)));
        }
    }
}