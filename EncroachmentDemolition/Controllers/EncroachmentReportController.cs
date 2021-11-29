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
using Utility.Helper;
namespace EncroachmentDemolition.Controllers
{
    public class EncroachmentReportController : Controller
    {
        private readonly IEncroachmentRegisterationService _encroachmentregistrationService;

        public EncroachmentReportController(IEncroachmentRegisterationService encroachmentregistrationService)
        {
            _encroachmentregistrationService = encroachmentregistrationService;
        }
        // Dropdown Dependency  calls below
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _encroachmentregistrationService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _encroachmentregistrationService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _encroachmentregistrationService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            EncroachmentRegisteration model = new EncroachmentRegisteration();
            model.DepartmentList = await _encroachmentregistrationService.GetAllDepartment();
            model.ZoneList = await _encroachmentregistrationService.GetAllZone(model.DepartmentId);
            model.DivisionList = await _encroachmentregistrationService.GetAllDivisionList(model.ZoneId);
            model.LocalityList = await _encroachmentregistrationService.GetAllLocalityList(model.DivisionId);
            return View(model);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody]EnchroachmentSearchDto dto)
        {
            var result = await _encroachmentregistrationService.GetEncroachmentReportData(dto);
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


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            var result = await _encroachmentregistrationService.GetAllEncroachmentRegisteration();
            List<EncroachmentListDto> data = new List<EncroachmentListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new EncroachmentListDto()
                    {
                        Department = result[i].Department == null ? "" : result[i].Department.Name.ToString(),
                        
                        Zone = result[i].Zone == null ? "" : result[i].Zone.Name.ToString(),
                        Division = result[i].Division == null ? "" : result[i].Division.Name.ToString(),                      
                          DateofDemolition = Convert.ToDateTime(result[i].EncrochmentDate).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].EncrochmentDate).ToString("dd-MMM-yyyy"),
                        Encroachment = result[i].IsEncroachment == "" ? "No" : result[i].IsEncroachment.ToString(),




                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}



