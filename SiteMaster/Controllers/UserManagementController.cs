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
using Microsoft.AspNetCore.Identity;
using Model.Entity;

namespace SiteMaster.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserManagementController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
          
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] UserManagementSearchDto model)
        {
            
            var result = await _userService.GetPagedUser(model);
            
            return PartialView("_List", result);
        }


        public async Task<IActionResult> Create()
        {
            User user = new User();
            user.IsActive = 1;
            user.DepartmentList = await _userService.GetAllDepartment();
            user.RoleList = await _userService.GetAllRole();

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                user.DepartmentList = await _userService.GetAllDepartment();
                user.RoleList = await _userService.GetAllRole();
                if (ModelState.IsValid)
                {
                    
                    var result1 =  await _userManager.CreateAsync(new ApplicationUser()
                    {
                        Email = user.Email,

                    }, user.Password);

                    if (result1.Succeeded)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _userService.GetAllUser();
                       
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(user);
                    }
                }
                else
                {
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(user);
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistLoginName(int Id, string loginname)
        {
            var result = await _userService.CheckUniqueLoginName(Id, loginname);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"User: {loginname} already exist");
            }
        }
  
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _userService.FetchSingleResult(id);
            Data.RoleList = await _userService.GetAllRole();
            Data.DepartmentList = await _userService.GetAllDepartment();

            if (Data.Password == "123")
            {
               
                Data.defaultpassword = true;
            }
            else
            {
              
                Data.defaultpassword = false;
            }


            if (Data.ChangePassword == "T")
            {
                Data.ChangePasswordA = true;
            }
            else
            {
                Data.ChangePasswordA = false;
            }


            if (Data.Locked == "T")
            {
                Data.LockedA = true;
            }
            else
            {
                Data.LockedA = false;
            }





            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userService.Update(id, user);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _userService.GetAllUser();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(user);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _userService.Delete(id);
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
            var list = await _userService.GetAllUser();
            return View("Index", list);
        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _userService.FetchSingleResult(id);
            Data.DepartmentList = await _userService.GetAllDepartment();
            Data.RoleList = await _userService.GetAllRole();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _userService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }

    }
}
