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

namespace SiteMaster.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserService _userService;
        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _userService.GetAllUser();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            User user = new User();
            user.IsActive = 1;
            user.DistrictList = await _userService.GetAllDistrict();
            user.RoleList = await _userService.GetAllRole();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                user.DistrictList = await _userService.GetAllDistrict();
                user.RoleList = await _userService.GetAllRole();
                if (ModelState.IsValid)
                {
                    if (user.defaultpassword == true)
                    {
                        user.Password = "123";


                    }


                    if (user.ChangePasswordA == true)
                    {
                        user.ChangePassword = "T";


                    }

                    else
                    {
                        user.ChangePassword = "F";

                    }

                    if (user.LockedA == true)
                    {
                        user.Locked = "T";


                    }

                    else
                    {
                        user.Locked = "F";

                    }

                    

                        var result = await _userService.Create(user);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _userService.GetAllUser();
                      //  var list1 = await _userService.GetAllRole();
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
        public async Task<IActionResult> Exist(int Id, string loginname)
        {
            var result = await _userService.CheckUniqueName(Id, loginname);
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
            Data.DistrictList = await _userService.GetAllDistrict();
            Data.RoleList = await _userService.GetAllRole();

          
            Data.DistrictList = await _userService.GetAllDistrict();
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
            Data.DistrictList = await _userService.GetAllDistrict();
            Data.RoleList = await _userService.GetAllRole();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}



