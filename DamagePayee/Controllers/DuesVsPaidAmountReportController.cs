using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using DamagePayee.Filters;
using Core.Enum;
namespace DamagePayee.Controllers
{
    public class DuesVsPaidAmountReportController : BaseController
    {
        private readonly IDemandLetterService _demandLetterService;


        public DuesVsPaidAmountReportController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ViewBag.FileNoList = await _demandLetterService.BindFileNoList();
           
            return View();
        }

      


        [HttpPost]
        public async Task<PartialViewResult> GetPaidVsDemandList([FromBody] DuesVsPaidAmountSearchDto model)
        {
            var result = await _demandLetterService.GetDuesVsPaidAmountListDto(model);

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
