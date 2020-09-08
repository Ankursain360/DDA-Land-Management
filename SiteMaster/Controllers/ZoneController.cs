using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using SiteMaster.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiteMaster.Controllers
{
    public class ZoneController : Controller
    {

        private readonly IZoneService _zoneService;

        public ZoneController(IZoneService zoneService)
        {
            _zoneService = zoneService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _zoneService.GetAllDetails();
            return View(result);
        }

        void BindDropDown()
        {
            Zone zone = new Zone();
          //  var list = _zoneService.GetDropDownList();
            zone.DepartmentList = (IEnumerable<SelectListItem>)_zoneService.GetDropDownList();

            //ViewBag.DepartmentList = list;

        }
        public IActionResult Create()
        {
            BindDropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Zone zone)
        {
            try
            {
                BindDropDown();

                if (ModelState.IsValid)
                {
                    //if (CheckUniqueName(0, zone))
                    //{
                    //    ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Unique Name Required for Zone Name", Status = "S", BackPageAction = "Create", BackPageController = "ZoneMaster" };
                    //    return View(zone);

                    //}
                    //if (CheckUniqueCode(0, zone))
                    //{
                    //    ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Unique Name Required for Zone Code", Status = "S", BackPageAction = "Create", BackPageController = "ZoneMaster" };
                    //    return View(zone);

                    //}

                    var result = await _zoneService.Create(zone);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(zone);

                    }
                }
                else
                {
                    return View(zone);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(zone);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            BindDropDown();
            var Data = await _zoneService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Zone zone)
        {
            BindDropDown();
            if (ModelState.IsValid)
            {
                try
                {

                    //if (CheckUniqueName(id, zone))
                    //{
                    //    ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Unique Name Required for Zone Name", Status = "S", BackPageAction = "Create", BackPageController = "ZoneMaster" };
                    //    return View(zone);

                    //}
                    //if (CheckUniqueCode(id, zone))
                    //{
                    //    ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Unique Name Required for Zone Code", Status = "S", BackPageAction = "Create", BackPageController = "ZoneMaster" };
                    //    return View(zone);

                    //}

                    var result = await _zoneService.Update(id, zone);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(zone);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(zone);
                }
            }
            return View(zone);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _zoneService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Designation: {Name} already exist");
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsCodeExist(int Id, string Code)
        {
            var result = await _zoneService.CheckUniqueCode(Id, Code);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Zone Code: {Code} already exist");
            }
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            //try
            //{

            var result = await _zoneService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "ZoneMaster");
            //}
            //catch(Exception ex)
            //{
            //    ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Something went wrong", Status = "S", BackPageAction = "Index", BackPageController = "Zone" };
            //    return View();
            //}

        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _zoneService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}