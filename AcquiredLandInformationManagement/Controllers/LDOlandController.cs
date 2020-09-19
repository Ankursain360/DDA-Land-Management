using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace AcquiredLandInformationManagement.Controllers
{
   
   public class LDOlandController : Controller
    {
        private readonly ILdolandService _ldolandService;

        public LDOlandController(ILdolandService ldolandService)
        {
            _ldolandService = ldolandService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _ldolandService.GetAllLdoland();
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            Ldoland ldoland = new Ldoland();
            ldoland.IsActive = 1;
            ldoland.LandNotificationList = await _ldolandService.GetAllLandNotification();
            ldoland.SerialnumberList = await _ldolandService.GetAllSerialnumber();
            return View(ldoland);
        }


        [HttpPost]
      
        public async Task<IActionResult> Create(Ldoland ldoland)
        {
            try
            {
                ldoland.LandNotificationList = await _ldolandService.GetAllLandNotification();
                ldoland.SerialnumberList = await _ldolandService.GetAllSerialnumber();
                if (ModelState.IsValid)
                {


                    var result = await _ldolandService.Create(ldoland);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _ldolandService.GetAllLdoland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(ldoland);

                    }
                }
                else
                {
                    return View(ldoland);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(ldoland);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _ldolandService.FetchSingleResult(id);
            Data.LandNotificationList = await _ldolandService.GetAllLandNotification();
            Data.SerialnumberList = await _ldolandService.GetAllSerialnumber();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
      
        public async Task<IActionResult> Edit(int id, Ldoland ldoland)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _ldolandService.Update(id, ldoland);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _ldolandService.GetAllLdoland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(ldoland);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(ldoland);
        }

       // [AcceptVerbs("Get", "Post")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Exist(int Id, string Name)
        //{
        //    var result = await _pageService.CheckUniqueName(Id, Name);
        //    if (result == false)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json($"Page: {Name} already exist");
        //    }
        //}


        public async Task<IActionResult> Delete(int id)  //Not in use
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _ldolandService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _ldolandService.GetAllLdoland();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _ldolandService.FetchSingleResult(id);
           
            Data.LandNotificationList = await _ldolandService.GetAllLandNotification();
            Data.SerialnumberList = await _ldolandService.GetAllSerialnumber();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


    }
}
