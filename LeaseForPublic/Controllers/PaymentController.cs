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
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;

        string targetPathExtensionDocuments = "";
        public PaymentController(IPaymentService paymentService,
            IConfiguration configuration, IWorkflowTemplateService workflowtemplateService,
            IApprovalProccessService approvalproccessService)
        {
            _configuration = configuration;
            _paymentService = paymentService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            targetPathExtensionDocuments = _configuration.GetSection("FilePaths:Extension:ExtensionFilePath").Value.ToString();

        }

        public async Task<IActionResult> Index()
        {
            Payment data = new Payment();
            var result = await _paymentService.GetAllotteeDetails(SiteContext.UserId);
            ViewBag.PaymentTypeId = result.Allotment.LeasesTypeId;
            ViewBag.AllotmentId = result.AllotmentId;
            return View(data);
        }
        public async Task<PartialViewResult> List(int AllotmentId)
        {
            var result = await _paymentService.GetPremiumDrDetails(AllotmentId, Convert.ToInt32(_configuration.GetSection("LeasePaymentPremiumId").Value), SiteContext.UserId);
            return PartialView("_List", result);
        }
        public async Task<PartialViewResult> ListGroundRent(int AllotmentId)
        {
            var result = await _paymentService.GetPremiumDrDetails(AllotmentId, Convert.ToInt32(_configuration.GetSection("LeasePaymentGroundRentId").Value),SiteContext.UserId);
            return PartialView("_List", result);
        }
        public async Task<PartialViewResult> ListDocumentCharge(int AllotmentId)
        {
            var result = await _paymentService.GetPremiumDrDetails(AllotmentId, Convert.ToInt32(_configuration.GetSection("LeasePaymentDocumentChargesId").Value),SiteContext.UserId);
            return PartialView("_ListDocumentCharge", result);
        }
        public async Task<PartialViewResult> ListLicenceFees(int AllotmentId)
        {
            var result = await _paymentService.GetPremiumDrDetails(AllotmentId, Convert.ToInt32(_configuration.GetSection("LeasePaymentLicenseFeesId").Value),SiteContext.UserId);
            return PartialView("_List", result);
        }
        public async Task<PartialViewResult> AlloteeDetails()
        {
            var result = await _paymentService.GetAllotteeDetails(SiteContext.UserId);
            return PartialView("_AlloteeDetails", result);
        }

        public async Task<IActionResult> PayNowCreate(int id)
        {
            Payment payment = await _paymentService.FetchResultPayment(id);
            Random r = new Random();
            int num = r.Next();
            var finalString = (DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond).ToUpper();
           
            var result = await _paymentService.GetPremiumDrDetails(payment.AllotmentId, payment.LeasePaymentTypeId, SiteContext.UserId);
            var data = result.FirstOrDefault();
            payment.Amount = data.Amount;
            payment.InterestAmount = 0;
            payment.CGSTAmount = 0;
            payment.SGSTAmount = 0;
            payment.TotalAmount = data.Amount;
            payment.PaymentTypeName = data.LeasePaymentType;
            payment.PaymentTransactionNo = num + finalString;
            payment.BillNo = num.ToString();
            return View(payment);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Extension extension)
        //{
        //    try
        //    {
        //        extension.ServiceTypeId = 5;
        //        extension.Documentchecklist = await _extensionService.GetDocumentChecklistDetails(Convert.ToInt32(_configuration.GetSection("ServiceTypeIdExtensionService").Value));

        //        if (ModelState.IsValid)
        //        {
        //            FileHelper fileHelper = new FileHelper();
        //            extension.CreatedBy = SiteContext.UserId;
        //            extension.IsActive = 1;
        //            extension.Id = 0;
        //            var result = await _extensionService.Create(extension);

        //            if (result)
        //            {
        //                List<Allotteeservicesdocument> allotteeservicesdocuments = new List<Allotteeservicesdocument>();
        //                for (int i = 0; i < extension.DocumentChecklistId.Count; i++)
        //                {
        //                    string filename = null;
        //                    if (extension.FileUploaded != null && extension.FileUploaded.Count > 0)
        //                        filename = extension.FileUploaded != null ?
        //                                                           extension.FileUploaded.Count <= i ? string.Empty :
        //                                                           fileHelper.SaveFile1(targetPathExtensionDocuments, extension.FileUploaded[i]) :
        //                                                           extension.FileUploaded[i] != null || extension.FileUploadedPath[i] != "" ?
        //                                                           extension.FileUploadedPath[i] : string.Empty;
        //                    allotteeservicesdocuments.Add(new Allotteeservicesdocument
        //                    {
        //                        DocumentChecklistId = extension.DocumentChecklistId.Count <= i ? 0 : extension.DocumentChecklistId[i],
        //                        ServiceId = extension.Id,
        //                        ServiceTypeId = extension.ServiceTypeId,
        //                        DocumentFileName = filename,
        //                        CreatedBy = SiteContext.UserId

        //                    });
        //                }
        //                if (allotteeservicesdocuments.Count > 0)
        //                    result = await _extensionService.SaveAllotteeServiceDocuments(allotteeservicesdocuments);

        //            }
        //            if (result == true)
        //            {
        //                #region Approval Proccess At 1st level start Added by Renu 16 March 2021
        //                var DataFlow = await dataAsync();
        //                for (int i = 0; i < DataFlow.Count; i++)
        //                {
        //                    if (!DataFlow[i].parameterSkip)
        //                    {
        //                        extension.ApprovedStatus = 0;
        //                        extension.PendingAt = (DataFlow[i].parameterName);
        //                        result = await _extensionService.UpdateBeforeApproval(extension.Id, extension);  //Update Table details 
        //                        if (result)
        //                        {
        //                            Approvalproccess approvalproccess = new Approvalproccess();
        //                            approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
        //                            approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessIdExtensionService").Value);
        //                            approvalproccess.ServiceId = extension.Id;
        //                            approvalproccess.SendFrom = SiteContext.UserId;
        //                            approvalproccess.SendTo = Convert.ToInt32(DataFlow[i].parameterName);
        //                            approvalproccess.PendingStatus = 1;   //1
        //                            approvalproccess.Status = null;   //1
        //                            approvalproccess.Remarks = "Record Added and Send for Approval";///May be Uncomment
        //                            result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
        //                        }

        //                        break;
        //                    }
        //                }

        //                #endregion 

        //                ViewBag.Message = Alert.Show(Messages.AddAndApprovalRecordSuccess, "", AlertType.Success);
        //                return View("Index");
        //            }
        //            else
        //            {
        //                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //                return View(extension);

        //            }
        //        }
        //        else
        //        {
        //            return View(extension);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        return View(extension);
        //    }
        //}

        //// [AuthorizeContext(ViewAction.Edit)]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var Data = await _extensionService.FetchSingleResult(id);
        //    var Msg = TempData["Message"] as string;
        //    if (Msg != null)
        //        ViewBag.Message = Msg;
        //    Data.IsActive = 1;
        //    Data.AllotteeservicesdocumentList = await _extensionService.AlloteeDocumentListDetails(id);

        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Extension extension)
        //{
        //    try
        //    {
        //        extension.ServiceTypeId = 5;
        //        extension.AllotteeservicesdocumentList = await _extensionService.AlloteeDocumentListDetails(id);
        //        var result = false;
        //        if (ModelState.IsValid)
        //        {
        //            FileHelper fileHelper = new FileHelper();
        //            if (extension.EditPosition == "NotComplete")
        //            {
        //                var Data = await _extensionService.FetchSingleResultDocument(extension.EditDocumentId);
        //                Allotteeservicesdocument allotteeservicesdocuments = new Allotteeservicesdocument();
        //                string filename = null;
        //                filename = extension.EditFileUploaded != null ?
        //                            fileHelper.SaveFile1(targetPathExtensionDocuments, extension.EditFileUploaded) :
        //                            extension.EditDocumentFileName;
        //                allotteeservicesdocuments.DocumentChecklistId = Data.DocumentChecklistId;
        //                allotteeservicesdocuments.ServiceId = extension.Id;
        //                allotteeservicesdocuments.ServiceTypeId = Data.ServiceTypeId;
        //                allotteeservicesdocuments.DocumentFileName = filename;
        //                allotteeservicesdocuments.CreatedBy = SiteContext.UserId;
        //                result = await _extensionService.UpdateAllotteeServiceDocuments(extension.EditDocumentId, allotteeservicesdocuments);



        //                if (result)
        //                    ViewBag.Message = Alert.Show("Document Updated Successfully", "", AlertType.Success);
        //                else
        //                    ViewBag.Message = Alert.Show("Enable to update Document, please try again", "", AlertType.Warning);

        //                // return RedirectToAction("Edit", id);
        //                //ViewBag.Message = Alert.Show("Document Updated Successfully", "", AlertType.Success);
        //                return View(extension);
        //            }
        //            else
        //            {
        //                extension.ModifiedBy = SiteContext.UserId;
        //                extension.IsActive = 1;
        //                result = await _extensionService.Update(id, extension);

        //                if (result)
        //                {
        //                    List<Allotteeservicesdocument> allotteeservicesdocuments = new List<Allotteeservicesdocument>();
        //                    for (int i = 0; i < extension.DocumentChecklistId.Count; i++)
        //                    {
        //                        string filename = null;
        //                        if (extension.FileUploaded != null && extension.FileUploaded.Count > 0)
        //                            filename = extension.FileUploaded != null ?
        //                                                               extension.FileUploaded.Count <= i ? string.Empty :
        //                                                               fileHelper.SaveFile1(targetPathExtensionDocuments, extension.FileUploaded[i]) :
        //                                                               extension.FileUploaded[i] != null || extension.FileUploadedPath[i] != "" ?
        //                                                               extension.FileUploadedPath[i] : string.Empty;
        //                        else
        //                            filename = extension.DocumentName[i];
        //                        allotteeservicesdocuments.Add(new Allotteeservicesdocument
        //                        {
        //                            DocumentChecklistId = extension.DocumentChecklistId.Count <= i ? 0 : extension.DocumentChecklistId[i],
        //                            ServiceId = extension.Id,
        //                            ServiceTypeId = extension.ServiceTypeId,
        //                            DocumentFileName = filename,
        //                            CreatedBy = SiteContext.UserId,
        //                            Id = extension.AllotteeDocumentId[i]

        //                        });
        //                    }
        //                    foreach (var item in allotteeservicesdocuments)
        //                    {
        //                        if (item.Id != 0)
        //                            result = await _extensionService.UpdateAllotteeServiceDocuments(item.Id, item);
        //                        else
        //                            result = await _extensionService.SaveAllotteeServiceDocumentsSingle(item);

        //                    }
        //                }
        //                if (result == true)
        //                {

        //                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
        //                    return View("Index");
        //                }
        //                else
        //                {
        //                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //                    return View(extension);

        //                }
        //            }

        //        }
        //        else
        //        {
        //            return View(extension);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        return View(extension);
        //    }
        //}

        ////   [AuthorizeContext(ViewAction.View)]
        //public async Task<IActionResult> View(int id)
        //{
        //    var Data = await _extensionService.FetchSingleResult(id);
        //    Data.IsActive = 1;
        //    Data.AllotteeservicesdocumentList = await _extensionService.AlloteeDocumentListDetails(id);

        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}

        //// [AuthorizeContext(ViewAction.Delete)]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var result = await _extensionService.Delete(id, SiteContext.UserId);
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
        //    return View("Index");
        //}

        //public async Task<FileResult> ViewExtensionDocument(int Id)
        //{
        //    FileHelper file = new FileHelper();
        //    var Data = await _extensionService.FetchSingleResultDocument(Id);
        //    string targetPhotoPathLayout = targetPathExtensionDocuments + Data.DocumentFileName;
        //    byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
        //    return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
        //}

        //[HttpGet]
        //public async Task<JsonResult> EditDocument(int DocumentId)
        //{
        //    return Json(await _extensionService.FetchSingleResultDocument(DocumentId));
        //}

        [HttpGet]
        public async Task<JsonResult> GetOtherData()
        {
            return Json(await _paymentService.GetAllotteeDetails(SiteContext.UserId));
        }

        //[HttpGet]
        //public async Task<JsonResult> GetTimeLineExtensionFees()
        //{
        //    return Json(await _extensionService.GetTimeLineExtensionFees());
        //}

        //#region Fetch workflow data for approval prrocess Added by Renu 16 March 2021
        //private async Task<List<TemplateStructure>> dataAsync()
        //{
        //    var Data = await _workflowtemplateService.FetchSingleResult(Convert.ToInt32(_configuration.GetSection("workflowPreccessIdExtensionService").Value));
        //    var template = Data.Template;
        //    List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
        //    return ObjList;
        //}
        //#endregion
    }
}
