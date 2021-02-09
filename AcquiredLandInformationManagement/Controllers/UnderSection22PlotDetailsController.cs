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

        //[HttpPost]
        //public async Task<PartialViewResult> List([FromBody] Undersection22SearchDto model)
        //{
        //    var result = await _undersection22Service.GetPagedUndersection22(model);
        //    return PartialView("_List", result);
        //}
        public async Task<IActionResult> Create()
        {
            Undersection22plotdetails model = new Undersection22plotdetails();
            
            model.Undersection22List = await _undersection22plotdetailsService.GetAllUndersection22();
            model.Undersection4List = await _undersection22plotdetailsService.GetAllUndersection4();
            model.Undersection6List = await _undersection22plotdetailsService.GetAllUndersection6();
            model.Undersection17List = await _undersection22plotdetailsService.GetAllUndersection17();
            model.AcquiredlandvillageList = await _undersection22plotdetailsService.GetAllAcquiredlandvillage();
            model.KhasraList = await _undersection22plotdetailsService.GetAllKhasra(model.AcquiredlandvillageId);
            return View(model);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Undersection22plotdetails us22plot)
        {
            //try
            //{
            us22plot.Undersection22List = await _undersection22plotdetailsService.GetAllUndersection22();
            us22plot.Undersection4List = await _undersection22plotdetailsService.GetAllUndersection4();
            us22plot.Undersection6List = await _undersection22plotdetailsService.GetAllUndersection6();
            us22plot.Undersection17List = await _undersection22plotdetailsService.GetAllUndersection17();
            us22plot.AcquiredlandvillageList = await _undersection22plotdetailsService.GetAllAcquiredlandvillage();
            us22plot.KhasraList = await _undersection22plotdetailsService.GetAllKhasra(us22plot.AcquiredlandvillageId);

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
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            //    return View(locality);
            //}

           
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _undersection22plotdetailsService.GetAllKhasra(Convert.ToInt32(villageId)));
        }
    }
}