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
using LeaseDetails.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dto.Search;
using LeaseDetails.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Microsoft.Extensions.Configuration;

namespace LeaseDetails.Controllers
{
    public class ProceedingEvictionLetterController : BaseController
    {

        private readonly IProceedingEvictionLetterService _proceedingEvictionLetterService;
        public IConfiguration _configuration;
        private readonly IApprovalProccessService _approvalproccessService;

        string ProceedingEvictionLetterPath = "";
        public ProceedingEvictionLetterController(IProceedingEvictionLetterService proceedingEvictionLetterService,
            IConfiguration configuration, IApprovalProccessService approvalproccessService)
        {
            _proceedingEvictionLetterService = proceedingEvictionLetterService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
            ProceedingEvictionLetterPath = _configuration.GetSection("FilePaths:ProceedingEvictionLetter:ProceedingEvictionLetterPath").Value.ToString();
        }

        //[AuthorizeContext(ViewAction.Add)]
        //public async Task<IActionResult> Create()
        //{
        //    ProceedingEvictionLetterCreateProfileDto data = new ProceedingEvictionLetterCreateProfileDto();
        //    data.RefNoNameList = await _proceedingEvictionLetterService.BindRefNoNameList();
        //    ViewBag.VisibleLetter = 0;
        //    return View(data);
        //}

        //[HttpPost]
        //public async Task<PartialViewResult> ViewLetter([FromBody] ProceedingEvictionLetterSearchDto model)
        //{
        //    var result = false;
        //    if(model !=  null)
        //    {
        //        var IsUpdate = await _proceedingEvictionLetterService.GetLetterRefNo(Convert.ToInt32(model.RefNoNameId));
        //        if (IsUpdate == null)
        //        {
        //            result = await _proceedingEvictionLetterService.UpdateRequestProceeding(model, SiteContext.UserId);
        //        }
        //        else
        //        {
        //            if (model.RefNoNameId != 0 && model.LetterReferenceNo != null)
        //            {
        //                result = true;
        //            }
        //        }
        //    }

        //    if(result)
        //    {
        //        var data = await _proceedingEvictionLetterService.FetchProceedingConvictionLetterData(model);
        //        ViewBag.VisibleLetter = 1;
        //        return PartialView("_ViewLetter", data);
        //    }
        //    else
        //    {
        //        Requestforproceeding data = new Requestforproceeding();
        //        ViewBag.Message = Alert.Show("No data Found", "", AlertType.Info);
        //        return PartialView("_ViewLetter", data);
        //    }
        //}

        //[HttpGet]
        //public async Task<JsonResult> GetLetterRefNo(int? Id)
        //{
        //    Id = Id ?? 0;
        //    return Json(await _proceedingEvictionLetterService.GetLetterRefNo(Convert.ToInt32(Id)));
        //}

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> UploadLetter(int id)
        {
            Requestforproceeding data = new Requestforproceeding();
            data = await _proceedingEvictionLetterService.FetchProceedingConvictionLetterData(id);
            data.Id = id;
            return View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadLetter(int id, Requestforproceeding requestforproceeding)
        {
            try
            {
                var result = false;
                if (requestforproceeding.checkIsSend != 1)
                {
                    if (requestforproceeding.ProcedingLetterDocument == null && requestforproceeding.ProcedingLetter == null)
                    {
                        ViewBag.Message = Alert.Show("Upload Document is Mandatory", "", AlertType.Warning);
                        return View(requestforproceeding);
                    }
                    FileHelper fileHelper = new FileHelper();
                    requestforproceeding.ProcedingLetter = requestforproceeding.ProcedingLetterDocument != null ?
                                                                       fileHelper.SaveFile1(ProceedingEvictionLetterPath, requestforproceeding.ProcedingLetterDocument) :
                                                                       requestforproceeding.ProcedingLetterDocument != null || requestforproceeding.ProcedingLetter != "" ?
                                                                       requestforproceeding.ProcedingLetter : string.Empty;
                    requestforproceeding.ModifiedBy = SiteContext.UserId;
                    result = await _proceedingEvictionLetterService.UpdateRequestProceedingUpload(id, requestforproceeding);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show("Proceeding Document Uploaded Sucessfully", "", AlertType.Success);
                        TempData["Message"] = Alert.Show("Proceeding Document Uploaded Sucessfully", "", AlertType.Success);
                        return RedirectToAction("Index", "RequestForProceedingEviction");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(requestforproceeding);

                    }

                }
                else
                {
                    #region Approval Proccess At 1st level start Added by Renu 22 March 2021
                    Requestforproceeding Data = await _proceedingEvictionLetterService.FetchProceedingConvictionLetterData(id);
                    result = await _proceedingEvictionLetterService.UpdateRequestProceedingIsSend(Data, SiteContext.UserId);//Update Table details 
                    if (result)
                    {
                        Approvalproccess approvalproccess = new Approvalproccess();
                        approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                        approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessIdLeaseHearingDetails").Value);
                        approvalproccess.ServiceId = id;
                        approvalproccess.SendFrom = SiteContext.UserId;
                        approvalproccess.SendTo = Data.UserId;
                        approvalproccess.PendingStatus = 0;  
                        approvalproccess.Status = 1;  
                        approvalproccess.Remarks = "Record Added and Send for Approval";///May be Uncomment
                        result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                    }

                    #endregion
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show("Letter Send to Official", "", AlertType.Success);
                        TempData["Message"] = Alert.Show("Letter Send to Officia", "", AlertType.Success);
                        return RedirectToAction("Index", "RequestForProceedingEviction");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(requestforproceeding);
                    }

                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(requestforproceeding);
            }
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> GenerateLetter(int id)
        {
            ProceedingEvictionLetterCreateProfileDto data = new ProceedingEvictionLetterCreateProfileDto();
            data.RefNoNameId = id;
            ViewBag.VisibleLetter = 0;
            return View(data);

        }
        [HttpPost]
        public async Task<PartialViewResult> ViewLetter([FromBody] ProceedingEvictionLetterSearchDto model)
        {
            var result = false;
            if (model != null)
            {
                result = await _proceedingEvictionLetterService.UpdateRequestProceeding(model, SiteContext.UserId);
            }
            if (result)
            {
                ProceedingEvictionLetterViewLetterDataDto data = new ProceedingEvictionLetterViewLetterDataDto();
                data = await _proceedingEvictionLetterService.BindProceedingConvictionLetterData(model.RefNoNameId);
                ViewBag.VisibleLetter = 1;
                return PartialView("_ViewLetter", data);
            }
            else
            {
                Requestforproceeding data = new Requestforproceeding();
                ViewBag.Message = Alert.Show("No data Found", "", AlertType.Info);
                return PartialView("_ViewLetter", data);
            }
        }


        public async Task<IActionResult> ViewProceedingLetter(int Id)
        {
            FileHelper file = new FileHelper();
            Requestforproceeding Data = await _proceedingEvictionLetterService.FetchProceedingConvictionLetterData(Id);
            string filename = ProceedingEvictionLetterPath + Data.ProcedingLetter;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }
    }
}