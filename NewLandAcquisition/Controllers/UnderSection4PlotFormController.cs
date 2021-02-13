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

namespace NewLandAcquisition.Controllers
{
    public class UnderSection4PlotFormController : Controller
    {

        private readonly IUndersection4PlotService _undersection4PlotService;


        public UnderSection4PlotFormController(IUndersection4PlotService undersection4PlotService)
        {
            _undersection4PlotService = undersection4PlotService;
        }


        public async Task<IActionResult> Index()
        {
            var list = await _undersection4PlotService.GetAllUndersection4Plot();
            return View(list);
        }

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
                        return View("Index", list);
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
        public async Task<IActionResult> Edit(int id, Undersection4plot undersection4plot)
        {
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

        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection4PlotService.FetchSingleResult(id);
            Data.NotificationList = await _undersection4PlotService.GetAllNotificationNo();
           // Data.KhasraList = await _undersection4PlotService.GetAllKhasra();
            Data.VillageList = await _undersection4PlotService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }









    }
}