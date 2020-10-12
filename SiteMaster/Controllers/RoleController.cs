using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Identity;
using Model.Entity;

namespace SiteMaster.Controllers
{
    public class RoleController : BaseController
    {
        private readonly RoleManager<ApplicationRole> _roleService;

        public RoleController(RoleManager<ApplicationRole> roleService)
        {
            _roleService = roleService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RoleSearchDto model)
        {
            var result = await _roleService.Roles.ToListAsync();
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create()
        {
            Role role = new Role();
            role.IsActive = 1;
            return View(role);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationRole role = new ApplicationRole() {
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
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(model);
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _roleService.FindByNameAsync(Name);
            if (string.IsNullOrEmpty(result.Name))
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
            var Data = await _roleService.FindByIdAsync(id.ToString());
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Role role)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var result = await _roleService.Update(id, role);
        //            if (result == true)
        //            {
        //                ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
        //                var list = await _roleService.GetAllRole();
        //                return View("Index", list);
        //            }
        //            else
        //            {
        //                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //                return View(role);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //            return View(role);
        //        }
        //    }
        //    else
        //    {
        //        return View(role);
        //    }
        //}

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
        //    var list = await _roleService.GetAllRole();
        //    return View("Index", list);
        //}

        //public async Task<IActionResult> View(int id)
        //{
        //    var Data = await _roleService.FetchSingleResult(id);
        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}
    }
}
