using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Utility.Helper;
using System.Collections.Generic;
namespace SiteMaster.Controllers
{
    public class ApplicationNotificationTemplateController : BaseController
    {
        private readonly IApplicationNotificationTemplateService _applicationNotificationService;

        public ApplicationNotificationTemplateController(IApplicationNotificationTemplateService applicationNotificationService)
        {
            _applicationNotificationService = applicationNotificationService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ApplicationNotificationTemplateSearchDto model)
        {
            var result = await _applicationNotificationService.GetPagedTemplate(model);
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
        public async Task<IActionResult> Create(ApplicationNotificationTemplate templates)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    templates.UserNotificationGuid = Guid.NewGuid().ToString();
                    templates.CreatedDate = DateTime.Now;
                    templates.CreatedBy = SiteContext.UserId;
                    var result = await _applicationNotificationService.Create(templates);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                        var list = await _applicationNotificationService.GetAllTemplate();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(templates);

                    }
                }
                else
                {
                    return View(templates);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(templates);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _applicationNotificationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, ApplicationNotificationTemplate updatetemplate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    updatetemplate.ModifiedBy = SiteContext.UserId;
                    updatetemplate.ModifiedDate = DateTime.Now;
                    var result = await _applicationNotificationService.Update(id, updatetemplate);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _applicationNotificationService.GetAllTemplate();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(updatetemplate);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(updatetemplate);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _applicationNotificationService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Template Name : {Name} already exist");
            }
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  //Not in use
        {

            var result = await _applicationNotificationService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _applicationNotificationService.GetAllTemplate();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _applicationNotificationService.GetAllTemplate();
                return View("Index", result1);
            }
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {

            var result = await _applicationNotificationService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _applicationNotificationService.GetAllTemplate();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _applicationNotificationService.GetAllTemplate();
                return View("Index", result1);
            }

        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _applicationNotificationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        public async Task<IActionResult> Download()
        {
            List<ApplicationNotificationTemplate> result = await _applicationNotificationService.GetAllTemplate();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Notificationtemplate.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
    }
}
