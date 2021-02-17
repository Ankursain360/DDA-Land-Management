

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
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
namespace AcquiredLandInformationManagement.Controllers
{
    public class NazulVillageReportController : Controller
    {
        private readonly INazulService _nazulService;

        public NazulVillageReportController(INazulService nazulService)
        {
            _nazulService = nazulService;
        }



        public async Task<IActionResult> Index()
        {
            Nazul model = new Nazul();


            model.VillageList = await _nazulService.GetAllVillageList();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] NazulVillageReportSearchDto model)
        {
            var result = await _nazulService.GetNazulReportData(model);
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
