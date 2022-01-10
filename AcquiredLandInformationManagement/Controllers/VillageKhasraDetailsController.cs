using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dto.Search;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Newtonsoft.Json;
using System.Text;

namespace AcquiredLandInformationManagement.Controllers
{
    public class VillageKhasraDetailsController : BaseController
    {
        private readonly IPossessiondetailsService _Possessiondetailservice;
        public VillageKhasraDetailsController(IPossessiondetailsService possessiondetailsService)
        {
            _Possessiondetailservice = possessiondetailsService;
        }
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
            var result = await _Possessiondetailservice.GetPagedKhasraDetails(model);
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
