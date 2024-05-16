using System;
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
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Microsoft.AspNetCore.Http;

namespace AcquiredLandInformationManagement.Controllers
{
    public class Undersection17plotdetailController : Controller
    {
        private readonly IUndersection17plotdetailService _undersection17plotdetailService;


        public Undersection17plotdetailController(IUndersection17plotdetailService undersection17plotdetailService)
        {
            _undersection17plotdetailService = undersection17plotdetailService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _undersection17plotdetailService.GetAllUndersection17plotdetail();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Undersection17plotdetailSearchDto model)
        {
            var result = await _undersection17plotdetailService.GetPagedUndersection17plotdetail(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()

        {
            Undersection17plotdetail undersection17plotdetail = new Undersection17plotdetail();
            undersection17plotdetail.IsActive = 1;
            undersection17plotdetail.VillageId = null;

            undersection17plotdetail.KhasraList = await _undersection17plotdetailService.BindKhasra(undersection17plotdetail.VillageId);
            undersection17plotdetail.VillageList = await _undersection17plotdetailService.GetAllVillageList();
            undersection17plotdetail.Undersection17List = await _undersection17plotdetailService.GetAllUndersection17List();
            return View(undersection17plotdetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Undersection17plotdetail undersection17Plotdetail)
        {
            try
            {
                
                undersection17Plotdetail.Undersection17List = await _undersection17plotdetailService.GetAllUndersection17List();
                undersection17Plotdetail.KhasraList = await _undersection17plotdetailService.BindKhasra(undersection17Plotdetail.VillageId);
                undersection17Plotdetail.VillageList = await _undersection17plotdetailService.GetAllVillageList();

                if (ModelState.IsValid)
                {
                    var result = await _undersection17plotdetailService.Create(undersection17Plotdetail);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection17plotdetailService.GetAllUndersection17plotdetail();
                        return View("Create", undersection17Plotdetail);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection17Plotdetail);
                    }
                }
                else
                {
                    return View(undersection17Plotdetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection17Plotdetail);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection17plotdetailService.FetchSingleResult(id);
            Data.Undersection17List = await _undersection17plotdetailService.GetAllUndersection17List();
            Data.KhasraList = await _undersection17plotdetailService.BindKhasra(Data.VillageId);
            Data.VillageList = await _undersection17plotdetailService.GetAllVillageList();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Undersection17plotdetail undersection17plotdetail)
        {
            undersection17plotdetail.Undersection17List = await _undersection17plotdetailService.GetAllUndersection17List();
            undersection17plotdetail.KhasraList = await _undersection17plotdetailService.BindKhasra(undersection17plotdetail.VillageId);
            undersection17plotdetail.VillageList = await _undersection17plotdetailService.GetAllVillageList();
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _undersection17plotdetailService.Update(id, undersection17plotdetail);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection17plotdetailService.GetAllUndersection17plotdetail();
                        return View("Edit", undersection17plotdetail);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection17plotdetail);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(undersection17plotdetail);
                }
            }
            else
            {
                return View(undersection17plotdetail);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _undersection17plotdetailService.Delete(id);
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
            var list = await _undersection17plotdetailService.GetAllUndersection17plotdetail();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection17plotdetailService.FetchSingleResult(id);

            Data.Undersection17List = await _undersection17plotdetailService.GetAllUndersection17List();
             Data.KhasraList = await _undersection17plotdetailService.BindKhasra(Data.VillageId);
            Data.VillageList = await _undersection17plotdetailService.GetAllVillageList();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _undersection17plotdetailService.BindKhasra(Convert.ToInt32(villageId)));
        }


     


        [HttpGet]
        public async Task<JsonResult> GetAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _undersection17plotdetailService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }

        public async Task<PartialViewResult> Notification4View([FromBody] NotificationList17SearchDto model)
        {
            var Data = await _undersection17plotdetailService.GetAllNotificationList(model);

            return PartialView("_ListNotification", Data);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Undersection17PlotList([FromBody] Undersection17plotdetailSearchDto model)
        {
            var result = await _undersection17plotdetailService.GetAllUndersection17plotdetailList(model);
            List<Undersection17PlotListDto> data = new List<Undersection17PlotListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new Undersection17PlotListDto()
                    {
                        Id = result[i].Id,
                        NotificationUS17 = result[i].UnderSection17 == null ? "" : result[i].UnderSection17.Number,
                        Village = result[i].Acquiredlandvillage == null ? "" : result[i].Acquiredlandvillage.Name,
                        Khasra = result[i].Khasra == null ? "" : result[i].Khasra.Name,
                        ActualArea = result[i].Khasra.Bigha.ToString()
                                  + '-' + result[i].Khasra.Biswa.ToString()
                                  + '-' + result[i].Khasra.Biswanshi.ToString(),
                        NotifyArea = result[i].Bigha.ToString()
                                  + '-' + result[i].Biswa.ToString()
                                  + '-' + result[i].Biswanshi.ToString(),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            //var memory = ExcelHelper.CreateExcel(data);
            //TempData["file"] = memory;
            //return Ok();
            var memory = ExcelHelper.CreateExcel(data);
            HttpContext.Session.Set("file", memory);
            return Ok();
        }

        [HttpGet]
        public virtual ActionResult download()
        {
            //byte[] data = TempData["file"] as byte[];
            //return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            byte[] data = HttpContext.Session.Get("file") as byte[];
            HttpContext.Session.Remove("file");
            // var dem = Decompress(data);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
