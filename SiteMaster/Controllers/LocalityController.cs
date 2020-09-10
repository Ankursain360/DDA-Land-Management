using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SiteMaster.Controllers
{
    public class LocalityController : Controller
    {
        public readonly ILocalityService _localityService;
        public LocalityController(ILocalityService localityService)
        {
            _localityService = localityService;
        }
        public async Task<IActionResult> Index()
        {
            List<Locality> list = await _localityService.GetAllLocality();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            Locality model = new Locality();
            model.IsActive = 1;
            model.DepartmentList = await _localityService.GetAllDepartment();
            model.ZoneList = await _localityService.GetAllZone(model.DepartmentId);
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> GetZoneList(int?DepartmentId)
        {
            DepartmentId= DepartmentId ?? 0;
            return Json(await _localityService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }
    }
}