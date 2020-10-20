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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            var result = await _roleService.FindByNameAsync(Name);
            if (result==null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Role: {Name} already exist");
            }
        }

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
        public async Task<IActionResult> Edit(int id, RoleDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userProfileService.GetRoleById(id);
                if (result != null)
                {
                    result.Id = model.Id;
                    result.Name = model.Name;
                    result.IsActive = model.IsActive;
                    result.ModifiedBy = 1;
                    result.ModifiedDate = DateTime.Now;
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _userProfileService.UpdateRole(result);
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }



        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {

        //        var result = await _roleService.Delete(id);
        //        if (result == true)
        //        {
        //            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
        //        }
        //        else
        //        {
        //            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //    }
        //    var list = await _roleService.GetRole();
        //    return View("Index", list);
        //}



    }
}
