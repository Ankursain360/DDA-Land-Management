using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
namespace LandInventory.Controllers
{
    public class ReportofLandTransferDivisionLocalityWiseController : Controller
    {
        private readonly ILandTransferService _landtransferService;

        public ReportofLandTransferDivisionLocalityWiseController(ILandTransferService landtransferService)
        {
            _landtransferService = landtransferService;
        }



        // Dropdown Dependency  calls below
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _landtransferService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _landtransferService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _landtransferService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }


        public async Task<IActionResult> Create()
        {
            Landtransfer model = new Landtransfer();
            model.DepartmentList = await _landtransferService.GetAllDepartment();
            model.ZoneList = await _landtransferService.GetAllZone(model.DepartmentId);
            model.DivisionList = await _landtransferService.GetAllDivisionList(model.ZoneId);
            model.LocalityList = await _landtransferService.GetAllLocalityList(model.DivisionId);
            return View(model);
        }


        public async Task<PartialViewResult> GetDetails(int department, int zone, int division, int locality)
        {
            var result = await _landtransferService.GetLandTransferReportData(department, zone, division, locality);

            if (result != null)
            {
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
            //[HttpPost]
            //public async Task<PartialViewResult> List([FromBody] LandTransferSearchDto model)
            //{

            //    if (model != null)
            //    {
            //        var result = await _landtransferService.GetPagedLandtransferReportDeptWise(model);
            //        ViewBag.ReportType = model.reportType;
            //        return PartialView("_List", result);
            //    }
            //    else
            //    {
            //        ViewBag.Message = Alert.Show("No Data Found", "", AlertType.Warning);
            //        return PartialView("_List", null);
            //    }
            //}

        }
    }
}


