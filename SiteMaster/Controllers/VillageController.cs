using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;

namespace SiteMaster.Controllers
{
    public class VillageController : Controller
    {
        private readonly IVillageService _villageService;
        public VillageController(IVillageService villageService)
        {
            _villageService = villageService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _villageService.GetAllVillage();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] VillageSearchDto model)
        {
            var result = await _villageService.GetPagedVillage(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create()
        {
            Village village = new Village();
            village.IsActive = 1;
            village.ZoneList = await _villageService.GetAllZone();
            return View(village);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Village village)
        {
            try
            {
                village.ZoneList = await _villageService.GetAllZone();
                if (ModelState.IsValid)
                {
                    var result = await _villageService.Create(village);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _villageService.GetAllVillage();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(village);
                    }
                }
                else
                {
                    return View(village);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(village);
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _villageService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Village: {Name} already exist");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _villageService.FetchSingleResult(id);
            Data.ZoneList = await _villageService.GetAllZone();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Village village)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _villageService.Update(id, village);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _villageService.GetAllVillage();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(village);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(village);
                }
            }
            else
            {
                return View(village);
            }
        }
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Praveen
        {
            try
            {

                var result = await _villageService.Delete(id);
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
            var list = await _villageService.GetAllVillage();
            return View("Index", list);
        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _villageService.FetchSingleResult(id);
            Data.ZoneList = await _villageService.GetAllZone();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}