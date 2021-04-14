using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Dto.Search;
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
    public class MorLandsController : Controller
    {
        private readonly IMorlandService _morlandService;


        public MorLandsController(IMorlandService morlandService)
        {
            _morlandService = morlandService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] MorLandsSearchDto model)
        {
            var result = await _morlandService.GetPagedMorland(model);
            return PartialView("_List", result);
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _morlandService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Location/Village Name : {Name} already exist");
            }
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()

        {
            Morland morland = new Morland();
            morland.IsActive = 1;
            morland.LandNotificationList = await _morlandService.GetAllLandNotification();

           
            return View(morland);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Morland morland)
        {
            try
            {
                morland.LandNotificationList = await _morlandService.GetAllLandNotification();

           

                if (ModelState.IsValid)
                {
                    var result = await _morlandService.Create(morland);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _morlandService.GetAllMorland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(morland);
                    }
                }
                else
                {
                    return View(morland);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(morland);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _morlandService.FetchSingleResult(id);
            Data.LandNotificationList = await _morlandService.GetAllLandNotification();

         

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Morland morland)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _morlandService.Update(id, morland);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _morlandService.GetAllMorland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(morland);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(morland);
                }
            }
            else
            {
                return View(morland);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _morlandService.Delete(id);
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
            var list = await _morlandService.GetAllMorland();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _morlandService.FetchSingleResult(id);

            Data.LandNotificationList = await _morlandService.GetAllLandNotification();
          
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}
