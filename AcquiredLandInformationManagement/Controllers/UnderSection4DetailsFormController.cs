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

namespace AcquiredLandInformationManagement.Controllers
{
    public class UnderSection4DetailsFormController : Controller
    {

        private readonly IUndersection4service _undersection4service;


        public UnderSection4DetailsFormController(IUndersection4service undersection4service)
        {
            _undersection4service = undersection4service;
        }


        public async Task<IActionResult> Index()
        {
            var list = await _undersection4service.GetAllUndersection4();
            return View(list);
        }



        public async Task<IActionResult> Create()
        {
            Undersection4 undersection4 = new Undersection4();
            undersection4.IsActive = 1;
            undersection4.PurposeList = await _undersection4service.GetAllPurpose();
           
            return View(undersection4);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Undersection4 undersection4)
        {
            try
            {
                undersection4.PurposeList = await _undersection4service.GetAllPurpose();

                if (ModelState.IsValid)
                {
                    var result = await _undersection4service.Create(undersection4);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection4service.GetAllUndersection4();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection4);
                    }
                }
                else
                {
                    return View(undersection4);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection4);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection4service.FetchSingleResult(id);
         
            Data.PurposeList = await _undersection4service.GetAllPurpose();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Undersection4 undersection4)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _undersection4service.Update(id, undersection4);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection4service.GetAllUndersection4();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection4);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(undersection4);
                }
            }
            else
            {
                return View(undersection4);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _undersection4service.Delete(id);
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
            var list = await _undersection4service.GetAllUndersection4();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection4service.FetchSingleResult(id);
            Data.PurposeList = await _undersection4service.GetAllPurpose();
           

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}