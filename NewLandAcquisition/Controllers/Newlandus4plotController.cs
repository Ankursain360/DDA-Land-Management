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
    public class Newlandus4plotController : BaseController
    {
        private readonly INewlandus4plotService _newlandus4plotService;
        public Newlandus4plotController(INewlandus4plotService newlandus4plotService)
        {
            _newlandus4plotService = newlandus4plotService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Newlandus4plotSearchDto model)
        {
            var result = await _newlandus4plotService.GetPagedUS4Plot(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandus4plot us4plot = new Newlandus4plot();
            us4plot.NotificationList = await _newlandus4plotService.GetAllNotification();
            us4plot.VillageList = await _newlandus4plotService.GetAllVillage();
            us4plot.KhasraList = await _newlandus4plotService.GetAllKhasra(us4plot.VillageId);

            return View(us4plot);
        }

        [HttpPost]
       
      [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandus4plot us4plot)
        {
            us4plot.NotificationList = await _newlandus4plotService.GetAllNotification();
            us4plot.VillageList = await _newlandus4plotService.GetAllVillage();
            us4plot.KhasraList = await _newlandus4plotService.GetAllKhasra(us4plot.VillageId);

            if (ModelState.IsValid)
                {
                    us4plot.CreatedBy = SiteContext.UserId;
                    var result = await _newlandus4plotService.Create(us4plot);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newlandus4plotService.GetAllUS4Plot();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(us4plot);

                    }
                }
                else
                {
                    return View(us4plot);
                }
           
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandus4plotService.FetchSingleResult(id);
            Data.NotificationList = await _newlandus4plotService.GetAllNotification();
            Data.VillageList = await _newlandus4plotService.GetAllVillage();
            Data.KhasraList = await _newlandus4plotService.GetAllKhasra(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandus4plot us4plot)
        {
            us4plot.NotificationList = await _newlandus4plotService.GetAllNotification();
            us4plot.VillageList = await _newlandus4plotService.GetAllVillage();
            us4plot.KhasraList = await _newlandus4plotService.GetAllKhasra(us4plot.VillageId);

            if (ModelState.IsValid)
            {
                us4plot.ModifiedBy = SiteContext.UserId;
                var result = await _newlandus4plotService.Update(id, us4plot);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _newlandus4plotService.GetAllUS4Plot();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(us4plot);

                    }
               
            }
            return View(us4plot);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandus4plotService.FetchSingleResult(id);
            Data.NotificationList = await _newlandus4plotService.GetAllNotification();
            Data.VillageList = await _newlandus4plotService.GetAllVillage();
            Data.KhasraList = await _newlandus4plotService.GetAllKhasra(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _newlandus4plotService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }

            var list = await _newlandus4plotService.GetAllUS4Plot();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Newlandus4plot> result = await _newlandus4plotService.GetAllUS4Plot();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Newlandus4plot.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _newlandus4plotService.GetAllKhasra(Convert.ToInt32(villageId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _newlandus4plotService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }
    }
}
