using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EncroachmentDemolition.Controllers
{
    public class EncroachmentRegisterController : BaseController
    {
        public IConfiguration _configuration;
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
        public EncroachmentRegisterController(IEncroachmentRegisterationService encroachmentRegisterationService, IConfiguration configuration)
        {
            _encroachmentRegisterationService = encroachmentRegisterationService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            EncroachmentRegisteration encroachmentRegisterations = new EncroachmentRegisteration();
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);
            return View(encroachmentRegisterations);
        }
        [HttpPost]
        public async Task<IActionResult> Create(EncroachmentRegisteration encroachmentRegisterations)
        {
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);
            return View(encroachmentRegisterations);
        }
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _encroachmentRegisterationService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _encroachmentRegisterationService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _encroachmentRegisterationService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }
    }
}
