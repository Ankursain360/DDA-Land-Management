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
namespace LeaseDetails.Controllers
{
    public class LeasepurposeController : Controller
    {
        private readonly ILeasepurposeService _LeasepurposeService;


        public LeasepurposeController(ILeasepurposeService LeasepurposeService)
        {
            _LeasepurposeService = LeasepurposeService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _LeasepurposeService.GetLeasepurposes();
            return View(list);
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LeasepurposeSearchDto model)
        {
            var result = await _LeasepurposeService.GetpagedLeasepurpose(model);
            return PartialView("_List", result);
        }

        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Leasepurpose Leasepurpose)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    var result = await _LeasepurposeService.Create(Leasepurpose);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _LeasepurposeService.GetLeasepurposes();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Leasepurpose);
                    }
                }
                else
                {
                    return View(Leasepurpose);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(Leasepurpose);
            }
        }




        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _LeasepurposeService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Leasepurpose Leasepurpose)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _LeasepurposeService.Update(id, Leasepurpose);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _LeasepurposeService.GetLeasepurposes();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Leasepurpose);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(Leasepurpose);
                }
            }
            else
            {
                return View(Leasepurpose);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _LeasepurposeService.Delete(id);
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
            var list = await _LeasepurposeService.GetLeasepurposes();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _LeasepurposeService.FetchSingleResult(id);




            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}

