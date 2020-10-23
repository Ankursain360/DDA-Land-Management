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
using Dto.Master;
using Service.IApplicationService;

namespace SiteMaster.Controllers
{
    public class UserManagementController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IDepartmentService _departmentService;
        private readonly IZoneService _zoneService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserManagementController(IUserService userService,
            IDepartmentService departmentService,
            IZoneService zoneService,
            IUserProfileService userProfileService,
            UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _departmentService = departmentService;
            _zoneService = zoneService;
            _userProfileService = userProfileService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] UserManagementSearchDto model)
        {

            var result = await _userProfileService.GetPagedUser(model);

            return PartialView("_List", result);
        }


        public async Task<IActionResult> Create()
        {
            AddUserDto model = new AddUserDto() {
                DepartmentList = await _departmentService.GetDepartment(),
                ZoneList = await _zoneService.GetZone(),
                RoleList = await _userProfileService.GetRole()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userProfileService.CreateUser(model);
                    return RedirectToAction("Index");
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
