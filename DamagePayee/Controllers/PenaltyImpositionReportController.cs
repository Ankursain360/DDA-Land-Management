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
    public class PenaltyImpositionReportController : BaseController
    {
        private readonly IDemandLetterService _demandLetterService;
        public PenaltyImpositionReportController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }

        async Task BindDropDownView(Demandletters demandletters)
        {
            demandletters.LocalityList = await _demandLetterService.GetLocalityList();
            demandletters.FileNoList = await _demandLetterService.GetFileNoList();
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            Demandletters model = new Demandletters();

            await BindDropDownView(model);
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] PenaltyImpositionReportSearchDto report)
        {
            var result = await _demandLetterService.GetPagedPenaltyImpositionReport(report);
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
