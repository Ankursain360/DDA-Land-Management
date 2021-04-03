using System;
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
using LeaseForPublic.Filters;
using Core.Enum;

namespace LeaseForPublic.Controllers
{
    public class MortgageController : BaseController
    {
        private readonly IMortgageService _mortgageService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;

        string targetPathMortgageDocuments = "";
        public MortgageController(IMortgageService mortgageService,
            IConfiguration configuration, IWorkflowTemplateService workflowtemplateService,
            IApprovalProccessService approvalproccessService)
        {
            _configuration = configuration;
            _mortgageService = mortgageService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            targetPathMortgageDocuments = _configuration.GetSection("FilePaths:Mortgage:MortgageFilePath").Value.ToString();

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] MortgageSearchDto model)
        {
            var result = await _mortgageService.GetPagedMortgageDetails(model);
            return PartialView("_List", result);
        }

        //   [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Mortgage mortgage = new Mortgage();
            mortgage.IsActive = 1;
            mortgage.Documentchecklist = await _mortgageService.GetDocumentChecklistDetails(Convert.ToInt32(_configuration.GetSection("ServiceTypeIdMortgage").Value));
            return View(mortgage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mortgage mortgage)
        {
            try
            {
                mortgage.ServiceTypeId = 2;
                mortgage.Documentchecklist = await _mortgageService.GetDocumentChecklistDetails(Convert.ToInt32(_configuration.GetSection("ServiceTypeIdMortgage").Value));
                ////Is Mandatory check
                //if (mortgage.DocumentName != null && mortgage.Mandatory != null)
                //{
                //    if (mortgage.DocumentName.Count > 0 && mortgage.Mandatory.Count > 0)
                //    {
                //        for (int i = 0; i < mortgage.DocumentName.Count; i++)
                //        {
                //            if (mortgage.IsMandatory[i] == 1)
                //            {
                //                if (mortgage.FileUploaded == null || mortgage.FileUploaded.Count <= i)
                //                {
                //                    ViewBag.Message = Alert.Show("Please Upload Mandatory Documents", "", AlertType.Warning);
                //                    return View(mortgage);

                //                }
                //            }
                //        }
                //    }
                //}
                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    mortgage.CreatedBy = SiteContext.UserId;
                    mortgage.ApprovedStatus = 0;
                    mortgage.PendingAt = "1";
                    mortgage.IsActive = 1;
                    mortgage.Id = 0;
                    var result = await _mortgageService.Create(mortgage);

                    if (result)
                    {
                        List<Allotteeservicesdocument> allotteeservicesdocuments = new List<Allotteeservicesdocument>();
                        for (int i = 0; i < mortgage.DocumentChecklistId.Count; i++)
                        {
                            string filename = null;
                            if (mortgage.FileUploaded != null && mortgage.FileUploaded.Count > 0)
                                filename = mortgage.FileUploaded != null ?
                                                                   mortgage.FileUploaded.Count <= i ? string.Empty :
                                                                   fileHelper.SaveFile1(targetPathMortgageDocuments, mortgage.FileUploaded[i]) :
                                                                   mortgage.FileUploaded[i] != null || mortgage.FileUploadedPath[i] != "" ?
                                                                   mortgage.FileUploadedPath[i] : string.Empty;
                            allotteeservicesdocuments.Add(new Allotteeservicesdocument
                            {
                                DocumentChecklistId = mortgage.DocumentChecklistId.Count <= i ? 0 : mortgage.DocumentChecklistId[i],
                                ServiceId = mortgage.Id,
                                ServiceTypeId = mortgage.ServiceTypeId,
                                DocumentFileName = filename,
                                CreatedBy = SiteContext.UserId

                            });
                        }
                        if (allotteeservicesdocuments.Count > 0)
                            result = await _mortgageService.SaveAllotteeServiceDocuments(allotteeservicesdocuments);

                    }
                    if (result == true)
                    {
                        //#region Approval Proccess At 1st level start Added by Renu 16 March 2021
                        //var DataFlow = await dataAsync();
                        //for (int i = 0; i < DataFlow.Count; i++)
                        //{
                        //    if (!DataFlow[i].parameterSkip)
                        //    {
                        //        mortgage.ApprovedStatus = 0;
                        //        mortgage.PendingAt = Convert.ToInt32(DataFlow[i].parameterName);
                        //        result = await _leaseApplicationFormService.UpdateBeforeApproval(mortgage.Id, mortgage);  //Update Table details 
                        //        if (result)
                        //        {
                        //            Approvalproccess approvalproccess = new Approvalproccess();
                        //            approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                        //            approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessIdLeaseApplicationForm").Value);
                        //            approvalproccess.ServiceId = mortgage.Id;
                        //            approvalproccess.SendFrom = SiteContext.UserId;
                        //            approvalproccess.SendTo = Convert.ToInt32(DataFlow[i].parameterName);
                        //            approvalproccess.PendingStatus = 1;   //1
                        //            approvalproccess.Status = null;   //1
                        //            approvalproccess.Remarks = "Record Added and Send for Approval";///May be Uncomment
                        //            result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                        //        }

                        //        break;
                        //    }
                        //}

                        //#endregion 

                        ViewBag.Message = Alert.Show(Messages.AddAndApprovalRecordSuccess, "", AlertType.Success);
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(mortgage);

                    }
                }
                else
                {
                    return View(mortgage);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(mortgage);
            }
        }

        //[AuthorizeContext(ViewAction.Edit)]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var Data = await _mortgageService.FetchSingleResult(id);
        //    Data.HonbleList = await _mortgageService.GetAllHonble();
        //    Data.AllotmententryList = await _mortgageService.GetAllAllotment();
        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Cancellationentry cancellationentry)
        //{
        //    cancellationentry.AllotmententryList = await _mortgageService.GetAllAllotment();
        //    cancellationentry.HonbleList = await _mortgageService.GetAllHonble();

        //    FileHelper fileHelper = new FileHelper();
        //    if (cancellationentry.DemandLetterPhoto != null)
        //    {
        //        cancellationentry.DemandLetter = fileHelper.SaveFile1(targetPathDemandLetter, cancellationentry.DemandLetterPhoto);
        //    }
        //    if (cancellationentry.NocPhoto != null)
        //    {
        //        cancellationentry.Noc = fileHelper.SaveFile1(targetPathNOC, cancellationentry.NocPhoto);
        //    }
        //    if (cancellationentry.CancellationPhoto != null)
        //    {
        //        cancellationentry.CancellationOrder = fileHelper.SaveFile1(targetPathCanellationOrder, cancellationentry.CancellationPhoto);
        //    }
        //    try
        //    {
        //        cancellationentry.ModifiedBy = SiteContext.UserId;
        //        var result = await _mortgageService.Update(id, cancellationentry);
        //        if (result == true)
        //        {
        //            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
        //            var list = await _mortgageService.GetAllRequestForProceeding();
        //            return View("Index", list);
        //        }
        //        else
        //        {
        //            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //            return View(cancellationentry);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        return View(cancellationentry);
        //    }
        //    return View(cancellationentry);
        //}

        //[AuthorizeContext(ViewAction.View)]
        //public async Task<IActionResult> View(int id)
        //{
        //    var Data = await _mortgageService.FetchSingleResult(id);
        //    Data.HonbleList = await _mortgageService.GetAllHonble();
        //    Data.AllotmententryList = await _mortgageService.GetAllAllotment();
        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}

        //[AuthorizeContext(ViewAction.Delete)]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var result = await _mortgageService.Delete(id);
        //        if (result == true)
        //        {
        //            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
        //        }
        //        else
        //        {
        //            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //    }
        //    var list = await _mortgageService.GetAllRequestForProceeding();
        //    return View("Index", list);
        //}

        //public async Task<FileResult> ViewLetter(int Id)
        //{
        //    try
        //    {
        //        FileHelper file = new FileHelper();
        //        var Data = await _mortgageService.FetchSingleResult(Id);
        //        string targetPhotoPathLayout = targetPathDemandLetter + Data.DemandLetter;
        //        byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
        //        return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
        //    }
        //    catch (Exception ex)
        //    {

        //        FileHelper file = new FileHelper();
        //        var Data = await _mortgageService.FetchSingleResult(Id);
        //        string targetPhotoPathLayout = targetPathDemandLetter + Data.DemandLetter;
        //        byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
        //        return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

        //    }
        //}

        //public async Task<FileResult> ViewLetter1(int Id)
        //{
        //    try
        //    {
        //        FileHelper file = new FileHelper();
        //        var Data = await _mortgageService.FetchSingleResult(Id);
        //        string targetPhotoPathLayout = targetPathNOC + Data.Noc;
        //        byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
        //        return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
        //    }
        //    catch (Exception ex)
        //    {

        //        FileHelper file = new FileHelper();
        //        var Data = await _mortgageService.FetchSingleResult(Id);
        //        string targetPhotoPathLayout = targetPathNOC + Data.Noc;
        //        byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
        //        return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

        //    }
        //}
        //public async Task<FileResult> ViewLetter2(int Id)
        //{
        //    try
        //    {
        //        FileHelper file = new FileHelper();
        //        var Data = await _mortgageService.FetchSingleResult(Id);
        //        string targetPhotoPathLayout = targetPathCanellationOrder + Data.CancellationOrder;
        //        byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
        //        return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
        //    }
        //    catch (Exception ex)
        //    {

        //        FileHelper file = new FileHelper();
        //        var Data = await _mortgageService.FetchSingleResult(Id);
        //        string targetPhotoPathLayout = targetPathCanellationOrder + Data.CancellationOrder;
        //        byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
        //        return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

        //    }
        //}
        [HttpGet]
        public async Task<JsonResult> GetOtherData()
        {            
            return Json(await _mortgageService.GetAllotteeDetails(SiteContext.UserId));
        }

    }
}
