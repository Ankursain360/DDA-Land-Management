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
            Watchandward model = new Watchandward();


            model.LocalityList = await _watchandwardService.GetAllLocality();
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
    }
}