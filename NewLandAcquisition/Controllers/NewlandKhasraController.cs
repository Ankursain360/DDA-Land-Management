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
using NewLandAcquisition.Filters;
using Core.Enum;

namespace NewLandAcquisition.Controllers
{
    public class NewlandKhasraController : Controller
    {
        private readonly INewlandkhasraService _khasraService;


        public NewlandKhasraController(INewlandkhasraService khasraService)
        {
            _khasraService = khasraService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _khasraService.GetAllKhasra();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandkhasraSearchDto model)
        {
            var result = await _khasraService.GetPagedKhasra(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandkhasra khasra = new Newlandkhasra();
            khasra.IsActive = 1;
            khasra.LandCategoryList = await _khasraService.GetAllLandCategory();
           
            khasra.VillageList = await _khasraService.GetAllVillageList();
            return View(khasra);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandkhasra khasra)
        {
            try
            {
                khasra.LandCategoryList = await _khasraService.GetAllLandCategory();

                khasra.VillageList = await _khasraService.GetAllVillageList();

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



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _khasraService.FetchSingleResult(id);
            Data.LandCategoryList = await _khasraService.GetAllLandCategory();
          
            Data.VillageList = await _khasraService.GetAllVillageList();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandkhasra khasra)

        {
            khasra.VillageList = await _khasraService.GetAllVillageList();
            khasra.LandCategoryList = await _khasraService.GetAllLandCategory();
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
        [AuthorizeContext(ViewAction.Delete)]
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
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _khasraService.FetchSingleResult(id);
           
            Data.LandCategoryList = await _khasraService.GetAllLandCategory();
            Data.VillageList = await _khasraService.GetAllVillageList();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}
