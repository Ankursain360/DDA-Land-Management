﻿using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using SiteMaster.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;
using Dto.Master;
namespace SiteMaster.Controllers
{
    public class LocalityController : BaseController
    {
        public readonly ILocalityService _localityService;
        public LocalityController(ILocalityService localityService)
        {
            _localityService = localityService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LocalitySearchDto model)
        {
            var result = await _localityService.GetPagedLocality(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Locality model = new Locality();
            model.IsActive = 1;
            model.DepartmentList = await _localityService.GetAllDepartment();
            model.ZoneList = await _localityService.GetAllZone(model.DepartmentId);
            return View(model);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
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


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _localityService.FetchSingleResult(id);
            Data.DepartmentList = await _localityService.GetAllDepartment();
            Data.ZoneList = await _localityService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _localityService.GetAllDivisionList(Data.ZoneId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _localityService.FetchSingleResult(id);
            Data.DepartmentList = await _localityService.GetAllDepartment();
            Data.ZoneList = await _localityService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _localityService.GetAllDivisionList(Data.ZoneId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Locality locality)
        {
            locality.DepartmentList = await _localityService.GetAllDepartment();
            locality.ZoneList = await _localityService.GetAllZone(locality.DepartmentId);
            locality.DivisionList = await _localityService.GetAllDivisionList(locality.ZoneId);
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
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Praveen
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
            var list = await _localityService.GetAllLocality();
            return View("Index", list);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistName(int Id, string Name, int DepartmentId, int DivisionId, int ZoneId)
        {
            var result = await _localityService.CheckUniqueName(Id, Name, DepartmentId, DivisionId, ZoneId);
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
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _localityService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _localityService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> LocalityList()
        {
            var result = await _localityService.GetAllLocality();
            List<LocalityListDto> data = new List<LocalityListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new LocalityListDto()
                    {
                        Id = result[i].Id,
                        Department = result[i].Department == null ? "" : result[i].Department.Name,
                        Division =  result[i].Division == null ? "" : result[i].Division.Name,
                        LocalityVillageCode = result[i].LocalityCode,
                        LocalityVillageName = result[i].Name,
                        Zone = result[i].Zone == null ? "" : result[i].Zone.Name,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);       
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
