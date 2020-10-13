using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
namespace EncroachmentDemolition.Controllers
{
    public class WacthWardPeriodReportController : Controller
    {
        private readonly IWatchandwardService _watchandwardService;

        public WacthWardPeriodReportController(IWatchandwardService watchandwardService)
        {
            _watchandwardService = watchandwardService;
        }



        // Dropdown Dependency  calls below
      
        //[HttpGet]
        //public async Task<JsonResult> GetLocalityList(int? DivisionId)
        //{
        //    DivisionId = DivisionId ?? 0;
        //    return Json(await _watchandwardService.GetAllVillage());
        //}


        public async Task<IActionResult> Create()
            {
            Watchandward model = new Watchandward();
           
            model.VillageList = await _watchandwardService.GetAllVillage();
            return View(model);
        }


        public async Task<PartialViewResult> GetDetails(int village, DateTime fromdate, DateTime todate)
        {
                var result = await _watchandwardService.GetWatchandwardReportData(village, fromdate, todate);

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

