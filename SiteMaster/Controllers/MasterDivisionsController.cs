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


namespace SiteMaster.Controllers
{
    public class MasterDivisionsController : Controller
    {
        private readonly IDivisionService _divisionService;


        public MasterDivisionsController(IDivisionService divisionService)
        {
            _divisionService = divisionService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _divisionService.GetAllDivision();
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            Division model = new Division();
            model.IsActive = 1;
            model.DepartmentList = await _divisionService.GetAllDepartment();
            model.ZoneList = await _divisionService.GetAllZone(model.DepartmentId);
            return View(model);
        }

      


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Division division)
        {
            try
            {
                division.DepartmentList = await _divisionService.GetAllDepartment();
                division.ZoneList = await _divisionService.GetAllZone(division.DepartmentId);


                if (ModelState.IsValid)
                {
                  
                    var result = await _divisionService.Create(division);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _divisionService.GetAllDivision();
                        return View("Index", list);
                       
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(division);

                    }
                }
                else
                {
                    return View(division);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(division);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _divisionService.FetchSingleResult(id);
            Data.DepartmentList = await _divisionService.GetAllDepartment();
            Data.ZoneList = await _divisionService.GetAllZone(Data.DepartmentId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Division division)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _divisionService.Update(id, division);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return RedirectToAction("Index", "MasterDivisions");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(division);

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
            }
            return View(division);
        }


        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _divisionService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Division: {Name} already exist");
            }
        }

        public async Task<IActionResult> Delete(int id)  
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _divisionService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }

            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View(form);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  
        {
            

            var result = await _divisionService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "Division");
         

        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _divisionService.FetchSingleResult(id);
            Data.DepartmentList = await _divisionService.GetAllDepartment();
            Data.ZoneList = await _divisionService.GetAllZone(Data.DepartmentId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _divisionService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }


    }
}
