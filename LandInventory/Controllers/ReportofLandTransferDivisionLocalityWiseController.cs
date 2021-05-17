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
using LandInventory.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
namespace LandInventory.Controllers
{
    public class ReportofLandTransferDivisionLocalityWiseController : BaseController
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

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Landtransfer model = new Landtransfer();
            model.DepartmentList = await _landtransferService.GetAllDepartment();
            model.ZoneList = await _landtransferService.GetAllZone(0);
            model.DivisionList = await _landtransferService.GetAllDivisionList(0);   
            model.LocalityList = await _landtransferService.GetAllLocalityList(0);   
            return View(model);
        }


       
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LandTransferReportDivisionLocalitySearchDto model)
        {
            if (model != null)
            {              
                var result = await _landtransferService.GetPagedLandTransferReportData(model);
               // ViewBag.ReportType = model.reportType;
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show("No Data Found", "", AlertType.Warning);
                return PartialView("_List", null);
            }
        }





        public async Task<IActionResult> HandoverTakeoverReportDepartmentZoneDivisionLocalitywiseList()
        {
            var result = await _landtransferService.GetAllLandTransferList();
            List<HandovertakeoverReportDepartmentZoneDivisionLocalityDto> data = new List<HandovertakeoverReportDepartmentZoneDivisionLocalityDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new HandovertakeoverReportDepartmentZoneDivisionLocalityDto()
                    {
                        Id = result[i].Id,
                        Department = result[i].PropertyRegistration.Department.Name,
                        Zone = result[i].PropertyRegistration.Zone.Name,
                        Division = result[i].PropertyRegistration.Division.Name,
                        //Locality = result[i].PropertyRegistration.Division.Name,


                        Locality = result[i].PropertyRegistration.LocalityId == null ? " " : result[i].PropertyRegistration.Locality.Name,
                        KhasraNo = result[i].PropertyRegistration.KhasraNo == null ? " " : result[i].PropertyRegistration.KhasraNo,

                        HandedOverBy = result[i].HandedOverByNameDesingnation,
                        HandedOverDate = Convert.ToString(result[i].HandedOverDate),
                        TakenOverBy = result[i].TakenOverByNameDesingnation,
                        TakenOverDate = result[i].DateofTakenOver.ToString(),
                        TransferorderIssueAuthority = result[i].TransferorderIssueAuthority,
                        Remarks = result[i].Remarks,

                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }




    }
}



