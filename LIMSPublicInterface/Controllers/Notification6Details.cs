using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;


namespace LIMSPublicInterface.Controllers
{
    public class Notification6Details : Controller
    {
        private readonly IUndersection6plotService _undersection4PlotService;

        public Notification6Details(IUndersection6plotService undersection4PlotService)
        {
            _undersection4PlotService = undersection4PlotService;
        }
        public async Task<IActionResult> Index()
        {
            Undersection6plot undersection4plot = new Undersection6plot();
            undersection4plot.IsActive = 1;
            undersection4plot.NotificationList = await _undersection4PlotService.GetAllNotificationNo();

            return View(undersection4plot);
        }



        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] Unotification6detailsSearchDto model)
        {
            var result = await _undersection4PlotService.GetPagednotification6detailsList(model);
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




    }
}
