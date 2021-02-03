using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;
using DamagePayee.Filters;
using Core.Enum;
namespace DamagePayee.Controllers
{
    public class DuplicateDemandLetterController : Controller
    {
        private readonly IDemandLetterService _demandLetterService;
        public DuplicateDemandLetterController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DuplicateDemandLetterSearchDto model)
        {
            try
            {
                var result = await _demandLetterService.GetPagedDuplicateDemandletter(model);

                return PartialView("_List1", result);
            }
            catch (Exception ex)
            {
                return PartialView(ex);
            }
        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _demandLetterService.FetchSingleResult(id);
            Data.LocalityList = await _demandLetterService.GetLocalityList();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        //public IActionResult DuplicateDemandLetter()
        //{
        //    return PartialView();
        //}
    }
}
