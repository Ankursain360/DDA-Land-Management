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
   public class UnderSection22DetailsController : Controller
    {
        private readonly IUndersection22Service _undersection22Service;

        public UnderSection22DetailsController(IUndersection22Service undersection22Service)
        {
            _undersection22Service = undersection22Service;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _undersection22Service.GetAllUndersection22();
            return View(result);

        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
      
        public async Task<IActionResult> Create(Undersection22 undersection22)
        {
            try
            {

                if (ModelState.IsValid)
                {
                   

                    var result = await _undersection22Service.Create(undersection22);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection22);

                    }
                }
                else
                {
                    return View(undersection22);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection22);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection22Service.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
       
        public async Task<IActionResult> Edit(int id, Undersection22 undersection22)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    var result = await _undersection22Service.Update(id, undersection22);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection22);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(undersection22);
        }

        //[AcceptVerbs("Get", "Post")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Exist(int Id, string Name)
        //{
        //    var result = await _undersection22Service.CheckUniqueName(Id, Name);
        //    if (result == false)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json($"Module: {Name} already exist");
        //    }
        //}


        public async Task<IActionResult> Delete(int id) 
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _undersection22Service.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _undersection22Service.GetAllUndersection22(); 
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }

      
        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection22Service.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
       
    }
}