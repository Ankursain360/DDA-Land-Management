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
    public class JaraiDetailsController : Controller
    {
        private readonly IJaraidetailService _jaraidetailService;
        public JaraiDetailsController(IJaraidetailService jaraidetailService)
        {
            _jaraidetailService = jaraidetailService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _jaraidetailService.GetJaraidetail();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            Jaraidetail jaraidetail = new Jaraidetail();
            jaraidetail.IsActive = 1;
            jaraidetail.KhewatList = await _jaraidetailService.GetAllKhewat();
            jaraidetail.KhasraList = await _jaraidetailService.BindKhasra();
            jaraidetail.VillageList = await _jaraidetailService.GetAllVillage();
            jaraidetail.TarafList = await _jaraidetailService.GetAllTaraf();
            jaraidetail.KhatauniList = await _jaraidetailService.GetAllKhatauni();


            return View(jaraidetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Jaraidetail jaraidetail)
        {
            try
            {
                jaraidetail.KhewatList = await _jaraidetailService.GetAllKhewat();
                jaraidetail.KhasraList = await _jaraidetailService.BindKhasra();
                jaraidetail.VillageList = await _jaraidetailService.GetAllVillage();
                jaraidetail.TarafList = await _jaraidetailService.GetAllTaraf();
                jaraidetail.KhatauniList = await _jaraidetailService.GetAllKhatauni();

                if (ModelState.IsValid)
                {
                    var result = await _jaraidetailService.Create(jaraidetail);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _jaraidetailService.GetJaraidetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(jaraidetail);
                    }
                }
                else
                {
                    return View(jaraidetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(jaraidetail);
            }
        }



        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _jaraidetailService.FetchSingleResult(id);


            Data.KhewatList = await _jaraidetailService.GetAllKhewat();
            Data.KhasraList = await _jaraidetailService.BindKhasra();
            Data.VillageList = await _jaraidetailService.GetAllVillage();
            Data.TarafList = await _jaraidetailService.GetAllTaraf();
            Data.KhatauniList = await _jaraidetailService.GetAllKhatauni();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Jaraidetail jaraidetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _jaraidetailService.Update(id, jaraidetail);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _jaraidetailService.GetJaraidetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(jaraidetail);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(jaraidetail);
                }
            }
            else
            {
                return View(jaraidetail);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _jaraidetailService.Delete(id);
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
            var list = await _jaraidetailService.GetJaraidetail();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _jaraidetailService.FetchSingleResult(id);
            Data.KhewatList = await _jaraidetailService.GetAllKhewat();
            Data.KhasraList = await _jaraidetailService.BindKhasra();
            Data.VillageList = await _jaraidetailService.GetAllVillage();
            Data.TarafList = await _jaraidetailService.GetAllTaraf();
            Data.KhatauniList = await _jaraidetailService.GetAllKhatauni();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }



    }
}