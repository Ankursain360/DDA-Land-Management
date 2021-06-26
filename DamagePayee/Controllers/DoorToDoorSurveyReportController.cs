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
    public class DoorToDoorSurveyReportController : BaseController
    {
        private readonly IDoortodoorsurveyService _DoortodoorsurveyService;

        public DoorToDoorSurveyReportController(IDoortodoorsurveyService DoortodoorsurveyService)
        {
            _DoortodoorsurveyService = DoortodoorsurveyService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Doortodoorsurvey model = new Doortodoorsurvey();

            model.presentuse = await _DoortodoorsurveyService.GetAllPresentuse();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] DoorToDoorSurveyReportSearchDto model)
        {
            var result = await _DoortodoorsurveyService.GetPagedDoortodoorsurveyReport(model);
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
