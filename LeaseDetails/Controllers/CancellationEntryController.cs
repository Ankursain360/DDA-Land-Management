﻿using System;
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
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Microsoft.AspNetCore.Http;
using System.IO;
using LeaseDetails.Filters;
using Core.Enum;
using Dto.Master;

namespace LeaseDetails.Controllers
{
    public class CancellationEntryController : BaseController
    {
        private readonly ICancellationEntryService _cancellationEntryService;
        public IConfiguration _configuration;
        string DemandletterFilePath = string.Empty;
        string NOCFilePath = string.Empty;
        string CancellationOrderFilePath = string.Empty;

        string targetPathDemandLetter = "";
        string targetPathNOC = "";
        string targetPathCanellationOrder = "";
        public CancellationEntryController(ICancellationEntryService cancellationEntryService, 
            IConfiguration configuration)
        {
            _configuration = configuration;
            _cancellationEntryService = cancellationEntryService;
            targetPathDemandLetter = _configuration.GetSection("FilePaths:CancellationEntry:DemandletterFilePath").Value.ToString();
            targetPathNOC = _configuration.GetSection("FilePaths:CancellationEntry:NOCFilePath").Value.ToString();
            targetPathCanellationOrder = _configuration.GetSection("FilePaths:CancellationEntry:CancellationOrderFilePath").Value.ToString();
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var Msg = TempData.Peek("Message");
            if (Msg != null)
                ViewBag.Message = Msg;
            var list = await _cancellationEntryService.GetAllRequestForProceeding();
            return View(list);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] CancellationEntrySearchDto model)
        {
            var result = await _cancellationEntryService.GetPagedCancellationEntry(model);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Cancellationentry cancellationentry = new Cancellationentry();
            cancellationentry.IsActive = 1;
            cancellationentry.HonbleList = await _cancellationEntryService.GetAllHonble();
            cancellationentry.AllotmententryList = await _cancellationEntryService.GetAllAllotment();
            return View(cancellationentry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cancellationentry cancellationentry)
        {            
            try
            {
                cancellationentry.AllotmententryList = await _cancellationEntryService.GetAllAllotment();
                cancellationentry.HonbleList = await _cancellationEntryService.GetAllHonble();
                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    if (cancellationentry.DemandLetterPhoto != null)
                    {
                        cancellationentry.DemandLetter = fileHelper.SaveFile1(targetPathDemandLetter, cancellationentry.DemandLetterPhoto);
                    }
                    if (cancellationentry.NocPhoto != null)
                    {
                        cancellationentry.Noc = fileHelper.SaveFile1(targetPathNOC, cancellationentry.NocPhoto);
                    }
                    if (cancellationentry.CancellationPhoto != null)
                    {
                        cancellationentry.CancellationOrder = fileHelper.SaveFile1(targetPathCanellationOrder, cancellationentry.CancellationPhoto);
                    }
                    cancellationentry.CreatedBy = SiteContext.UserId;

                    cancellationentry.CreatedBy = SiteContext.UserId;
                    var result = await _cancellationEntryService.Create(cancellationentry);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _cancellationEntryService.GetAllRequestForProceeding();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(cancellationentry);
                    }
                }
                else
                {
                    return View(cancellationentry);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(cancellationentry);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _cancellationEntryService.FetchSingleResult(id);
            Data.HonbleList = await _cancellationEntryService.GetAllHonble();
            Data.AllotmententryList = await _cancellationEntryService.GetAllAllotment();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Cancellationentry cancellationentry)
        {
            cancellationentry.AllotmententryList = await _cancellationEntryService.GetAllAllotment();
            cancellationentry.HonbleList = await _cancellationEntryService.GetAllHonble();

            FileHelper fileHelper = new FileHelper();
            if (cancellationentry.DemandLetterPhoto != null)
            {
                cancellationentry.DemandLetter = fileHelper.SaveFile1(targetPathDemandLetter, cancellationentry.DemandLetterPhoto);
            }
            if (cancellationentry.NocPhoto != null)
            {
                cancellationentry.Noc = fileHelper.SaveFile1(targetPathNOC, cancellationentry.NocPhoto);
            }
            if (cancellationentry.CancellationPhoto != null)
            {
                cancellationentry.CancellationOrder = fileHelper.SaveFile1(targetPathCanellationOrder, cancellationentry.CancellationPhoto);
            }
            try
            {
                cancellationentry.ModifiedBy = SiteContext.UserId;
                var result = await _cancellationEntryService.Update(id, cancellationentry);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _cancellationEntryService.GetAllRequestForProceeding();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(cancellationentry);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(cancellationentry);
            }
            return View(cancellationentry);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _cancellationEntryService.FetchSingleResult(id);
            Data.HonbleList = await _cancellationEntryService.GetAllHonble();
            Data.AllotmententryList = await _cancellationEntryService.GetAllAllotment();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _cancellationEntryService.Delete(id);
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
            var list = await _cancellationEntryService.GetAllRequestForProceeding();
            return View("Index", list);
        }

        public async Task<FileResult> ViewLetter(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathDemandLetter + Data.DemandLetter;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathDemandLetter + Data.DemandLetter;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }

        public async Task<FileResult> ViewLetter1(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathNOC + Data.Noc;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathNOC + Data.Noc;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }
        public async Task<FileResult> ViewLetter2(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathCanellationOrder + Data.CancellationOrder;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathCanellationOrder + Data.CancellationOrder;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }
        [HttpGet]
        public async Task<JsonResult> GetOtherData(int? AllottmentId)
        {
            AllottmentId = AllottmentId ?? 0;
            return Json(await _cancellationEntryService.FetchAllottmentDetails(Convert.ToInt32(AllottmentId)));
        }
        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> CancellationentryList()
        {
            var result = await _cancellationEntryService.GetAllRequestForProceeding();
            List<CancellationentryListDto> data = new List<CancellationentryListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new CancellationentryListDto()
                    {
                        Id = result[i].Id,
                        ReferenceNo = result[i].Allotment == null ? "" : result[i].Allotment.Application.RefNo,
                        SocietyName = result[i].Allotment == null ? "" : result[i].Allotment.Application.Name,
                        AllotmentDate = Convert.ToDateTime(result[i].Allotment.AllotmentDate).ToString("dd-MMM-yyyy"),
                        Area = result[i].Allotment == null ? "" : result[i].Allotment.TotalArea.ToString(),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
