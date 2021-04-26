
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
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
    public class NewlandnotificationdetailsController : BaseController
    {
        private readonly INewlandnotificationdetailsService _newlandnotificationdetailsService;

        public NewlandnotificationdetailsController(INewlandnotificationdetailsService newlandnotificationdetailsService)
        {
            _newlandnotificationdetailsService = newlandnotificationdetailsService;
        }
      //  [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandnotificationdetailsSearchDto model)
        {

            var result = await _newlandnotificationdetailsService.GetPagedNotifications(model);
            return PartialView("_List", result);
        }
       // [AuthorizeContext(ViewAction.Add)]
       
        public async Task<IActionResult> Create()
        {
            Newlandnotificationdetails notification = new Newlandnotificationdetails();
            notification.NotificationTypeList = await _newlandnotificationdetailsService.GetAllNotificationType();
            notification.VillageList = await _newlandnotificationdetailsService.GetAllVillage();
            notification.KhasraList = await _newlandnotificationdetailsService.GetAllKhasra(notification.VillageId);

            return View(notification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandnotificationdetails notification)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    notification.NotificationTypeList = await _newlandnotificationdetailsService.GetAllNotificationType();
                    notification.VillageList = await _newlandnotificationdetailsService.GetAllVillage();
                    notification.KhasraList = await _newlandnotificationdetailsService.GetAllKhasra(notification.VillageId);
                    notification.CreatedBy = SiteContext.UserId;

                    var result = await _newlandnotificationdetailsService.Create(notification);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                        var list = await _newlandnotificationdetailsService.GetAllNotifications();
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
       // [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandnotificationdetailsService.FetchSingleResult(id);
            Data.NotificationTypeList = await _newlandnotificationdetailsService.GetAllNotificationType();
            Data.VillageList = await _newlandnotificationdetailsService.GetAllVillage();
            Data.KhasraList = await _newlandnotificationdetailsService.GetAllKhasra(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       // [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandnotificationdetails notification)
        {
            if (ModelState.IsValid)
            {
                notification.NotificationTypeList = await _newlandnotificationdetailsService.GetAllNotificationType();
                notification.VillageList = await _newlandnotificationdetailsService.GetAllVillage();
                notification.KhasraList = await _newlandnotificationdetailsService.GetAllKhasra(notification.VillageId);
                notification.ModifiedBy = SiteContext.UserId;
                try
                {
                    var result = await _newlandnotificationdetailsService.Update(id, notification);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _newlandnotificationdetailsService.GetAllNotifications();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(notification);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(notification);

                }
            }
            return View(notification);
        }
       
      //  [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _newlandnotificationdetailsService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _newlandnotificationdetailsService.GetAllNotifications();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _newlandnotificationdetailsService.GetAllNotifications();
                return View("Index", result1);
            }
        }

       // [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandnotificationdetailsService.FetchSingleResult(id);

            Data.NotificationTypeList = await _newlandnotificationdetailsService.GetAllNotificationType();
            Data.VillageList = await _newlandnotificationdetailsService.GetAllVillage();
            Data.KhasraList = await _newlandnotificationdetailsService.GetAllKhasra(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

      //  [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Newlandnotificationdetails> result = await _newlandnotificationdetailsService.GetAllNotifications();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Newlandnotificationdetails.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _newlandnotificationdetailsService.GetAllKhasra(Convert.ToInt32(villageId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _newlandnotificationdetailsService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }
    }
}

