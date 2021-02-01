
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
using EncroachmentDemolition.Filters;
using Core.Enum;

namespace EncroachmentDemolition.Controllers
{
    public class DemolitionReportController : Controller
    {
        private readonly IEncroachmentRegisterationService _encroachmentRegisterationService;

        public DemolitionReportController(IEncroachmentRegisterationService encroachmentRegisterationService)
        {
            _encroachmentRegisterationService = encroachmentRegisterationService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            EncroachmentRegisteration model = new EncroachmentRegisteration();


            model.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] DemolitionReportSearchDto model)
        {
            var result = await _encroachmentRegisterationService.GetPagedDemolitionReport(model);
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
