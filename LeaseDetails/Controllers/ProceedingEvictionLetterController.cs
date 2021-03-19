using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using LeaseDetails.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dto.Search;
using LeaseDetails.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;

namespace LeaseDetails.Controllers
{
    public class ProceedingEvictionLetterController : BaseController
    {

        private readonly IProceedingEvictionLetterService _proceedingEvictionLetterService;

        public ProceedingEvictionLetterController(IProceedingEvictionLetterService proceedingEvictionLetterService)
        {
            _proceedingEvictionLetterService = proceedingEvictionLetterService;
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            ProceedingEvictionLetterCreateProfileDto data = new ProceedingEvictionLetterCreateProfileDto();
            data.RefNoNameList = await _proceedingEvictionLetterService.BindRefNoNameList();
            ViewBag.VisibleLetter = 0;
            return View(data);
        }

        [HttpPost]
        public async Task<PartialViewResult> ViewLetter([FromBody] ProceedingEvictionLetterSearchDto model)
        {
            var result = false;
            if(model !=  null)
            {
                var IsUpdate = await _proceedingEvictionLetterService.GetLetterRefNo(Convert.ToInt32(model.RefNoNameId));
                if (IsUpdate == null)
                {
                    result = await _proceedingEvictionLetterService.UpdateRequestProceeding(model, SiteContext.UserId);
                }
                else
                {
                    if (model.RefNoNameId != 0 && model.LetterReferenceNo != null)
                    {
                        result = true;
                    }
                }
            }

            if(result)
            {
                var data = await _proceedingEvictionLetterService.FetchProceedingConvictionLetterData(model);
                ViewBag.VisibleLetter = 1;
                return PartialView("_ViewLetter", data);
            }
            else
            {
                Requestforproceeding data = new Requestforproceeding();
                ViewBag.Message = Alert.Show("No data Found", "", AlertType.Info);
                return PartialView("_ViewLetter", data);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetLetterRefNo(int? Id)
        {
            Id = Id ?? 0;
            return Json(await _proceedingEvictionLetterService.GetLetterRefNo(Convert.ToInt32(Id)));
        }
    }
}