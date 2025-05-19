using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using SiteMaster.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using System.Collections.Generic;
using Utility.Helper;
using Dto.Master;

namespace SiteMaster.Controllers
{ 
    public class NewLandNotificationTypeController : BaseController
    {
        private readonly INewLandNotificationTypeService _newLandNotificationTypeService;

        public NewLandNotificationTypeController(INewLandNotificationTypeService newLandNotificationTypeService)
        {
            _newLandNotificationTypeService = newLandNotificationTypeService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewLandNotificationTypeSearchDto model)
        {
            var result = await _newLandNotificationTypeService.GetPagedNotification(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(NewlandNotificationtype notification)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _newLandNotificationTypeService.Create(notification);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newLandNotificationTypeService.GetAllNotificationType();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(notification);

                    }
                }
                else
                {
                    return View(notification);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(notification);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newLandNotificationTypeService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, NewlandNotificationtype notification)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _newLandNotificationTypeService.Update(id, notification);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _newLandNotificationTypeService.GetAllNotificationType();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(notification);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(notification);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            var result = await _newLandNotificationTypeService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _newLandNotificationTypeService.GetAllNotificationType();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _newLandNotificationTypeService.GetAllNotificationType();
                return View("Index", result1);
            }
        }

       // [HttpPost]
       [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _newLandNotificationTypeService.Delete(id);
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
            var list = await _newLandNotificationTypeService.GetAllNotificationType();
            return View("Index", list);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newLandNotificationTypeService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> NotificationList()
        {
            var result = await _newLandNotificationTypeService.GetAllNotificationType();
            List<NotificationListDto> data = new List<NotificationListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NotificationListDto()
                    {
                        Id = result[i].Id,
                        Notification = result[i].NotificationType,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
