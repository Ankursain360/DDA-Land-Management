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
    public class WacthWardPeriodReportController : Controller
    {
        private readonly IWatchandwardService _watchandwardService;

        public WacthWardPeriodReportController(IWatchandwardService watchandwardService)
        {
            _watchandwardService = watchandwardService;
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            WatchWardListDto model = new WatchWardListDto(); 
            ViewBag.LocalityList = await _watchandwardService.GetAllLocality();
            model.FromDate = DateTime.Now.AddDays(-30);
            model.Todate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] WatchAndWardPeriodReportSearchDto watchAndWardPeriodReportSearchDto)
        {
            var result = await _watchandwardService.GetWatchandwardReportData(watchAndWardPeriodReportSearchDto);
            if (result != null)
            {
                return PartialView("Index", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }


       // [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> GetAllWatchWardPeriodReportList([FromBody] WatchAndWardPeriodReportSearchDto model)
        {
            var result = await _watchandwardService.GetAllWatchWardPeriodReport(model);
            List<WatchWardListDto> data = new List<WatchWardListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new WatchWardListDto()
                    {
                        Id = i + 1,
                        Date = Convert.ToDateTime(result[i].Date).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].Date).ToString("dd-MMM-yyyy"),
                        Loaclity = result[i].PrimaryListNoNavigation.LocalityId == null ? result[i].PrimaryListNoNavigation.Colony : result[i].PrimaryListNoNavigation.Locality.Name == null ? "" : result[i].PrimaryListNoNavigation.Locality.Name,
                        KhasraNo = result[i].PrimaryListNoNavigation.KhasraNo == null ? result[i].PrimaryListNoNavigation.PlotNo : result[i].PrimaryListNoNavigation.KhasraNo.ToString(),
                        PrimaryListNo = result[i].PrimaryListNoNavigation.PrimaryListNo == null ? "NA" : result[i].PrimaryListNoNavigation.PrimaryListNo,
                        LandMark= result[i].Landmark,
                        Encroachment = result[i].Encroachment.ToString() == "1" ? "Yes" : "No",
                        StatusOnGround = result[i].StatusOnGround.ToString(),
                       // Remarks = result[i].Remarks,                        
                        CreatedDate = Convert.ToDateTime(result[i].CreatedDate).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].CreatedDate).ToString("dd-MMM-yyyy"),

                        //IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"]  = memory;
            return Ok();
            //return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}