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
using Core.Enum;
using AcquiredLandInformationManagement.Filters;

namespace AcquiredLandInformationManagement.Controllers
{
    public class UnderSection6Master : Controller
    {
        private readonly IUnderSection6Service _undersection4service;

        public UnderSection6Master(IUnderSection6Service undersection4service)
        {
            _undersection4service = undersection4service;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Undersection6SearchDto model)
        {
            var result = await _undersection4service.GetPagedUndersection6details(model);

            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Undersection6 undersection4 = new Undersection6();
            undersection4.IsActive = 1;
            undersection4.NotificationList = await _undersection4service.GetAllundersection4();

            return View(undersection4);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Undersection6 undersection6)
        {
            try
            {
                undersection6.NotificationList = await _undersection4service.GetAllundersection4();

                if (ModelState.IsValid)
                {
                    var result = await _undersection4service.Create(undersection6);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection4service.GetAllUndersection6();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection6);
                    }
                }
                else
                {
                    return View(undersection6);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection6);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection4service.FetchSingleResult(id);

            Data.NotificationList = await _undersection4service.GetAllundersection4();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Undersection6 undersection6)
        {
            undersection6.NotificationList = await _undersection4service.GetAllundersection4();

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _undersection4service.Update(id, undersection6);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection4service.GetAllUndersection6();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection6);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(undersection6);
                }
            }
            else
            {
                return View(undersection6);
            }
        }


        [AuthorizeContext(ViewAction.Delete)]
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
            var list = await _undersection4service.GetAllUndersection6();
            return View("Index", list);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection4service.FetchSingleResult(id);
            Data.NotificationList = await _undersection4service.GetAllundersection4();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}
