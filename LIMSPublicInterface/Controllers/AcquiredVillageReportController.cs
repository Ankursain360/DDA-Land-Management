
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
using LIMSPublicInterface.Filters;
using Core.Enum;
namespace LIMSPublicInterface.Controllers
{
    public class AcquiredVillageReportController : BaseController
    {
        private readonly IAcquiredlandvillageService _acquiredlandvillageService;

        public AcquiredVillageReportController(IAcquiredlandvillageService acquiredlandvillageService)
        {
            _acquiredlandvillageService = acquiredlandvillageService;
        }



        public async Task<IActionResult> Index()
        {
            Acquiredlandvillage model = new Acquiredlandvillage();


            model.VillageList = await _acquiredlandvillageService.GetAllVillageList();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] AcquiredVillageReportSearchDto model)
        {
            var result = await _acquiredlandvillageService.GetPagedAcquiredVillageReport(model);
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

