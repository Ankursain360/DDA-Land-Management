using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace SiteMaster.Controllers
{
    public class LocalityController : Controller
    {
        public readonly ILocalityService _localityService;
        public LocalityController(ILocalityService localityService)
        {
            _localityService = localityService;
        }
        public async Task<IActionResult> Index()
        {
            List<Locality> list = await _localityService.GetAllLocality();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            Locality model = new Locality();
            model.IsActive = 1;
            model.DepartmentList = await _localityService.GetAllDepartment();
            model.ZoneList = await _localityService.GetAllZone(model.DepartmentId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Locality locality)
        {
            try
            {
                locality.DepartmentList = await _localityService.GetAllDepartment();
                locality.ZoneList = await _localityService.GetAllZone(locality.DepartmentId);
                if (ModelState.IsValid)
                {
                    var result = await _localityService.Create(locality);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _localityService.GetAllLocality();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(locality);
                    }
                }
                else
                {
                    return View(locality);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(locality);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _localityService.FetchSingleResult(id);
            Data.DepartmentList = await _localityService.GetAllDepartment();
            Data.ZoneList = await _localityService.GetAllZone(Data.DepartmentId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _localityService.FetchSingleResult(id);
            Data.DepartmentList = await _localityService.GetAllDepartment();
            Data.ZoneList = await _localityService.GetAllZone(Data.DepartmentId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Locality locality)
        {
            locality.DepartmentList = await _localityService.GetAllDepartment();
            locality.ZoneList = await _localityService.GetAllZone(locality.DepartmentId);
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _localityService.Update(id, locality);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _localityService.GetAllLocality();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(locality);
                    }
                }

                else
                {
                    return View(locality);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(locality);
            }
        }
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Praveen
        {
            try
            {

                var result = await _localityService.Delete(id);
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
            var list = await _localityService.GetAllLocality();
            return View("Index", list);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistName(int Id, string Name)
        {
            var result = await _localityService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Locality: {Name} already exist");
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistCode(int Id, string LocalityCode)
        {
            var result = await _localityService.CheckUniqueCode(Id, LocalityCode);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Locality Code : {LocalityCode} already exist");
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetZoneList(int?DepartmentId)
        {
            DepartmentId= DepartmentId ?? 0;
            return Json(await _localityService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }
    }
}