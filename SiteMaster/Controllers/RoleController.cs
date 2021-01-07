using Core.Enum;
using Dto.Master;
using Dto.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Service.IApplicationService;
using SiteMaster.Filters;
using System;
using System.Threading.Tasks;

namespace SiteMaster.Controllers
{
    public class RoleController : BaseController
    {
        private readonly RoleManager<ApplicationRole> _roleService;
        private readonly IUserProfileService _userProfileService;

        public RoleController(RoleManager<ApplicationRole> roleService,
            IUserProfileService userProfileService)
        {
            _roleService = roleService;
            _userProfileService = userProfileService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RoleSearchDto model)
        {
            var result = await _userProfileService.GetPagedRole(model);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(RoleDto model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = new ApplicationRole()
                {
                    Name = model.Name,
                    IsActive = 1,
                };
                var result = await _roleService.CreateAsync(role);

                if (result.Succeeded)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(role);
                }
            }
            else
            {
                return View(model);
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            //var result = await _roleService.FindByNameAsync(Name);
            var result = await _userProfileService.ValidateUniqueRoleName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Role: {Name} already exist");
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _userProfileService.GetRoleById(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Edit(int id, RoleDto model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                var list = await _userProfileService.UpdateRole( model);
                return View("Index", list);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(model);
            }
        }
     


        public async Task<IActionResult> Delete(int id, RoleDto model)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                    var list = await _userProfileService.DeleteRole(model);
                    return View("Index", list);
                //}
                //else
                //{
                //    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                //    return View(model);
                //}
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }

            return View("Index");
        }

        public async Task<IActionResult> View(int id)
        {
            var result = await _userProfileService.GetRoleById(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }


    }
}
