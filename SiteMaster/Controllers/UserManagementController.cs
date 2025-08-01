﻿using System;
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
using Utility.Helper;
using SiteMaster.Filters;
using Core.Enum;

namespace SiteMaster.Controllers
{
    public class UserManagementController : BaseController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IDepartmentService _departmentService;
        private readonly IZoneService _zoneService;
        private readonly IBranchService _branchService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserManagementController(
            IDepartmentService departmentService,
            IZoneService zoneService,
            IBranchService branchService,
            IUserProfileService userProfileService,
            UserManager<ApplicationUser> userManager)
        {
            _departmentService = departmentService;
            _zoneService = zoneService;
            _branchService = branchService;
            _userProfileService = userProfileService;
            _userManager = userManager;
        }
        [AuthorizeContext(ViewAction.View)]
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

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            AddUserDto model = new AddUserDto()
            {
                DepartmentList = await _departmentService.GetDepartment(),
             
                RoleList = await _userProfileService.GetRole(),
               
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(AddUserDto model)
        {
            if (ModelState.IsValid)
            {
               var result =  await _userProfileService.CreateUser(model);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.ValidationFailed, "", AlertType.Warning);
                    return View("Create");
                }
                
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
                return Json($"This Username is already Linked with Another Account. Kindly use different Username");
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistEmail(int Id, string email)
        {
            var result = await _userProfileService.ValidateUniqueEmail(Id, email);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"This Email is already Linked with Another Account. Kindly use different Email");
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistPhoneNumber(int Id, string phonenumber)
        {
            var result = await _userProfileService.ValidateUniquePhone(Id, phonenumber);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"This Phone Number is already Linked with Another Account. Kindly use different Phone Number");
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userProfileService.GetUserById(id);
            EditUserDto model = new EditUserDto()
            { 
            };
            return View(model);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(EditUserDto model)
        {
            if (ModelState.IsValid)
            {
                var result =  await _userProfileService.UpdateUser(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            await _userProfileService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }


        [AuthorizeContext(ViewAction.View)]
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
            return Json(await _userProfileService.GetAllZone(Convert.ToInt32(DepartmentId)));
            
        }
        [HttpGet]
        public async Task<JsonResult> GetBranchList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _branchService.GetGetBranchList(Convert.ToInt32(DepartmentId)));

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
                RoleList = await _userProfileService.GetRole(),
                DepartmentId = user.DepartmentId,
                BranchList = await _branchService.GetGetBranchList(Convert.ToInt32(user.DepartmentId)),
                ZoneList = await _userProfileService.GetAllZone(Convert.ToInt32(user.DepartmentId)),
                BranchId = user.BranchId,
                RoleId = user.RoleId,
                DistrictId = user.DistrictId,
                ZoneId = user.ZoneId
            };
            return PartialView("_UserProfileInfo", model);

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonalDetails([FromBody] UserPersonalInfoDto model)
        {
            List<string> JsonMsg = new List<string>();
            if (ModelState.IsValid)
            {
                var IsUsernameExist = await _userProfileService.ValidateUniqueUserName(model.Id, model.UserName);
                if (IsUsernameExist)
                {
                    JsonMsg.Add("false");
                    JsonMsg.Add("This Username is already Linked with Another Account. Kindly use different Username");
                    return Json(JsonMsg);
                }
                var IsEmailExist = await _userProfileService.ValidateUniqueEmail(model.Id, model.Email);
                if (IsEmailExist)
                {
                    JsonMsg.Add("false");
                    JsonMsg.Add("This Email Id is already Linked with Another Account. Kindly use different Email Id");
                    return Json(JsonMsg);
                }
                var IsPhoneExist = await _userProfileService.ValidateUniquePhone(model.Id, model.PhoneNumber);
                if (IsPhoneExist)
                {
                    JsonMsg.Add("false");
                    JsonMsg.Add("This Phone Number is already Linked with Another Account. Kindly use different Phone Number");
                    return Json(JsonMsg);
                }
                await _userProfileService.UpdateUserPersonalDetails(model);
                JsonMsg.Add("true");
                return Json(JsonMsg);
                //return Json(Url.Action("Index", "UserManagement"));
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



        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> UserList()
        {
            var result = await _userProfileService.GetAllUser();
            List<UserListDto> data = new List<UserListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new UserListDto()
                    {
                        Id = result[i].Id,
                        UserName= result[i].User == null ? "" : result[i].User.UserName,
                        Name = result[i].User == null ? "" : result[i].User.Name,
                        Role = result[i].Role == null ? "" : result[i].Role.Name,
                        EmailId = result[i].User == null ? "" : result[i].User.Email,
                        ContactNumber = result[i].User == null ? "" : result[i].User.PhoneNumber,
                        Zone = result[i].Zone == null ? "" : result[i].Zone.Name,
                        District = result[i].District == null ? "" : result[i].District.Name,
                        Department = result[i].Department == null ? "" : result[i].Department.Name,
                       
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }


    }
}
