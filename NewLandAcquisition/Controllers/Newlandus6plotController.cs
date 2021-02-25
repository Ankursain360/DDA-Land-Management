using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using NewLandAcquisition.Filters;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;

namespace NewLandAcquisition.Controllers
{
    public class Newlandus6plotController : BaseController
    {
        private readonly INewlandus6plotService _newlandus6plotService;
        public Newlandus6plotController(INewlandus6plotService newlandus6plotService)
        {
            _newlandus6plotService = newlandus6plotService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Newlandus6plotSearchDto model)
        {
            var result = await _newlandus6plotService.GetPagedUS6Plot(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandus6plot us4plot = new Newlandus6plot();
            us4plot.NotificationList = await _newlandus6plotService.GetAllNotification();
            us4plot.VillageList = await _newlandus6plotService.GetAllVillage();
            us4plot.KhasraList = await _newlandus6plotService.GetAllKhasra(us4plot.VillageId);

            return View(us4plot);
        }

        [HttpPost]

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandus6plot us6plot)
        {
            us6plot.NotificationList = await _newlandus6plotService.GetAllNotification();
            us6plot.VillageList = await _newlandus6plotService.GetAllVillage();
            us6plot.KhasraList = await _newlandus6plotService.GetAllKhasra(us6plot.VillageId);

            if (ModelState.IsValid)
            {
                us6plot.CreatedBy = SiteContext.UserId;
                var result = await _newlandus6plotService.Create(us6plot);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var list = await _newlandus6plotService.GetAllUS6Plot();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(us6plot);

                }
            }
            else
            {
                return View(us6plot);
            }

        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandus6plotService.FetchSingleResult(id);
            Data.NotificationList = await _newlandus6plotService.GetAllNotification();
            Data.VillageList = await _newlandus6plotService.GetAllVillage();
            Data.KhasraList = await _newlandus6plotService.GetAllKhasra(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandus6plot us6plot)
        {
            us6plot.NotificationList = await _newlandus6plotService.GetAllNotification();
            us6plot.VillageList = await _newlandus6plotService.GetAllVillage();
            us6plot.KhasraList = await _newlandus6plotService.GetAllKhasra(us6plot.VillageId);

            if (ModelState.IsValid)
            {
                us6plot.ModifiedBy = SiteContext.UserId;
                var result = await _newlandus6plotService.Update(id, us6plot);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _newlandus6plotService.GetAllUS6Plot();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(us6plot);

                }

            }
            return View(us6plot);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandus6plotService.FetchSingleResult(id);
            Data.NotificationList = await _newlandus6plotService.GetAllNotification();
            Data.VillageList = await _newlandus6plotService.GetAllVillage();
            Data.KhasraList = await _newlandus6plotService.GetAllKhasra(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _newlandus6plotService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }

            var list = await _newlandus6plotService.GetAllUS6Plot();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Newlandus6plot> result = await _newlandus6plotService.GetAllUS6Plot();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Newlandus6plot.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _newlandus6plotService.GetAllKhasra(Convert.ToInt32(villageId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _newlandus6plotService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }
    }
}
