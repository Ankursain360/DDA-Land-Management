
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
using Dto.Master;

namespace EncroachmentDemolition.Controllers
{
    public class DemolitionReportController : Controller
    {
        private readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
      
        public DemolitionReportController(IEncroachmentRegisterationService encroachmentRegisterationService)
        {
            _encroachmentRegisterationService = encroachmentRegisterationService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            EncroachmentReportListDto model = new EncroachmentReportListDto();
            ViewBag.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList();
            model.FromDate = DateTime.Now.AddDays(-30);
            model.ToDate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] DemolitionReportSearchDto model)
        {
            var result = await _encroachmentRegisterationService.GetPagedDemolitionReport(model);
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
        public async Task<IActionResult> DemolationReporlist([FromBody] DemolitionReportSearchDto model)
        {
            var result = await _encroachmentRegisterationService.GetAllDownloadEncroachmentList(model);
            List<EncroachmentReportListDto> data = new List<EncroachmentReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new EncroachmentReportListDto()
                    {
                        Id = result[i].Id,
                        Department = result[i].Department == null ? "" : result[i].Department.Name,
                        Zone = result[i].Zone == null ? "" : result[i].Zone.Name,
                        Division = result[i].Division == null ? "" : result[i].Division.Name,
                        VillageName = result[i].Locality == null ? "" : result[i].Locality.Name,
                        KhasraNo = result[i].KhasraNoNavigation.LocalityId == null ? "" : result[i].KhasraNoNavigation.KhasraNo,
                        Encroachment = result[i].IsEncroachment == null ? "No" : result[i].IsEncroachment,

                        DateofDemolition = Convert.ToDateTime(result[i].EncrochmentDate).ToString("dd-MMM-yyyy"),
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
            try
            {
                byte[] data = TempData["file"] as byte[];
                return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception ex)
            {

                return Json(new { status = "error", message = "Please generet the record before downloading" }); 
            }
            //byte[] data = TempData["file"] as byte[];            
            //if (data != null)
            //{
            //    return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //}
            //else
            //{
            //    TempData["msg"] = "<script>alert('Please generet the record before downloading')</script>";
            //}
            //string msg = TempData["msg"].ToString();
            //return View(msg);
        }
    }
}
