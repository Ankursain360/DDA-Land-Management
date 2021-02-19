using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using Dto.Search;
using Dto.Master;
using LIMSPublicInterface.Filters;
using Core.Enum;
namespace LIMSPublicInterface.Controllers
{
    public class PossessionReportController : BaseController
    {
        private readonly IPossessiondetailsService _possessiondetailsService;

        public PossessionReportController(IPossessiondetailsService possessiondetailsService)
        {
            _possessiondetailsService = possessiondetailsService;
        }
        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            PossessionReportDtoProfile demandletter = new PossessionReportDtoProfile();
            ViewBag.PossessionDateList = await _possessiondetailsService.BindPossessionDateList();
            return View(demandletter);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] PossessionReportSearchDto model)
        {
            var result = await _possessiondetailsService.GetPagedPossessionReport(model);

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