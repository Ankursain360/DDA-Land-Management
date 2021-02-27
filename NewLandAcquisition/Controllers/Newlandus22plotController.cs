using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using NewLandAcquisition.Filters;
using Microsoft.AspNetCore.Authorization;
using Utility.Helper;
using Core.Enum;

namespace NewLandAcquisition.Controllers
{
    public class Newlandus22plotController : BaseController
    {
        private readonly INewlandus22plotService _newlandus22plotService;
        public Newlandus22plotController(INewlandus22plotService newlandus22plotService)
        {
            _newlandus22plotService = newlandus22plotService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Newlandus22plotSearchDto model)
        {
            var result = await _newlandus22plotService.GetPagedUS22Plot(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandus22plot us22plot = new Newlandus22plot();
            us22plot.NotificationList = await _newlandus22plotService.GetAllNotification();
            us22plot.VillageList = await _newlandus22plotService.GetAllVillage();
            us22plot.KhasraList = await _newlandus22plotService.GetAllKhasra(us22plot.VillageId);
            us22plot.Us4List = await _newlandus22plotService.GetAllUS4Plot(us22plot.NotificationId);
            us22plot.Us6List = await _newlandus22plotService.GetAllUS6Plot(us22plot.NotificationId);
            us22plot.Us17List = await _newlandus22plotService.GetAllUS17Plot(us22plot.NotificationId);
            return View(us22plot);
        }

        [HttpPost]

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandus22plot us22plot)
        {
            us22plot.NotificationList = await _newlandus22plotService.GetAllNotification();
            us22plot.VillageList = await _newlandus22plotService.GetAllVillage();
            us22plot.KhasraList = await _newlandus22plotService.GetAllKhasra(us22plot.VillageId);

            us22plot.Us4List = await _newlandus22plotService.GetAllUS4Plot(us22plot.NotificationId);
            us22plot.Us6List = await _newlandus22plotService.GetAllUS6Plot(us22plot.NotificationId);
            us22plot.Us17List = await _newlandus22plotService.GetAllUS17Plot(us22plot.NotificationId);
            if (ModelState.IsValid)
            {
                us22plot.CreatedBy = SiteContext.UserId;
                var result = await _newlandus22plotService.Create(us22plot);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var list = await _newlandus22plotService.GetAllUS22Plot();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(us22plot);

                }
            }
            else
            {
                return View(us22plot);
            }

        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandus22plotService.FetchSingleResult(id);
            Data.NotificationList = await _newlandus22plotService.GetAllNotification();
            Data.VillageList = await _newlandus22plotService.GetAllVillage();
            Data.KhasraList = await _newlandus22plotService.GetAllKhasra(Data.VillageId);
            Data.Us4List = await _newlandus22plotService.GetAllUS4Plot(Data.NotificationId);
            Data.Us6List = await _newlandus22plotService.GetAllUS6Plot(Data.NotificationId);
            Data.Us17List = await _newlandus22plotService.GetAllUS17Plot(Data.NotificationId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandus22plot us22plot)
        {
            us22plot.NotificationList = await _newlandus22plotService.GetAllNotification();
            us22plot.VillageList = await _newlandus22plotService.GetAllVillage();
            us22plot.KhasraList = await _newlandus22plotService.GetAllKhasra(us22plot.VillageId);
            us22plot.Us4List = await _newlandus22plotService.GetAllUS4Plot(us22plot.NotificationId);
            us22plot.Us6List = await _newlandus22plotService.GetAllUS6Plot(us22plot.NotificationId);
            us22plot.Us17List = await _newlandus22plotService.GetAllUS17Plot(us22plot.NotificationId);
            if (ModelState.IsValid)
            {
                us22plot.ModifiedBy = SiteContext.UserId;
                var result = await _newlandus22plotService.Update(id, us22plot);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _newlandus22plotService.GetAllUS22Plot();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(us22plot);

                }

            }
            return View(us22plot);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandus22plotService.FetchSingleResult(id);
            Data.NotificationList = await _newlandus22plotService.GetAllNotification();
            Data.VillageList = await _newlandus22plotService.GetAllVillage();
            Data.KhasraList = await _newlandus22plotService.GetAllKhasra(Data.VillageId);
            Data.Us4List = await _newlandus22plotService.GetAllUS4Plot(Data.NotificationId);
            Data.Us6List = await _newlandus22plotService.GetAllUS6Plot(Data.NotificationId);
            Data.Us17List = await _newlandus22plotService.GetAllUS17Plot(Data.NotificationId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _newlandus22plotService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }

            var list = await _newlandus22plotService.GetAllUS22Plot();
            return View("Index", list);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Newlandus22plot> result = await _newlandus22plotService.GetAllUS22Plot();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Newlandus22plot.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _newlandus22plotService.GetAllKhasra(Convert.ToInt32(villageId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _newlandus22plotService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }

        [HttpGet]
        public async Task<JsonResult> GetAllUS4Plot(int? notificationId)
        {
            notificationId = notificationId ?? 0;

            return Json(await _newlandus22plotService.GetAllUS4Plot(Convert.ToInt32(notificationId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllUS6Plot(int? notificationId)
        {
            notificationId = notificationId ?? 0;

            return Json(await _newlandus22plotService.GetAllUS6Plot(Convert.ToInt32(notificationId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetAllUS17Plot(int? notificationId)
        {
            notificationId = notificationId ?? 0;

            return Json(await _newlandus22plotService.GetAllUS17Plot(Convert.ToInt32(notificationId)));
        }

    }
}
