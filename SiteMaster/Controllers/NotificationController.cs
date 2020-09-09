using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using SiteMaster.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;

namespace SiteMaster.Controllers
{
    public class NotificationController : Controller
    {

        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _notificationService.GetAllNotification();
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }

       

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _notificationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _notificationService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Notification: {Name} already exist");
            }
        }


        public async Task<IActionResult> Delete(int id)  //Not in use
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _notificationService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }

            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View(form);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            //try
            //{

            var result = await _notificationService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _notificationService.GetAllNotification();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _notificationService.GetAllNotification();
                return View("Index", result1);
            }

            //}
            //catch(Exception ex)
            //{
            //    ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Something went wrong", Status = "S", BackPageAction = "Index", BackPageController = "Notification" };
            //    return View();
            //}

        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _notificationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }

}

