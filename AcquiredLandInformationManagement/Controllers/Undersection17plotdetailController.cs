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
namespace AcquiredLandInformationManagement.Controllers
{
    public class Undersection17plotdetailController : Controller
    {
        private readonly IUndersection17plotdetailService _undersection17plotdetailService;


        public Undersection17plotdetailController(IUndersection17plotdetailService undersection17plotdetailService)
        {
            _undersection17plotdetailService = undersection17plotdetailService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _undersection17plotdetailService.GetAllUndersection17plotdetail();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Undersection17plotdetailSearchDto model)
        {
            var result = await _undersection17plotdetailService.GetPagedUndersection17plotdetail(model);
            return PartialView("_List", result);
        }

        public async Task<IActionResult> Create()

        {
            Undersection17plotdetail undersection17plotdetail = new Undersection17plotdetail();
            undersection17plotdetail.IsActive = 1;
           

            undersection17plotdetail.VillageList = await _undersection17plotdetailService.GetAllVillageList();
            undersection17plotdetail.KhasraList = await _undersection17plotdetailService.GetAllKhasraList(undersection17plotdetail.VillageId);
            undersection17plotdetail.Undersection17List = await _undersection17plotdetailService.GetAllUndersection17List();
            return View(undersection17plotdetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Undersection17plotdetail undersection17Plotdetail)
        {
            try
            {
                undersection17Plotdetail.Undersection17List = await _undersection17plotdetailService.GetAllUndersection17List();

                undersection17Plotdetail.VillageList = await _undersection17plotdetailService.GetAllVillageList();
                undersection17Plotdetail.KhasraList = await _undersection17plotdetailService.GetAllKhasraList(undersection17Plotdetail.VillageId);

                if (ModelState.IsValid)
                {
                    var result = await _undersection17plotdetailService.Create(undersection17Plotdetail);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection17plotdetailService.GetAllUndersection17plotdetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection17Plotdetail);
                    }
                }
                else
                {
                    return View(undersection17Plotdetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection17Plotdetail);
            }
        }




        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection17plotdetailService.FetchSingleResult(id);
            Data.Undersection17List = await _undersection17plotdetailService.GetAllUndersection17List();

            Data.VillageList = await _undersection17plotdetailService.GetAllVillageList();
            Data.KhasraList = await _undersection17plotdetailService.GetAllKhasraList(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Undersection17plotdetail undersection17plotdetail)
        {
            undersection17plotdetail.Undersection17List = await _undersection17plotdetailService.GetAllUndersection17List();
            undersection17plotdetail.VillageList = await _undersection17plotdetailService.GetAllVillageList();
            undersection17plotdetail.KhasraList = await _undersection17plotdetailService.GetAllKhasraList(undersection17plotdetail.VillageId);
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _undersection17plotdetailService.Update(id, undersection17plotdetail);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection17plotdetailService.GetAllUndersection17plotdetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection17plotdetail);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(undersection17plotdetail);
                }
            }
            else
            {
                return View(undersection17plotdetail);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _undersection17plotdetailService.Delete(id);
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
            var list = await _undersection17plotdetailService.GetAllUndersection17plotdetail();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection17plotdetailService.FetchSingleResult(id);

            Data.Undersection17List = await _undersection17plotdetailService.GetAllUndersection17List();
            Data.VillageList = await _undersection17plotdetailService.GetAllVillageList();
            Data.KhasraList = await _undersection17plotdetailService.GetAllKhasraList(Data.VillageId);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _undersection17plotdetailService.GetAllKhasraList(Convert.ToInt32(villageId)));
        }


     


        [HttpGet]
        public async Task<JsonResult> GetAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _undersection17plotdetailService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }


    }
}
