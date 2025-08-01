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
using Core.Enum;
using SiteMaster.Filters;
using Utility.Helper;
using Dto.Master;

namespace SiteMaster.Controllers
{
    public class ClassificationOfLandController : BaseController
    {

        private readonly IClassificationOfLandService _classificationoflandService;

        public ClassificationOfLandController(IClassificationOfLandService classificationoflandService)
        {
            _classificationoflandService = classificationoflandService;
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ClassificationOfLandSearchDto model)
        {
            var result = await _classificationoflandService.GetPagedClassificationOfLand(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Classificationofland classificationofland)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _classificationoflandService.Create(classificationofland);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var result1 = await _classificationoflandService.GetAllClassificationOfLand();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(classificationofland);

                    }
                }
                else
                {
                    return View(classificationofland);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(classificationofland);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _classificationoflandService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Classificationofland classificationofland)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _classificationoflandService.Update(id, classificationofland);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _classificationoflandService.GetAllClassificationOfLand();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(classificationofland);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(classificationofland);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _classificationoflandService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Classification of Land : {Name} already exist");
            }
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  //Not in use
        {
            var result = await _classificationoflandService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _classificationoflandService.GetAllClassificationOfLand();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _classificationoflandService.GetAllClassificationOfLand();
                return View("Index", result1);
            }
           
        }
      
        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {

            var result = await _classificationoflandService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _classificationoflandService.GetAllClassificationOfLand();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _classificationoflandService.GetAllClassificationOfLand();
                return View("Index", result1);
            }

        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _classificationoflandService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
      
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> ClassificationoflandList()
        {
            var result = await _classificationoflandService.GetAllClassificationOfLand();
            List<ClassificationoflandListDto> data = new List<ClassificationoflandListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new ClassificationoflandListDto()
                    {
                        Id = result[i].Id,
                        Name = result[i].Name,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
