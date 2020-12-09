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
using Dto.Search;

namespace SiteMaster.Controllers
{
    public class UserManagementController : BaseController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IDepartmentService _departmentService;
        private readonly IZoneService _zoneService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserManagementController(
            IDepartmentService departmentService,
            IZoneService zoneService,
            IUserProfileService userProfileService,
            UserManager<ApplicationUser> userManager)
        {
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
            AddUserDto model = new AddUserDto()
            {
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

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistLoginName(int Id, string UserName)
        {
            var result = await _userProfileService.ValidateUniqueUserName(Id, UserName);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"User: {UserName} already exist");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userProfileService.GetUserById(id);
            EditUserDto model = new EditUserDto()
            { 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserDto model)
        {
            if (ModelState.IsValid)
            {
                await _userProfileService.UpdateUser(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _userProfileService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> View(int id)
        {
            var user = await _userProfileService.GetUserById(id);
            EditUserDto model = new EditUserDto()
            {
            };
            return View(model);
        }
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            //return Json(await _userService.GetAllZone(Convert.ToInt32(DepartmentId)));
            return Json("");
        }

        [HttpPost]
        public async Task<PartialViewResult> LoadPersonalDetails([FromBody] UsermanagementEditPartialLoad dtodata)
        {
            var user = await _userProfileService.GetUserById(dtodata.id);
            UserPersonalInfoDto model = new UserPersonalInfoDto()
            {
                Id = user.User.Id,
                Email = user.User.Email,
                Name = user.User.Name,
                UserName = user.User.UserName,
                PhoneNumber = user.User.PhoneNumber
            };
            return PartialView("_UserPersonalInfo", model);

        }

        [HttpPost]
        public async Task<PartialViewResult> LoadProfileDetails([FromBody] UsermanagementEditPartialLoad dtodata)
        {
            var user = await _userProfileService.GetUserById(dtodata.id);
            UserProfileInfoDto model = new UserProfileInfoDto()
            {
                DepartmentList = await _departmentService.GetDepartment(),
                ZoneList = await _zoneService.GetZone(),
                RoleList = await _userProfileService.GetRole(),
                DepartmentId = user.DepartmentId,
                RoleId = user.RoleId,
                DistrictId = user.DistrictId,
                ZoneId = user.ZoneId
            };
            return PartialView("_UserProfileInfo", model);

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonalDetails([FromBody] UserPersonalInfoDto model)
        {
            if (ModelState.IsValid)
            {
                await _userProfileService.UpdateUserPersonalDetails(model);
                return Json(Url.Action("Index", "UserManagement"));
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfileDetails([FromBody] UserProfileEditDto model)
        {
            if (ModelState.IsValid)
            {
                await _userProfileService.UpdateUserProfileDetails(model);
                return Json(Url.Action("Index", "UserManagement"));
            }
            else
            {
                return View(model);
            }
        }

    }
}
