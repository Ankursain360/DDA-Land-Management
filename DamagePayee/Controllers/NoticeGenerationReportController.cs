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

namespace DamagePayee.Controllers
{
    public class NoticeGenerationReportController : BaseController
    {
        private readonly INoticeToDamagePayeeService _noticeToDamagePayeeService;

        public NoticeGenerationReportController(INoticeToDamagePayeeService noticeToDamagePayeeService)
        {
            _noticeToDamagePayeeService = noticeToDamagePayeeService;
        }
        public async Task<IActionResult> Index()
        {
            Noticetodamagepayee model = new Noticetodamagepayee();

            model.FileNoList = await _noticeToDamagePayeeService.GetFileNoList();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] NoticeGenerationReportSearchDto notice)
        {
            var result = await _noticeToDamagePayeeService.GetPagedNoticeGenerationReport(notice);
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
