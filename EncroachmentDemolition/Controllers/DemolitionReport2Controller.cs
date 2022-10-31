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
using EncroachmentDemolition.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;

namespace EncroachmentDemolition.Controllers
{
    public class DemolitionReport2Controller : Controller
    {
        private readonly IDemolitionstructuredetailsService _demolitionstructuredetailsService;

        public DemolitionReport2Controller(IDemolitionstructuredetailsService demolitionstructuredetailsService)
        {
            _demolitionstructuredetailsService = demolitionstructuredetailsService;
        }
        // Dropdown Dependency  calls below
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Demolitionstructuredetails demolitionstructuredetails = new Demolitionstructuredetails();
            demolitionstructuredetails.DepartmentList = await _demolitionstructuredetailsService.GetAllDepartment();
            demolitionstructuredetails.ZoneList = await _demolitionstructuredetailsService.GetAllZone(Convert.ToInt32(demolitionstructuredetails.DepartmentId ?? 0));
            demolitionstructuredetails.DivisionList = await _demolitionstructuredetailsService.GetAllDivisionList(Convert.ToInt32(demolitionstructuredetails.ZoneId ?? 0));
            demolitionstructuredetails.LocalityList = await _demolitionstructuredetailsService.GetAllLocalityList(Convert.ToInt32(demolitionstructuredetails.DivisionId ?? 0));
            return View(demolitionstructuredetails);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] DemolitionReportZoneDivisionLocalityWiseSearchDto dto)
        {
            var result = await _demolitionstructuredetailsService.GetPagedDemolitionReportDataDepartmentZoneWise(dto);
            if (result != null)
            {
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }
        public async Task<IActionResult> getAllDemolitionReport([FromBody] DemolitionReportZoneDivisionLocalityWiseSearchDto model)
        {
            var result = await _demolitionstructuredetailsService.GetAllDemolitionReport(model);
            List<DemolitionReportListDto> data = new List<DemolitionReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DemolitionReportListDto()
                    {
                        Department = result[i].Department ==null?"":result[i].Department.Name,
                        Zone = result[i].Zone == null ? "": result[i].Zone.Name,
                        Division = result[i].Division == null ? "" : result[i].Division.Name,
                        Locality_VillageName = result[i].Locality == null ? "":result[i].Locality.Name,
                        khasra_PlotNo = result[i].FixingDemolition.Encroachment.KhasraNoNavigation.LocalityId == null ? result[i].FixingDemolition.Encroachment.KhasraNoNavigation.PlotNo : result[i].FixingDemolition.Encroachment.KhasraNoNavigation.KhasraNo,
                        DemolitionDate = result[i].EndOfDemolitionActionDate.HasValue ? Convert.ToDateTime(result[i].EndOfDemolitionActionDate).ToString("dd-MM-yyyy"):"",
                        AreaReclaimed_Sq_mtr = result[i].Areareclaimedrpt.Count >0?result[i].Areareclaimedrpt.Select(x=>x.AreaReclaimed).Sum().ToString():"_",
                        ReasonofDemolition = result[i].Remarks
                    });

                }

            }
            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok(); 
        }
        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DemolitionReport2Data.xlsx");
        }
    }
}



