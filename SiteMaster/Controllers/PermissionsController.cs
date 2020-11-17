using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Service.IApplicationService;

namespace SiteMaster.Controllers
{
    public class PermissionsController : BaseController
    {
        private readonly IPermissionsService _permissionsService;
        private readonly IUserProfileService _userProfileService;
        private readonly IModuleService _moduleService;

        public PermissionsController(IPermissionsService permissionsService,
            IUserProfileService userProfileService,
            IModuleService moduleService)
        {
            _permissionsService = permissionsService;
            _userProfileService = userProfileService;
            _moduleService = moduleService;
        }

        public async Task<IActionResult> Index()
        {
            List<Module> lstModule = await _moduleService.GetActiveModule();
            ViewBag.Modules = lstModule;
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetRoleList()
        {
            var data = await _userProfileService.GetRole();
            return Json(data);
        }
    }
}