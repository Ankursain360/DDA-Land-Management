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
using Core.Enum;
using AcquiredLandInformationManagement.Filters;
using Utility.Helper;

namespace AcquiredLandInformationManagement.Controllers
{
    public class UnderSection6plotDetails : Controller
    {
        private readonly IUndersection6plotService _undersection6plotservice;

        public UnderSection6plotDetails(IUndersection6plotService undersection6plotservice)
        {
            _undersection6plotservice = undersection6plotservice;
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NotificationUndersection6plotDto model)
        {
            var result = await _undersection6plotservice.GetPagedNoUndersection6plot(model);

            return PartialView("_List", result);
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Undersection6plot undersection4plot = new Undersection6plot();
            undersection4plot.IsActive = 1;
            undersection4plot.NotificationList = await _undersection6plotservice.GetAllNotificationNo();
            undersection4plot.KhasraList = await _undersection6plotservice.BindKhasra(undersection4plot.VillageId);
            undersection4plot.VillageList = await _undersection6plotservice.GetAllVillage();

            return View(undersection4plot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Undersection6plot undersection4plot)
        {
            try
            {
                undersection4plot.NotificationList = await _undersection6plotservice.GetAllNotificationNo();
                undersection4plot.KhasraList = await _undersection6plotservice.BindKhasra(undersection4plot.VillageId);
                undersection4plot.VillageList = await _undersection6plotservice.GetAllVillage();


                if (ModelState.IsValid)
                {
                    var result = await _undersection6plotservice.Create(undersection4plot);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection6plotservice.GetAllUndersection6Plot();
                        return View("Create", undersection4plot);
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
            var Data = await _undersection6plotservice.FetchSingleResult(id);


            Data.NotificationList = await _undersection6plotservice.GetAllNotificationNo();
            Data.KhasraList = await _undersection6plotservice.BindKhasra(Data.VillageId);
            Data.VillageList = await _undersection6plotservice.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Undersection6plot undersection4plot)
        {

            undersection4plot.NotificationList = await _undersection6plotservice.GetAllNotificationNo();
            undersection4plot.KhasraList = await _undersection6plotservice.BindKhasra(undersection4plot.VillageId);
            undersection4plot.VillageList = await _undersection6plotservice.GetAllVillage();


            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _undersection6plotservice.Update(id, undersection4plot);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection6plotservice.GetAllUndersection6Plot();
                        return View("Edit", undersection4plot);
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

                var result = await _undersection6plotservice.Delete(id);
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
            var list = await _undersection6plotservice.GetAllUndersection6Plot();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection6plotservice.FetchSingleResult(id);
            Data.NotificationList = await _undersection6plotservice.GetAllNotificationNo();
            Data.KhasraList = await _undersection6plotservice.BindKhasra(Data.VillageId);
            Data.VillageList = await _undersection6plotservice.GetAllVillage();


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
            return Json(await _undersection6plotservice.BindKhasra(Convert.ToInt32(villageId)));
        }


        [HttpGet]
        public async Task<JsonResult> GetAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _undersection6plotservice.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }


        public async Task<PartialViewResult> Notification6View([FromBody] NotificationList6SearchDto model)
        {
            var Data = await _undersection6plotservice.GetAllNotificationList(model);

            return PartialView("_ListNotification", Data);
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Undersection6plotList()
        {
            var result = await _undersection6plotservice.GetAllUndersection6Plot();
            List<Undersection6plotListDto> data = new List<Undersection6plotListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new Undersection6plotListDto()
                    {
                        Id = result[i].Id,
                        NotificationNo = result[i].Undersection6 == null ? "" : result[i].Undersection6.Number,
                        VillageName = result[i].Village == null ? "" : result[i].Village.Name,
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name,
                        ActualArea = result[i].Khasra.Bigha.ToString()
                                  + '-' + result[i].Khasra.Biswa.ToString()
                                  + '-' + result[i].Khasra.Biswanshi.ToString(),

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
