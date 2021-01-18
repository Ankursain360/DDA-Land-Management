
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

namespace EncroachmentDemolition.Controllers
{
    public class DemolitionReportController : BaseController
    {
        private readonly IEncroachmentRegisterationService _encroachmentRegisterationService;

        public DemolitionReportController(IEncroachmentRegisterationService encroachmentRegisterationService)
        {
            _encroachmentRegisterationService = encroachmentRegisterationService;
        }

        public async Task<IActionResult> Index()
        {
            DemolitionReportDtoProfile encroachmentregistration = new DemolitionReportDtoProfile();
           
            ViewBag.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList();
            return View(encroachmentregistration);
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
