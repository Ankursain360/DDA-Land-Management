using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;

namespace EncroachmentDemolition.Controllers
{
    public class AnnexureAController : Controller
    {
        private readonly  IAnnexureAService _annexureAService;
        public AnnexureAController(IAnnexureAService annexureAService)
        {
            _annexureAService = annexureAService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            var list = await _annexureAService.GetDemolitionchecklist();
            return View(list);
        }
    }
}