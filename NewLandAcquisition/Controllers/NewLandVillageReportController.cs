
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
    public class NewLandVillageReportController : BaseController
    {
        private readonly INewlandvillageService _NewlandvillageService;

        public NewLandVillageReportController(INewlandvillageService NewlandvillageService)
        {
            _NewlandvillageService = NewlandvillageService;  
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Newlandvillage model = new Newlandvillage();



            model.VillageList = await _NewlandvillageService.GetNewlandvillage();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] NewlandVillageReportSearchDto model)
        {
            var result = await _NewlandvillageService.GetPagedNewLandVillageReport(model);
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
