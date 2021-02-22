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

namespace NewLandAcquisition.Controllers
{
    public class Newlandus17plotController : BaseController
    {
        private readonly INewlandus17plotService _newlandus17plotService;
        public Newlandus17plotController(INewlandus17plotService newlandus17plotService)
        {
            _newlandus17plotService = newlandus17plotService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Newlandus17plotSearchDto model)
        {
            var result = await _newlandus17plotService.GetPagedUS17Plot(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create()
        {
            Newlandus17plot us17plot = new Newlandus17plot();
            us17plot.NotificationList = await _newlandus17plotService.GetAllNotification();
            us17plot.VillageList = await _newlandus17plotService.GetAllVillage();
            us17plot.KhasraList = await _newlandus17plotService.GetAllKhasra(us17plot.VillageId);

            return View(us17plot);
        }

        [HttpPost]

        //[AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandus17plot us17plot)
        {
            us17plot.NotificationList = await _newlandus17plotService.GetAllNotification();
            us17plot.VillageList = await _newlandus17plotService.GetAllVillage();
            us17plot.KhasraList = await _newlandus17plotService.GetAllKhasra(us17plot.VillageId);

            if (ModelState.IsValid)
            {
                us17plot.CreatedBy = SiteContext.UserId;
                var result = await _newlandus17plotService.Create(us17plot);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var list = await _newlandus17plotService.GetAllUS17Plot();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(us17plot);

                }
            }
            else
            {
                return View(us17plot);
            }

        }
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandus17plotService.FetchSingleResult(id);
            Data.NotificationList = await _newlandus17plotService.GetAllNotification();
            Data.VillageList = await _newlandus17plotService.GetAllVillage();
            Data.KhasraList = await _newlandus17plotService.GetAllKhasra(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Newlandus17plot us17plot)
        {
            us17plot.NotificationList = await _newlandus17plotService.GetAllNotification();
            us17plot.VillageList = await _newlandus17plotService.GetAllVillage();
            us17plot.KhasraList = await _newlandus17plotService.GetAllKhasra(us17plot.VillageId);

            if (ModelState.IsValid)
            {
                us17plot.ModifiedBy = SiteContext.UserId;
                var result = await _newlandus17plotService.Update(id, us17plot);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _newlandus17plotService.GetAllUS17Plot();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(us17plot);

                }

            }
            return View(us17plot);
        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandus17plotService.FetchSingleResult(id);
            Data.NotificationList = await _newlandus17plotService.GetAllNotification();
            Data.VillageList = await _newlandus17plotService.GetAllVillage();
            Data.KhasraList = await _newlandus17plotService.GetAllKhasra(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var result = await _newlandus17plotService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }

            var list = await _newlandus17plotService.GetAllUS17Plot();
            return View("Index", list);
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _newlandus17plotService.GetAllKhasra(Convert.ToInt32(villageId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _newlandus17plotService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }
    }
}
