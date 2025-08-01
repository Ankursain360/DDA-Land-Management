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
using Microsoft.AspNetCore.Mvc.Rendering;
using Dto.Search;
using Dto.Master;
using SiteMaster.Filters;
using Core.Enum;
using Utility.Helper;

namespace SiteMaster.Controllers
{
    public class DocumentChecklistController : BaseController
    {

        private readonly IDocumentCheckListService _documentCheckListService;

        public DocumentChecklistController(IDocumentCheckListService documentCheckListService)
        {
            _documentCheckListService = documentCheckListService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ViewBag.ServiceTypeList = await _documentCheckListService.GetServiceTypeList();
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DocumentChecklistSearchDto model)
        {
            var result = await _documentCheckListService.GetPagedDocumentChecklistData(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Documentchecklist documentchecklist)
        {
            documentchecklist.ServiceTypeList = await _documentCheckListService.GetServiceTypeList();
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Documentchecklist documentchecklist = new Documentchecklist();
            documentchecklist.IsActive = 1;
            await BindDropDown(documentchecklist);
            return View(documentchecklist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Documentchecklist documentchecklist)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await BindDropDown(documentchecklist);
                    documentchecklist.CreatedBy = SiteContext.UserId;
                    var result = await _documentCheckListService.Create(documentchecklist);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        ViewBag.ServiceTypeList = await _documentCheckListService.GetServiceTypeList();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(documentchecklist);

                    }
                }
                else
                {
                    return View(documentchecklist);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(documentchecklist);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _documentCheckListService.FetchSingleResult(id);
            await BindDropDown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Documentchecklist documentchecklist)
        {
            await BindDropDown(documentchecklist);
            if (ModelState.IsValid)
            {
                try
                {

                    documentchecklist.ModifiedBy = SiteContext.UserId;
                    var result = await _documentCheckListService.Update(id, documentchecklist);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        ViewBag.ServiceTypeList = await _documentCheckListService.GetServiceTypeList();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(documentchecklist);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(documentchecklist);
                }
            }
            return View(documentchecklist);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistName(int Id, string Name , int ServiceTypeId)
        {
            var result = await _documentCheckListService.CheckUniqueName(Id, Name, ServiceTypeId);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Document Name : {Name} already exist");
            }
        }
        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            var result = await _documentCheckListService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            ViewBag.ServiceTypeList = await _documentCheckListService.GetServiceTypeList();
            return View("Index");

        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _documentCheckListService.FetchSingleResult(id);
            await BindDropDown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }



        public async Task<IActionResult> DocumentChecklistList()
        {
            var result = await _documentCheckListService.GetAllDocumentchecklist();
            List<DocumentChecklistListDto> data = new List<DocumentChecklistListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DocumentChecklistListDto()
                    {
                        Id = result[i].Id,

                        ServiceType = result[i].ServiceType == null ? "" : result[i].ServiceType.Name.ToString(),
                        DocumentName = result[i].Name.ToString(),
                        IsMandatory = result[i].IsMandatory == 1 ? "Yes" : "No",
                  


                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


    }
}