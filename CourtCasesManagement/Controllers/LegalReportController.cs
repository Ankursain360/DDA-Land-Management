using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace CourtCasesManagement.Controllers
{
    public class LegalReportController : BaseController
    {
        private readonly ILegalmanagementsystemservice _legalmanagementsystemservice;
        public LegalReportController(ILegalmanagementsystemservice legalmanagementsystemservice)
        {
            _legalmanagementsystemservice = legalmanagementsystemservice;
        }

        async Task BindDropDownView(Legalmanagementsystem legalmanagementsystem)
        {
            legalmanagementsystem.ZoneList = await _legalmanagementsystemservice.GetZoneList();
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> Index()
        //{
        //    Noticetodamagepayee model = new Noticetodamagepayee();

        //    model.FileNoList = await _noticeToDamagePayeeService.GetFileNoList();
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<PartialViewResult> GetDetails([FromBody] NoticeGenerationReportSearchDto notice)
        //{
        //    var result = await _noticeToDamagePayeeService.GetPagedNoticeGenerationReport(notice);
        //    if (result != null)
        //    {
        //        return PartialView("_List", result);
        //    }
        //    else
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        return PartialView();
        //    }
        //}

        // Dropdown Dependency  calls below
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _legalmanagementsystemservice.GetLocalityList(Convert.ToInt32(zoneId)));
        }
    }
}
