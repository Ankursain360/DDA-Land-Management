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
using AcquiredLandInformationManagement.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;

namespace AcquiredLandInformationManagement.Controllers
{
    public class NazulController : Controller
    {

        private readonly INazulService _nazulService;

        public NazulController(INazulService nazulService)
        {
            _nazulService = nazulService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _nazulService.GetAllNazul();
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            Nazul nazul = new Nazul();
            nazul.IsActive = 1;
           

            nazul.VillageList = await _nazulService.GetAllVillage();
            return View(nazul);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nazul nazul)
        {
            try
            {
                nazul.VillageList = await _nazulService.GetAllVillage();

                if (ModelState.IsValid)
                {
                    if (nazul.JaraiSakni == 0)
                        nazul.JaraiSakni = 0;
                    else if (nazul.JaraiSakni == 1)
                        nazul.JaraiSakni = 1;
                    else if (nazul.JaraiSakni == 2)
                        nazul.JaraiSakni = 2;

                    var result = await _nazulService.Create(nazul);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View(nazul);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(nazul);

                    }
                }
                else
                {
                    return View(nazul);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(nazul);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _nazulService.FetchSingleResult(id);
            Data.VillageList = await _nazulService.GetAllVillage();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Nazul nazul)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _nazulService.Update(id, nazul);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _nazulService.GetAllNazul();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(nazul);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(nazul);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {

            var result = await _nazulService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _nazulService.GetAllNazul();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _nazulService.GetAllNazul();
                return View("Index", result1);
            }

        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _nazulService.FetchSingleResult(id);
            Data.VillageList = await _nazulService.GetAllVillage();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        
    }

}
