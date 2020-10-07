using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;

namespace LandingPage.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly IModuleService _moduleService;

        public LandingPageController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _moduleService.GetAllModule();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}



