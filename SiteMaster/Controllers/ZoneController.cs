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
            var result = await _zoneService.GetAllZone();
            return View(result);
        }

        void BindDropDown()
        {
            //ViewBag.DepartmentList = _context.Tbldepartmentmaster.Select(x => new { x.DepartmentRecNo, x.DepartmentName }).ToList();

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
                    if (CheckUniqueName(0, zone))
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Unique Name Required for Zone Name", Status = "S", BackPageAction = "Create", BackPageController = "ZoneMaster" };
                        return View(zone);

                    }
                    if (CheckUniqueCode(0, zone))
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Unique Name Required for Zone Code", Status = "S", BackPageAction = "Create", BackPageController = "ZoneMaster" };
                        return View(zone);

                    }

                    var result = await _zoneService.Create(zone);

                    if (result == true)
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Details Successfully Saved!!.", Status = "S", BackPageAction = "Index", BackPageController = "ZoneMaster" };
                        return View();
                    }
                    else
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Please try again later", Status = "S", BackPageAction = "Create", BackPageController = "ZoneMaster" };
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
                ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Something went wrong!!.", Status = "S", BackPageAction = "Create", BackPageController = "ZoneMaster" };
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

                    if (CheckUniqueName(id, zone))
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Unique Name Required for Zone Name", Status = "S", BackPageAction = "Create", BackPageController = "ZoneMaster" };
                        return View(zone);

                    }
                    if (CheckUniqueCode(id, zone))
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Unique Name Required for Zone Code", Status = "S", BackPageAction = "Create", BackPageController = "ZoneMaster" };
                        return View(zone);

                    }

                    var result = await _zoneService.Update(id, zone);
                    if (result == true)
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Details Successfully Updated!!.", Status = "S", BackPageAction = "Index", BackPageController = "ZoneMaster" };
                        return View();
                    }
                    else
                    {
                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Please try again later", Status = "S", BackPageAction = "Index", BackPageController = "ZoneMaster" };
                        return View(zone);

                    }
                }
                catch (Exception ex)
                {
                    ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Something went wrong!!.", Status = "S", BackPageAction = "Create", BackPageController = "ZoneMaster" };
                    return View(zone);
                }
            }
            return View(zone);
        }

        private bool CheckUniqueName(int id, Zone zone)
        {
            var result = _zoneService.CheckUniqueName(id, zone);
            return result;
        }
        private bool CheckUniqueCode(int id, Zone zone)
        {
            var result = _zoneService.CheckUniqueCode(id, zone);
            return result;
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            //try
            //{

            var result = await _zoneService.Delete(id);
            if (result == true)
            {
                ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Details Successfully Deleted!!.", Status = "S", BackPageAction = "Index", BackPageController = "ZoneMaster" };
            }
            else
            {
                ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Please try again later", Status = "S", BackPageAction = "Index", BackPageController = "ZoneMaster" };
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