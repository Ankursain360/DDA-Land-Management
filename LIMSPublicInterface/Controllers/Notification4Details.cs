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
    public class Notification4Details : Controller
    {
        private readonly IUndersection4PlotService _undersection4PlotService;

        public Notification4Details(IUndersection4PlotService undersection4PlotService)
        {
            _undersection4PlotService = undersection4PlotService;
        }
        public async Task<IActionResult> Index()
        {
            Undersection4plot undersection4plot = new Undersection4plot();
            undersection4plot.IsActive = 1;
            undersection4plot.NotificationList = await _undersection4PlotService.GetAllNotificationNo();
          
            return View(undersection4plot);
        }



        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] Unotification4detailsSearchDto model)
        {
            var result = await _undersection4PlotService.GetPagednotification4detailsList(model);
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
