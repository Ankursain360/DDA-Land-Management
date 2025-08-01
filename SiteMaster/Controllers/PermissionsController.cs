﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Service.IApplicationService;
using Dto.Master;
using SiteMaster.Filters;
using Core.Enum;
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


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            List<Module> lstModule = await _moduleService.GetActiveModule();
            List<RoleDto> lstRole = await _userProfileService.GetRole();
            ViewBag.Modules = lstModule;
            ViewBag.Roles = lstRole;
            return View();
        }


        public async Task<JsonResult> AddUpdatePermission([FromBody] List<MenuActionRoleMapDto> model)
        {
            if (model.Count > 0)
            {
                bool result = await _permissionsService.AddUpdatePermission(model);
                if (result)
                {
                    return Json("Permission updated successfully.");
                }
                else
                {
                    return Json("Error occur during update the record.");
                }
            }
            else
            {
                return Json("Please select atleast one record.");
            }
        }
        public async Task<JsonResult> NotAnyPermissionForRole([FromBody] MenuActionRoleMapDto model)
        {

            bool result = await _permissionsService.NotAnyPermissionForRole(model);
            if (result)
            {
                return Json("Permission updated successfully.");
            }
            else
            {
                return Json("Permission updated successfully.");
            }

        }

        public IActionResult GetPermissions(int moduleId, int roleId)
        {
            return ViewComponent("Permission", new { moduleId = moduleId, roleId = roleId });
        }
    }
}