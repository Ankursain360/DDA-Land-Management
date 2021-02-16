using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class SakaniDetailsController : Controller
    {
        private readonly ISakanidetailService _sakanidetailService;
        public SakaniDetailsController(ISakanidetailService sakanidetailService)
        {
            _sakanidetailService = sakanidetailService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] SakaniDetailsSearchDto model)
        {
            var result = await _sakanidetailService.GetPagedSakanidetail(model);
            return PartialView("_List", result);
        }

        public async Task<IActionResult> Create()
        {
            Saknidetails sakanidetail = new Saknidetails();
            sakanidetail.IsActive = 1;
          
            return View(sakanidetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Saknidetails sakanidetail)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    var result = await _sakanidetailService.Create(sakanidetail);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _sakanidetailService.GetSakanidetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(sakanidetail);
                    }
                }
                else
                {
                    return View(sakanidetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(sakanidetail);
            }
        }



        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _sakanidetailService.FetchSingleResult(id);


           

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Saknidetails sakanidetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sakanidetailService.Update(id, sakanidetail);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _sakanidetailService.GetSakanidetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(sakanidetail);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(sakanidetail);
                }
            }
            else
            {
                return View(sakanidetail);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _sakanidetailService.Delete(id);
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
            var list = await _sakanidetailService.GetSakanidetail();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _sakanidetailService.FetchSingleResult(id);
            

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}