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
    public class EncroachmentDetailsController : Controller
    {
        private readonly IEnchroachmentService _enchroachmentService;

        public EncroachmentDetailsController(IEnchroachmentService enchroachmentService)
        {
            _enchroachmentService = enchroachmentService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _enchroachmentService.GetAllEnchroachment();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            Enchroachment enchroachment = new Enchroachment();
            enchroachment.IsActive = 1;

            enchroachment.KhasraList = await _enchroachmentService.BindKhasra();
            enchroachment.VillageList = await _enchroachmentService.GetAllVillage();
           enchroachment.NencroachmentList = await _enchroachmentService.GetAllNencroachment();
            enchroachment.ReasonsList = await _enchroachmentService.GetAllReasons();

            return View(enchroachment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enchroachment enchroachment)
        {
            try
            {

                enchroachment.KhasraList = await _enchroachmentService.BindKhasra();
                enchroachment.VillageList = await _enchroachmentService.GetAllVillage();
                enchroachment.NencroachmentList = await _enchroachmentService.GetAllNencroachment();
                enchroachment.ReasonsList = await _enchroachmentService.GetAllReasons();

                if (ModelState.IsValid)
                {
                    var result = await _enchroachmentService.Create(enchroachment);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _enchroachmentService.GetAllEnchroachment();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(enchroachment);
                    }
                }
                else
                {
                    return View(enchroachment);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(enchroachment);
            }
        }




        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _enchroachmentService.FetchSingleResult(id);

            Data.NencroachmentList = await _enchroachmentService.GetAllNencroachment();
            Data.ReasonsList = await _enchroachmentService.GetAllReasons();

            Data.KhasraList = await _enchroachmentService.BindKhasra();
            Data.VillageList = await _enchroachmentService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enchroachment enchroachment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _enchroachmentService.Update(id, enchroachment);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _enchroachmentService.GetAllEnchroachment();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(enchroachment);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(enchroachment);
                }
            }
            else
            {
                return View(enchroachment);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _enchroachmentService.Delete(id);
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
            var list = await _enchroachmentService.GetAllEnchroachment();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _enchroachmentService.FetchSingleResult(id);
            Data.NencroachmentList = await _enchroachmentService.GetAllNencroachment();
            Data.ReasonsList = await _enchroachmentService.GetAllReasons();

            Data.KhasraList = await _enchroachmentService.BindKhasra();
            Data.VillageList = await _enchroachmentService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }









    }
}