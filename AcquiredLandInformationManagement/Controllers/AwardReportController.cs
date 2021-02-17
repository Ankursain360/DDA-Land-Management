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
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
namespace AcquiredLandInformationManagement.Controllers
{
    public class AwardReportController : BaseController
    {
        private readonly IAwardplotDetailService _awardPlotDetailsService;

        public AwardReportController(IAwardplotDetailService awardPlotDetailsService)
        {
            _awardPlotDetailsService = awardPlotDetailsService;
        }
        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            AwardReportDtoProfile data= new AwardReportDtoProfile();
            ViewBag.AwardNoDateList = await _awardPlotDetailsService.BindAwardNoDateList();
            return View(data);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] AwardReportSearchDto model)
        {
            var result = await _awardPlotDetailsService.GetPagedPossessionReport(model);

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