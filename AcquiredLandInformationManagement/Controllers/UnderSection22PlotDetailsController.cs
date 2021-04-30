using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Service.IApplicationService;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Libraries.Model.Entity;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using Utility.Helper;
using Dto.Master;

namespace AcquiredLandInformationManagement.Controllers
{
    public class UnderSection22PlotDetailsController  : BaseController
    {
        private readonly IUndersection22plotdetailsService _undersection22plotdetailsService;
        public UnderSection22PlotDetailsController(IUndersection22plotdetailsService undersection22plotdetailsService)
        {
            _undersection22plotdetailsService = undersection22plotdetailsService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Undersection22plotdetailsSearchDto model)
        {
            var result = await _undersection22plotdetailsService.GetPagedUndersection22plotdetails(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Undersection22plotdetails model = new Undersection22plotdetails();
            
            model.Undersection22List = await _undersection22plotdetailsService.GetAllUndersection22();
           
            model.AcquiredlandvillageList = await _undersection22plotdetailsService.GetAllAcquiredlandvillage();
            model.KhasraList = await _undersection22plotdetailsService.GetAllKhasra(model.AcquiredlandvillageId);
           
            model.IsActive = 1;
            return View(model);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Undersection22plotdetails us22plot)
        {
           
            us22plot.Undersection22List = await _undersection22plotdetailsService.GetAllUndersection22();
            
            us22plot.AcquiredlandvillageList = await _undersection22plotdetailsService.GetAllAcquiredlandvillage();
            us22plot.KhasraList = await _undersection22plotdetailsService.GetAllKhasra(us22plot.AcquiredlandvillageId);
            us22plot.IsActive = 1;
           
            if (ModelState.IsValid)
                {
                    var result = await _undersection22plotdetailsService.Create(us22plot);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection22plotdetailsService.GetAllUS22PlotDetails();
                        return View("Create", us22plot);
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

            var Data = await _undersection22plotdetailsService.FetchSingleResult(id);
            Data.Undersection22List = await _undersection22plotdetailsService.GetAllUndersection22();
           
            Data.AcquiredlandvillageList = await _undersection22plotdetailsService.GetAllAcquiredlandvillage();
            Data.KhasraList = await _undersection22plotdetailsService.GetAllKhasra(Data.AcquiredlandvillageId);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Undersection22plotdetails us22plot)
        {

            us22plot.Undersection22List = await _undersection22plotdetailsService.GetAllUndersection22();
            
            us22plot.AcquiredlandvillageList = await _undersection22plotdetailsService.GetAllAcquiredlandvillage();
            us22plot.KhasraList = await _undersection22plotdetailsService.GetAllKhasra(us22plot.AcquiredlandvillageId);

            if (ModelState.IsValid)
            {
               
                    var result = await _undersection22plotdetailsService.Update(id, us22plot);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection22plotdetailsService.GetAllUS22PlotDetails();
                        return View("Edit", us22plot);
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
            var Data = await _undersection22plotdetailsService.FetchSingleResult(id);
            Data.Undersection22List = await _undersection22plotdetailsService.GetAllUndersection22();
           
            Data.AcquiredlandvillageList = await _undersection22plotdetailsService.GetAllAcquiredlandvillage();
            Data.KhasraList = await _undersection22plotdetailsService.GetAllKhasra(Data.AcquiredlandvillageId);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _undersection22plotdetailsService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _undersection22plotdetailsService.GetAllUS22PlotDetails();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _undersection22plotdetailsService.GetAllKhasra(Convert.ToInt32(villageId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _undersection22plotdetailsService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }

        public async Task<PartialViewResult> Notification4View([FromBody] NotificationList22SearchDto model)
        {
            var Data = await _undersection22plotdetailsService.GetAllNotificationList(model);

            return PartialView("_ListNotification", Data);
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Undersection22plotList()
        {
            var result = await _undersection22plotdetailsService.GetAllUS22PlotDetails();
            List<Undersection22plotListDto> data = new List<Undersection22plotListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new Undersection22plotListDto()
                    {
                        Id = result[i].Id,
                        NotificationUS22 = result[i].UnderSection22 == null ? "" : result[i].UnderSection22.NotificationNo,
                        VillageName = result[i].Acquiredlandvillage == null ? "" : result[i].Acquiredlandvillage.Name,
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