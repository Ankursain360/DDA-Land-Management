using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;

using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

using Utility;
using Utility.Helper;
using System.Net;
using LandInventory.Filters;
using Core.Enum;
using Dto.Master;

namespace LandInventory.Controllers
{
    public class UserWiseLandStatusController : BaseController
    {
        private readonly IUserWiseLandStatusReportService _UserWiseLandStatusReportService;

        public UserWiseLandStatusController(IUserWiseLandStatusReportService UserWiseLandStatusReportService)
        {
            _UserWiseLandStatusReportService = UserWiseLandStatusReportService;
        }



        // Dropdown Dependency  calls below
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _UserWiseLandStatusReportService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _UserWiseLandStatusReportService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
 


     

        //[AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Index()
        {
            Vacantlandimage model = new Vacantlandimage();
            model.DepartmentList = await _UserWiseLandStatusReportService.GetAllDepartment();
            model.ZoneList = await _UserWiseLandStatusReportService.GetAllZone(0);
            model.DivisionList = await _UserWiseLandStatusReportService.GetAllDivisionList(0);
        
            return View(model);
        }



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] UserWiseLandStatusReportSearchDto model)
        {
            if (model != null)
            {
                var result = await _UserWiseLandStatusReportService.GetPagedUserWiseLandStatusReport(model);
                // ViewBag.ReportType = model.reportType;
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show("No Data Found", "", AlertType.Warning);
                return PartialView("_List", null);
            }
        }





 




    }
}



