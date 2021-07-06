
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
namespace LandInventory.Controllers
{
    public class StatusOfVacantLandController : BaseController
    {
        private readonly IStatusofVacantLandService _statusofVacantLandService;
        public StatusOfVacantLandController(IStatusofVacantLandService statusofVacantLandService)
        {
            _statusofVacantLandService = statusofVacantLandService;
        }


      
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] StatusOfVacantLandSearchDto model)
        {
            if (model != null)
            {
                var result = await _statusofVacantLandService.GetStatusOfVacantLandReportData(model);
               
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show("No Data Found", "", AlertType.Warning);
                return PartialView("_List", null);
            }
        }
        //public async Task<IActionResult> HandoverTakeoverReportDepartmentZoneDivisionLocalitywiseList()
        //{
        //    var result = await _landtransferService.GetAllLandTransferList();
        //    List<HandovertakeoverReportDepartmentZoneDivisionLocalityDto> data = new List<HandovertakeoverReportDepartmentZoneDivisionLocalityDto>();
        //    if (result != null)
        //    {
        //        for (int i = 0; i < result.Count; i++)
        //        {
        //            data.Add(new HandovertakeoverReportDepartmentZoneDivisionLocalityDto()
        //            {
        //                Id = result[i].Id,
        //                Department = result[i].PropertyRegistration.Department.Name,
        //                Zone = result[i].PropertyRegistration.Zone.Name,
        //                Division = result[i].PropertyRegistration.Division.Name,
        //                //Locality = result[i].PropertyRegistration.Division.Name,


        //                Locality = result[i].PropertyRegistration.LocalityId == null ? " " : result[i].PropertyRegistration.Locality.Name,
        //                KhasraNo = result[i].PropertyRegistration.KhasraNo == null ? " " : result[i].PropertyRegistration.KhasraNo,

        //                HandedOverBy = result[i].HandedOverByNameDesingnation,
        //                HandedOverDate = Convert.ToString(result[i].HandedOverDate),
        //                TakenOverBy = result[i].TakenOverByNameDesingnation,
        //                TakenOverDate = result[i].DateofTakenOver.ToString(),
        //                TransferorderIssueAuthority = result[i].TransferorderIssueAuthority,
        //                Remarks = result[i].Remarks,

        //            }); ;
        //        }
        //    }

        //    var memory = ExcelHelper.CreateExcel(data);
        //    return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //}



    }
}
