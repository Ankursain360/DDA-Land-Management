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
    public class ServiceTypeController : BaseController
    {
        private readonly IServiceTypeService _serviceTypeService;


        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _serviceTypeService.GetAllServicetype();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ServiceTypeSearchDto model)
        {
            var result = await _serviceTypeService.GetPagedServicetype(model);
            return PartialView("_List", result);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servicetype servicetype)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    var result = await _serviceTypeService.Create(servicetype);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _serviceTypeService.GetAllServicetype();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(servicetype);
                    }
                }
                else
                {
                    return View(servicetype);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(servicetype);
            }
        }




        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _serviceTypeService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servicetype servicetype)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _serviceTypeService.Update(id, servicetype);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _serviceTypeService.GetAllServicetype();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(servicetype);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(servicetype);
                }
            }
            else
            {
                return View(servicetype);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _serviceTypeService.Delete(id);
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
            var list = await _serviceTypeService.GetAllServicetype();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _serviceTypeService.FetchSingleResult(id);




            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}

