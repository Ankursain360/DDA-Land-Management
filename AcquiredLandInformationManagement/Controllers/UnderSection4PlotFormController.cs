using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Master;
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

namespace AcquiredLandInformationManagement.Controllers
{
    public class UnderSection4PlotFormController : Controller
    {

        private readonly IUndersection4PlotService _undersection4PlotService;


        public UnderSection4PlotFormController(IUndersection4PlotService undersection4PlotService)
        {
            _undersection4PlotService = undersection4PlotService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _undersection4PlotService.GetAllUndersection4Plot();
            return View(list);
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NotificationUndersection4plotDto model)
        {
            var result = await _undersection4PlotService.GetPagedNoUndersection4plot(model);

            return PartialView("_List", result);
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Undersection4plot undersection4plot = new Undersection4plot();
            undersection4plot.IsActive = 1;
            undersection4plot.NotificationList = await _undersection4PlotService.GetAllNotificationNo();
            undersection4plot.KhasraList = await _undersection4PlotService.BindKhasra(undersection4plot.VillageId);
            undersection4plot.VillageList = await _undersection4PlotService.GetAllVillage();

            return View(undersection4plot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Undersection4plot undersection4plot)
        {
            try
            {
                undersection4plot.NotificationList = await _undersection4PlotService.GetAllNotificationNo();
               undersection4plot.KhasraList = await _undersection4PlotService.BindKhasra(undersection4plot.VillageId);
                undersection4plot.VillageList = await _undersection4PlotService.GetAllVillage();


                if (ModelState.IsValid)
                {
                    var result = await _undersection4PlotService.Create(undersection4plot);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection4PlotService.GetAllUndersection4Plot();
                         return View("Create", undersection4plot);
                       // return Redirect("Create");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection4plot);
                    }
                }
                else
                {
                    return View(undersection4plot);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection4plot);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection4PlotService.FetchSingleResult(id);

           
            Data.NotificationList = await _undersection4PlotService.GetAllNotificationNo();
            Data.KhasraList = await _undersection4PlotService.BindKhasra(Data.VillageId);
            Data.VillageList = await _undersection4PlotService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Undersection4plot undersection4plot)
        {

            undersection4plot.NotificationList = await _undersection4PlotService.GetAllNotificationNo();
            undersection4plot.KhasraList = await _undersection4PlotService.BindKhasra(undersection4plot.VillageId);
            undersection4plot.VillageList = await _undersection4PlotService.GetAllVillage();

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _undersection4PlotService.Update(id, undersection4plot);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection4PlotService.GetAllUndersection4Plot();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection4plot);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(undersection4plot);
                }
            }
            else
            {
                return View(undersection4plot);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _undersection4PlotService.Delete(id);
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
            var list = await _undersection4PlotService.GetAllUndersection4Plot();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection4PlotService.FetchSingleResult(id);
            Data.NotificationList = await _undersection4PlotService.GetAllNotificationNo();
            Data.KhasraList = await _undersection4PlotService.BindKhasra(Data.VillageId);
            Data.VillageList = await _undersection4PlotService.GetAllVillage();


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
            return Json(await _undersection4PlotService.BindKhasra(Convert.ToInt32(villageId)));
        }


        [HttpGet]
        public async Task<JsonResult> GetAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;
          
            return Json(await _undersection4PlotService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }




        public async Task<PartialViewResult> Notification4View([FromBody] NotificationList4SearchDto model)
        {
            var Data = await _undersection4PlotService.GetAllNotificationList(model);

            return PartialView("_ListNotification", Data);
        }




        [AuthorizeContext(ViewAction.Download)]


        public async Task<IActionResult> Undersection4plotList()
        {
            var result = await _undersection4PlotService.GetAllUndersection4Plot();
            List<Undersection4plotListDto> data = new List<Undersection4plotListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new Undersection4plotListDto()
                    {
                        Id = result[i].Id,
                        NotificationNo = result[i].UnderSection4 == null ? "" : result[i].UnderSection4.Number,
                        VillageName = result[i].Village == null ? "" : result[i].Village.Name,
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name,
                        ActualArea = result[i].Khasra.Bigha
                                  + '-' + result[i].Khasra.Biswa
                                  + '-' + result[i].Khasra.Biswanshi,
                        Area = result[i].Bigha.ToString()
                                  + '-' + result[i].Biswa.ToString()
                                  + '-' + result[i].Biswanshi.ToString(),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }




    }
}