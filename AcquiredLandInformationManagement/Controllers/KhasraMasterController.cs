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
    public class KhasraMasterController : Controller
    {
        private readonly IKhasraService _khasraService;


        public KhasraMasterController(IKhasraService khasraService)
        {
            _khasraService = khasraService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _khasraService.GetAllKhasra();
            return View(list);
        }


        public async Task<IActionResult> Create()
        
        {
            Khasra khasra = new Khasra();
            khasra.IsActive = 1;
            khasra.LandCategoryList = await _khasraService.GetAllLandCategory();
           
            khasra.VillageList = await _khasraService.GetAllVillage();
            return View(khasra);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Khasra khasra)
        {
            try
            {
                khasra.LandCategoryList = await _khasraService.GetAllLandCategory();

                khasra.VillageList = await _khasraService.GetAllVillage();

                if (ModelState.IsValid)
                {
                    var result = await _khasraService.Create(khasra);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _khasraService.GetAllKhasra();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(khasra);
                    }
                }
                else
                {
                    return View(khasra);    
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(khasra);
            }
        }




        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _khasraService.FetchSingleResult(id);
            Data.LandCategoryList = await _khasraService.GetAllLandCategory();
          
            Data.VillageList = await _khasraService.GetAllVillage();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Khasra khasra)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _khasraService.Update(id, khasra);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _khasraService.GetAllKhasra();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(khasra);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(khasra);
                }
            }
            else
            {
                return View(khasra);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _khasraService.Delete(id);
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
            var list = await _khasraService.GetAllKhasra();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _khasraService.FetchSingleResult(id);
           
            Data.LandCategoryList = await _khasraService.GetAllLandCategory();
            Data.VillageList = await _khasraService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}
