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
    public class InspectionRegisterPeriodReportController : Controller
    {
        private readonly IEncroachmentRegisterationService _encroachmentregistrationService;

        public InspectionRegisterPeriodReportController(IEncroachmentRegisterationService encroachmentregistrationService)
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
            model.FromDate = DateTime.Now.AddDays(-30);
            model.ToDate = DateTime.Now;
            //EncroachmentRegisteration encroachment
            //EncroachmentRegisterListDto model = new EncroachmentRegisterListDto();
            //ViewBag.Department = await _encroachmentregistrationService.GetAllDepartment();
            //ViewBag.Zone = await _encroachmentregistrationService.GetAllZone(encroachment.DepartmentId);
            //ViewBag.Division = await _encroachmentregistrationService.GetAllDivisionList(encroachment.ZoneId);
            //ViewBag.Loaclity = await _encroachmentregistrationService.GetAllLocalityList(encroachment.DivisionId);
            //model.Fromdate = DateTime.Now.AddDays(-30);
            //model.Todate = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] InspectionEncroachmentregistrationSearchDto dto)
        {
            var result = await _encroachmentregistrationService.GetEncroachmentRegisterationReportData(dto);
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
        public async Task<IActionResult> GetAllEncroachmentRegisterlistForDownload([FromBody] InspectionEncroachmentregistrationSearchDto model) 
        {
            var result = await _encroachmentregistrationService.GetAllEncroachmentRegisterlistForDownload2(model);
            List<EncroachmentRegisterListDto> data = new List<EncroachmentRegisterListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new EncroachmentRegisterListDto()
                    {
                        Id = i + 1,
                        Date = Convert.ToDateTime(result[i].EncrochmentDate).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].EncrochmentDate).ToString("dd-MMM-yyyy"),
                        Department = result[i].Department.Name,
                        Zone = result[i].Zone.Name,
                        Division = result[i].Division.Name,
                        Loaclity = result[i].Locality.Name,
                        PrimaryListNo = result[i].KhasraNoNavigation.PrimaryListNo,
                        KhasraNo = result[i].KhasraNoNavigation.KhasraNo == null ? result[i].KhasraNoNavigation.PlotNo : result[i].KhasraNoNavigation.KhasraNo.ToString(),
                        Encroachment = result[i].IsEncroachment.ToString(),
                        StatusOfLand = result[i].StatusOfLand.ToString(),
                        Status = result[i].ApprovedStatusNavigation == null ? "NA" : result[i].ApprovedStatusNavigation.SentStatusName.ToString(),
                        Area = result[i].Area.ToString(),
                        Remarks = result[i].Remarks,
                        PoliceStation = result[i].PoliceStation,
                        OfficerOnDuty = result[i].SecurityGuardOnDuty

                    }); ;
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}



