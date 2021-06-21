

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using SiteMaster.Filters;
using Utility.Helper;

namespace SiteMaster.Controllers
{
    public class SurveyuserdetailController : BaseController
    {
        private readonly ISurveyuserdetailService _surveyuserdetailService;

        public SurveyuserdetailController(ISurveyuserdetailService surveyuserdetailService)
        {
            _surveyuserdetailService = surveyuserdetailService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] SurveyuserdetailSearchDto model)
        {

            var result = await _surveyuserdetailService.GetPagedSurveyuserdetail(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Surveyuserdetail user  = new Surveyuserdetail();
            user.SurveyUserRoleList = await _surveyuserdetailService.GetUserDropDownList();
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Surveyuserdetail user)
        {
            user.SurveyUserRoleList = await _surveyuserdetailService.GetUserDropDownList();
            try
            {

                if (ModelState.IsValid)
                {
                    //string passwordencode = Convert.ToBase64String(Encoding.UTF8.GetBytes("user.password"));
                    //user.Password = passwordencode;
                    var result = await _surveyuserdetailService.Create(user);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                        var list = await _surveyuserdetailService.GetAllSurveyuserdetail();
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
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _surveyuserdetailService.FetchSingleResult(id);
            Data.SurveyUserRoleList = await _surveyuserdetailService.GetUserDropDownList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Surveyuserdetail user)
        {
            user.SurveyUserRoleList = await _surveyuserdetailService.GetUserDropDownList();
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _surveyuserdetailService.Update(id, user);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _surveyuserdetailService.GetAllSurveyuserdetail();
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
            return View(user);
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string UserName)
        {
            var result = await _surveyuserdetailService.CheckUniqueuserName(Id, UserName);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"User Name : {UserName} already exist");
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> PhoneNoExist(int Id, string PhoneNo)
        {
            var result = await _surveyuserdetailService.CheckUniquePhone(Id, PhoneNo);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Phone Number : {PhoneNo} already exist.");
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> EmailExist(int Id,string EmailId)
        {
            var result = await _surveyuserdetailService.CheckUniqueEmail(Id, EmailId);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email Id : {EmailId} already exist.");
            }
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _surveyuserdetailService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _surveyuserdetailService.GetAllSurveyuserdetail();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _surveyuserdetailService.GetAllSurveyuserdetail();
                return View("Index", result1);
            }
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _surveyuserdetailService.FetchSingleResult(id);
            Data.SurveyUserRoleList = await _surveyuserdetailService.GetUserDropDownList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

       
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> SurveyuserdetailList()
        {
            var result = await _surveyuserdetailService.GetAllSurveyuserdetail();
            List<SurveyuserdetailDto> data = new List<SurveyuserdetailDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new SurveyuserdetailDto()
                    {
                        Id = result[i].Id,
                        UserName = result[i].UserName,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
