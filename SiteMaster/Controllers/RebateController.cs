using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using SiteMaster.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;

namespace SiteMaster.Controllers
{
    public class RebateController : Controller
    {

        private readonly IRebateService _rebateService;

        public RebateController(IRebateService rebateService)
        {
            _rebateService = rebateService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _rebateService.GetAllRebate();
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            Rebate rebate = new Rebate();
            rebate.IsActive = 1;
            return View(rebate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rebate rebate)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _rebateService.Create(rebate);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View(rebate);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rebate);

                    }
                }
                else
                {
                    return View(rebate);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(rebate);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _rebateService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rebate rebate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _rebateService.Update(id, rebate);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _rebateService.GetAllRebate();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rebate);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(rebate);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {

            var result = await _rebateService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _rebateService.GetAllRebate();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _rebateService.GetAllRebate();
                return View("Index", result1);
            }

        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _rebateService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }

}
