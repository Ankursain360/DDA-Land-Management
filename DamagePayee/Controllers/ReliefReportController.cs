using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using Dto.Search;

namespace DamagePayee.Controllers
{
    public class ReliefReportController : BaseController
    {
        private readonly IDemandLetterService _demandLetterService;

        public ReliefReportController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }

        public async Task<IActionResult> Index()
        {
            Demandletter demandletter = new Demandletter();
            //ViewBag.LocalityList = _demandLetterService.BindLoclityList();
            return View(demandletter);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] ReliefReportSearchDto model)
        {
            var result = await _demandLetterService.GetPagedReliefReport(model);

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