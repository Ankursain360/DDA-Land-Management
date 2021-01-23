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
using DamagePayee.Filters;
using Core.Enum;
namespace DamagePayee.Controllers
{
    public class ImpositionOfChargesController : BaseController
    {
        private readonly IDemandLetterService _demandLetterService;

        public ImpositionOfChargesController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }


       [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ImpositionOfChargesDtoProfile demandletter = new ImpositionOfChargesDtoProfile();
            ViewBag.FileNoList = await _demandLetterService.BindFileNoList();
            ViewBag.LocalityList = await _demandLetterService.BindLoclityList();
            return View(demandletter);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] ImpositionOfChargesSearchDto model)
        {
            var result = await _demandLetterService.GetPagedImpositionReportOfCharges(model);

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