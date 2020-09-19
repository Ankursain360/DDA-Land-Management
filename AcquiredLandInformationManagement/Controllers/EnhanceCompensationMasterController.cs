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
    public class EnhanceCompensationMasterController : Controller
    {
        private readonly IEnhancecompensationService _enhancecompensationService;
        public EnhanceCompensationMasterController(IEnhancecompensationService enhancecompensationService)
        {
            _enhancecompensationService = enhancecompensationService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _enhancecompensationService.GetAllEnhancecompensation();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            Enhancecompensation enhancecompensation = new Enhancecompensation();
            enhancecompensation.IsActive = 1;

            enhancecompensation.KhasraList = await _enhancecompensationService.BindKhasra();
            enhancecompensation.VillageList = await _enhancecompensationService.GetAllVillage();

            return View(enhancecompensation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enhancecompensation enhancecompensation)
        {
            try
            {

                enhancecompensation.KhasraList = await _enhancecompensationService.BindKhasra();
                enhancecompensation.VillageList = await _enhancecompensationService.GetAllVillage();

                if (ModelState.IsValid)
                {
                    var result = await _enhancecompensationService.Create(enhancecompensation);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _enhancecompensationService.GetAllEnhancecompensation();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(enhancecompensation);
                    }
                }
                else
                {
                    return View(enhancecompensation);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(enhancecompensation);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _enhancecompensationService.FetchSingleResult(id);


            Data.KhasraList = await _enhancecompensationService.BindKhasra();
            Data.VillageList = await _enhancecompensationService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enhancecompensation enhancecompensation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _enhancecompensationService.Update(id, enhancecompensation);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _enhancecompensationService.GetAllEnhancecompensation();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(enhancecompensation);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(enhancecompensation);
                }
            }
            else
            {
                return View(enhancecompensation);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _enhancecompensationService.Delete(id);
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
            var list = await _enhancecompensationService.GetAllEnhancecompensation();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _enhancecompensationService.FetchSingleResult(id);

            Data.KhasraList = await _enhancecompensationService.BindKhasra();
            Data.VillageList = await _enhancecompensationService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }



    }
}