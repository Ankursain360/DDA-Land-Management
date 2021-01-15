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

namespace DamagePayee.Controllers
{
    public class DemandCollectionLedgerController : BaseController
    {
        private readonly IDemandLetterService _demandLetterService;

        public DemandCollectionLedgerController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }

        public async Task<IActionResult> Create()
        {
            DemandCollectionLedgerReportDtoProfile demandletter = new DemandCollectionLedgerReportDtoProfile();
            ViewBag.FileNoList = await _demandLetterService.BindFileNoList();
            ViewBag.LocalityList = await _demandLetterService.BindLoclityList();
            ViewBag.PropertyNo = await _demandLetterService.BindFileNoList();
            return View(demandletter);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] DemandCollectionLedgerSearchDto model)
        {
            var result = await _demandLetterService.GetPagedDemandCollectionLedgerReport1(model);

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