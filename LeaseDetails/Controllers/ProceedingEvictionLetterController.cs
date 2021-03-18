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
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> ViewLetter([FromBody] ProceedingEvictionLetterSearchDto model)
        {
            return PartialView("_ViewLetter");
        }

        [HttpGet]
        public async Task<JsonResult> GetLetterRefNo(int? Id)
        {
            Id = Id ?? 0;
            return Json(await _proceedingEvictionLetterService.GetLetterRefNo(Convert.ToInt32(Id)));
        }
    }
}