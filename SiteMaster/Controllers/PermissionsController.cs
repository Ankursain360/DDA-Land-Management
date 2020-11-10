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

        public PermissionsController(IPermissionsService permissionsService, IUserProfileService userProfileService)
        {
            _permissionsService = permissionsService;
            _userProfileService = userProfileService;
        }

        public IActionResult Index()
        {
            return View();
        }
        async Task BindDropDown(Menuactionrolemap menuactionrolemap)
        {
            //menuactionrolemap.ModuleList = await _permissionsService.GetModuleList();
        }
        public async Task<IActionResult> Create()
        {
            Menuactionrolemap menuactionrolemap = new Menuactionrolemap();
           await BindDropDown(menuactionrolemap);
            return View(menuactionrolemap);
        }

        [HttpGet]
        public async Task<JsonResult> GetRoleList()
        {
            var data = await _userProfileService.GetRole();
            return Json(data);
        }
    }
}