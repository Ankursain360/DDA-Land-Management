
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
using DamagePayee.Filters;
using Core.Enum;
namespace DamagePayee.Controllers
{
    public class DefaulterListingReportController : BaseController
    {
        private readonly IDemandLetterService _demandLetterService;

        public DefaulterListingReportController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Demandletters model = new Demandletters();

            model.Damagelist = await _demandLetterService.GetAllDemandletter();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] DefaulterListingReportSearchDto defaulterListingReportSearchDto)
        {
            var result = await _demandLetterService.GetDefaultListingReportData(defaulterListingReportSearchDto);
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
