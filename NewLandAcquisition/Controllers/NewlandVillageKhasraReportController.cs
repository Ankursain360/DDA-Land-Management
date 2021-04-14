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
using NewLandAcquisition.Filters;
using Core.Enum;
namespace NewLandAcquisition.Controllers
{
    public class NewlandVillageKhasraReportController : BaseController
    {
        private readonly INewlandkhasraService _NewlandkhasraService;

        public NewlandVillageKhasraReportController(INewlandkhasraService newlandkhasraService)
        {
            _NewlandkhasraService = newlandkhasraService;
        }
        // Dropdown Dependency  calls below
        [HttpGet]
        public async Task<JsonResult> GetAllKhasraList(int? NewLandvillageId)
        {
            NewLandvillageId = NewLandvillageId ?? 0;
            return Json(await _NewlandkhasraService.GetAllKhasraList(Convert.ToInt32(NewLandvillageId)));
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Newlandkhasra model = new Newlandkhasra();
            model.VillageList = await _NewlandkhasraService.GetAllVillageList();
            model.KhasraList = await _NewlandkhasraService.GetAllKhasraList(model.NewLandvillageId);

            return View(model);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] NewlandVillageDetailsKhasraWiseReportSearchDto dto)
        {
            var result = await _NewlandkhasraService.GetPagednewlandVillageKhasraReport(dto);
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




