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
        public async Task<IActionResult> Create()
        {
            Undersection22plotdetails model = new Undersection22plotdetails();
            
            model.Undersection22List = await _undersection22plotdetailsService.GetAllUndersection22();
            model.Undersection4List = await _undersection22plotdetailsService.GetAllUndersection4();
            model.Undersection6List = await _undersection22plotdetailsService.GetAllUndersection6();
            model.Undersection17List = await _undersection22plotdetailsService.GetAllUndersection17();
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
            us22plot.Undersection4List = await _undersection22plotdetailsService.GetAllUndersection4();
            us22plot.Undersection6List = await _undersection22plotdetailsService.GetAllUndersection6();
            us22plot.Undersection17List = await _undersection22plotdetailsService.GetAllUndersection17();
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

            var Data = await _undersection22plotdetailsService.FetchSingleResult(id);
            Data.Undersection22List = await _undersection22plotdetailsService.GetAllUndersection22();
            Data.Undersection4List = await _undersection22plotdetailsService.GetAllUndersection4();
            Data.Undersection6List = await _undersection22plotdetailsService.GetAllUndersection6();
            Data.Undersection17List = await _undersection22plotdetailsService.GetAllUndersection17();
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
            us22plot.Undersection4List = await _undersection22plotdetailsService.GetAllUndersection4();
            us22plot.Undersection6List = await _undersection22plotdetailsService.GetAllUndersection6();
            us22plot.Undersection17List = await _undersection22plotdetailsService.GetAllUndersection17();
            us22plot.AcquiredlandvillageList = await _undersection22plotdetailsService.GetAllAcquiredlandvillage();
            us22plot.KhasraList = await _undersection22plotdetailsService.GetAllKhasra(us22plot.AcquiredlandvillageId);

            if (ModelState.IsValid)
            {
               
                    var result = await _undersection22plotdetailsService.Update(id, us22plot);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection22plotdetailsService.GetAllUS22PlotDetails();
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
            var Data = await _undersection22plotdetailsService.FetchSingleResult(id);
            Data.Undersection22List = await _undersection22plotdetailsService.GetAllUndersection22();
            Data.Undersection4List = await _undersection22plotdetailsService.GetAllUndersection4();
            Data.Undersection6List = await _undersection22plotdetailsService.GetAllUndersection6();
            Data.Undersection17List = await _undersection22plotdetailsService.GetAllUndersection17();
            Data.AcquiredlandvillageList = await _undersection22plotdetailsService.GetAllAcquiredlandvillage();
            Data.KhasraList = await _undersection22plotdetailsService.GetAllKhasra(Data.AcquiredlandvillageId);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
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



    }
}