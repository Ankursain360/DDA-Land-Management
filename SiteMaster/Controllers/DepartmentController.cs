﻿using System;
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
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;


using Utility.Helper;
using Dto.Master;

namespace SiteMaster.Controllers
{
    public class DepartmentController : BaseController
    {

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> List([FromBody] DepartmentSearchDto model)
        {
            var result = await _departmentService.GetPagedDepartment(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            try
            {

                if (ModelState.IsValid)
                {


                    var result = await _departmentService.Create(department);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        
                        var list = await _departmentService.GetAllDepartment();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(department);

                    }
                }
                else
                {
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(department);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _departmentService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {



                    var result = await _departmentService.Update(id, department);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _departmentService.GetAllDepartment();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(department);

                    }
                }
                catch (Exception ex)
                {

                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(department);

                }
            }
            return View(department);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name) 
        {
            var result = await _departmentService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Department: {Name} already exist");
            }
        }



        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            

            var result = await _departmentService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "Department");
            

        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _departmentService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Praveen
        {
            try
            {

                var result = await _departmentService.Delete(id);
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
            var list = await _departmentService.GetAllDepartment();
            return View("Index", list);
        }





        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> DepartmentList()
        {
            var result = await _departmentService.GetAllDepartment();
            List<DepartmentListDto> data = new List<DepartmentListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DepartmentListDto()
                    {
                        Id = result[i].Id,
                        
                        DepartmentName = result[i].Name,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }





    }

}
