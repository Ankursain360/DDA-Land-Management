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

namespace SiteMaster.Controllers
{
   
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;


        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        public async Task< IActionResult> Index()
        {
            var list = await _roleService.GetAllRole();
            return View(list);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RoleSearchDto model)
        {
            var result = await _roleService.GetPagedRole(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create()
        {
            Role role = new Role();
            role.IsActive = 1;
            role.ZoneList = await _roleService.GetAllZone();
            return View(role);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            try
            {
                role.ZoneList = await _roleService.GetAllZone();
                if (ModelState.IsValid)
                {
                    role.ZoneId = 0;
                    var result = await _roleService.Create(role);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _roleService.GetAllRole();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(role);
                    }
                }
                else
                {
                    return View(role);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(role);
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _roleService.CheckUniqueName(Id, Name);
            if (result == false)
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
            var Data = await _roleService.FetchSingleResult(id);
            Data.ZoneList = await _roleService.GetAllZone();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Role role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _roleService.Update(id, role);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _roleService.GetAllRole();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(role);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(role);
                }
            }
            else
            {
                return View(role);
            }
        }

        public async Task<IActionResult> Delete(int id)  
        {
            try
            {
                var result = await _roleService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _roleService.GetAllRole();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _roleService.FetchSingleResult(id);
            Data.ZoneList = await _roleService.GetAllZone();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}
