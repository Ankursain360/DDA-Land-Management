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
using AcquiredLandInformationManagement.Filters;
using Core.Enum;

namespace AcquiredLandInformationManagement.Controllers
{
    public class VillageAndKhasraDetailsController : Controller
    {
        private readonly IPossessiondetailsService _Possessiondetailservice;


        public VillageAndKhasraDetailsController(IPossessiondetailsService possessiondetailsService)
        {
            _Possessiondetailservice = possessiondetailsService;
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Possessiondetails undersection4plot = new Possessiondetails();
            undersection4plot.IsActive = 1;

            undersection4plot.KhasraList = await _Possessiondetailservice.BindKhasra(undersection4plot.VillageId);
            undersection4plot.VillageList = await _Possessiondetailservice.GetAllVillage();

            return View(undersection4plot);
        }


        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _Possessiondetailservice.BindKhasra(Convert.ToInt32(villageId)));
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] VillageAndKhasraDetailsSearchDto model)
        {
            var result = await _Possessiondetailservice.GetPagedvillageAndKhasradetailsList(model);
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
