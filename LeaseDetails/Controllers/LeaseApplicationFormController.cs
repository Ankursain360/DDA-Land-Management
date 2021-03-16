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
using Microsoft.Extensions.Configuration;
using Dto.Master;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace LeaseDetails.Controllers
{
    public class LeaseApplicationFormController : BaseController
    {

        private readonly ILeaseApplicationFormService _leaseApplicationFormService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public LeaseApplicationFormController(ILeaseApplicationFormService leaseApplicationFormService,
            IConfiguration configuration, IApprovalProccessService approvalproccessService,
            IWorkflowTemplateService workflowtemplateService, IHostingEnvironment hostingEnvironment)
        {
            _leaseApplicationFormService = leaseApplicationFormService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
            _workflowtemplateService = workflowtemplateService;
            _hostingEnvironment = hostingEnvironment;
        }
        //public async Task<IActionResult> Index()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<PartialViewResult> List([FromBody] DocumentChecklistSearchDto model)
        //{
        //    var result = await _leaseApplicationFormService.GetPagedDocumentChecklistData(model);
        //    return PartialView("_List", result);
        //}
        //async Task BindDropDown(Documentchecklist documentchecklist)
        //{
        //    documentchecklist.ServiceTypeList = await _leaseApplicationFormService.GetServiceTypeList();
        //}

        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            var Msg = TempData["Message"] as string;
            if (Msg != null)
                ViewBag.Message = Msg;
            Leaseapplication leaseapplication = new Leaseapplication();
            //  leaseapplication.Documentchecklist = await _leaseApplicationFormService.GetDocumentChecklistDetails(Convert.ToInt32(_configuration.GetSection("ServiceTypeIdLeaseAppForm").Value));
            ViewBag.IsPrintable = 0;
            return View(leaseapplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Leaseapplication leaseapplication)
        {
            try
            {
                var finalString = (DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond).ToUpper();
                leaseapplication.RefNo = leaseapplication.RegistrationNo + leaseapplication.ContactNo + finalString;
                string FilePath = _configuration.GetSection("FilePaths:LeaseApplicationForm:DocumentFilePath").Value.ToString();

                //Is Mandatory check
                if (leaseapplication.DocumentName != null && leaseapplication.Mandatory != null)
                {
                    if (leaseapplication.DocumentName.Count > 0 && leaseapplication.Mandatory.Count > 0)
                    {
                        for (int i = 0; i < leaseapplication.DocumentName.Count; i++)
                        {
                            if (leaseapplication.IsMandatory[i] == 1)
                            {
                                if(leaseapplication.FileUploaded == null || leaseapplication.FileUploaded.Count <= i)
                                {
                                    ViewBag.Message = Alert.Show("Please Upload Mandatory Documents", "", AlertType.Warning);
                                    return View(leaseapplication);

                                }
                            }
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    leaseapplication.CreatedBy = 1;
                    leaseapplication.ApprovedStatus = 0;
                    leaseapplication.PendingAt = 1;
                    leaseapplication.IsActive = 1;
                    leaseapplication.Id = 0;
                    var result = await _leaseApplicationFormService.Create(leaseapplication);

                    if (result)
                    {
                        //List<Leaseapplicationdocuments> leaseapplicationdocuments = new List<Leaseapplicationdocuments>();
                        //for (int i = 0; i < leaseapplication.DocumentChecklistId.Count; i++)
                        //{
                        //    string filename = null;
                        //    if (leaseapplication.FileUploaded != null && leaseapplication.FileUploaded.Count > 0)
                        //        filename = fileHelper.SaveFile1(FilePath, leaseapplication.FileUploaded[i]);
                        //    leaseapplicationdocuments.Add(new Leaseapplicationdocuments
                        //    {
                        //        DocumentChecklistId = leaseapplication.DocumentChecklistId.Count <= i ? 0 : leaseapplication.DocumentChecklistId[i],
                        //        LeaseApplicationId = leaseapplication.Id,
                        //        DocumentFileName = filename

                        //    });
                        //}
                        //if (leaseapplicationdocuments.Count > 0)
                        //    result = await _leaseApplicationFormService.SaveLeaseApplicationDocuments(leaseapplicationdocuments);
                        if (leaseapplication.DocumentName != null && leaseapplication.Mandatory != null)
                        {
                            if (leaseapplication.DocumentName.Count > 0 && leaseapplication.Mandatory.Count > 0)
                            {
                                List<Leaseapplicationdocuments> leaseapplicationdocuments = new List<Leaseapplicationdocuments>();
                                for (int i = 0; i < leaseapplication.DocumentName.Count; i++)
                                {
                                    if (leaseapplication.FileUploaded != null && leaseapplication.FileUploaded.Count > 0)
                                    {
                                        leaseapplicationdocuments.Add(new Leaseapplicationdocuments
                                        {
                                            DocumentChecklistId = leaseapplication.DocumentChecklistId.Count <= i ? 0 : leaseapplication.DocumentChecklistId[i],
                                            LeaseApplicationId = leaseapplication.Id,
                                            DocumentFileName = leaseapplication.FileUploaded != null ?
                                                               leaseapplication.FileUploaded.Count <= i ? string.Empty :
                                                               fileHelper.SaveFile1(FilePath, leaseapplication.FileUploaded[i]) :
                                                               leaseapplication.FileUploaded[i] != null || leaseapplication.FileUploadedPath[i] != "" ?
                                                               leaseapplication.FileUploadedPath[i] : string.Empty
                                        });
                                    }
                                }

                                if (leaseapplicationdocuments.Count > 0)
                                    result = await _leaseApplicationFormService.SaveLeaseApplicationDocuments(leaseapplicationdocuments);
                            }
                        }
                    }
                    if (result == true)
                    {
                        #region Approval Proccess At 1st level start Added by Renu 16 March 2021
                        var DataFlow = await dataAsync();
                        for (int i = 0; i < DataFlow.Count; i++)
                        {
                            if (!DataFlow[i].parameterSkip)
                            {
                                leaseapplication.ApprovedStatus = 0;
                                leaseapplication.PendingAt = Convert.ToInt32(DataFlow[i].parameterName);
                                result = await _leaseApplicationFormService.UpdateBeforeApproval(leaseapplication.Id, leaseapplication);  //Update Table details 
                                if (result)
                                {
                                    Approvalproccess approvalproccess = new Approvalproccess();
                                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                    approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessIdLeaseApplicationForm").Value);
                                    approvalproccess.ServiceId = leaseapplication.Id;
                                    approvalproccess.SendFrom = SiteContext.UserId;
                                    approvalproccess.SendTo = Convert.ToInt32(DataFlow[i].parameterName);
                                    approvalproccess.PendingStatus = 1;   //1
                                    approvalproccess.Status = null;   //1
                                    approvalproccess.Remarks = "Record Added and Send for Approval";///May be Uncomment
                                    result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                                }

                                break;
                            }
                        }

                        #endregion 

                        if (leaseapplication.EmailId != null)
                        {
                            #region Mail Generate
                            //At successfull completion send mail and sms
                            Uri uri = new Uri("http://localhost:1011/DamagePayeeRegistration");
                            string Action = "Dear " + leaseapplication.Name + ",  You are succesfully registered with DDA Portal. For verify your email click  below link :-  " + uri;
                            string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "RefMailDetails.html");

                            #region Mail Generation Added By Renu

                            MailSMSHelper mailG = new MailSMSHelper();

                            #region HTML Body Generation
                            LeaseRefBodyDto bodyDTO = new LeaseRefBodyDto();
                            bodyDTO.displayName = leaseapplication.Name;
                            bodyDTO.RefNo = leaseapplication.RefNo;
                            bodyDTO.link = Action;
                            bodyDTO.path = path;
                            string strBodyMsg = mailG.PopulateBodyLeaseRefernceNo(bodyDTO);
                            #endregion

                            string strMailSubject = "User Reference No. Details ";
                            string strMailCC = "", strMailBCC = "", strAttachPath = "";
                            var sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, leaseapplication.EmailId, strMailCC, strMailBCC, strAttachPath);
                            #endregion

                            if (sendMailResult)
                                ViewBag.Message = Alert.Show("Dear User,<br/>" + leaseapplication.Name + "  Your Request is sent for Approval and  Reference No.is send on your Registered email and Mobile No", "", AlertType.Success);
                            else
                                ViewBag.Message = Alert.Show("Dear User,<br/>" + leaseapplication.Name + "  Your Request is sent for Approval but Enable to send Reference No. on your mail or sms due to network issue", "", AlertType.Info);


                            #endregion
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.AddAndApprovalRecordSuccess, "", AlertType.Success);
                            TempData["Message"] = Alert.Show(Messages.AddAndApprovalRecordSuccess, "", AlertType.Success);
                        }

                        ViewBag.IsPrintable = 1;
                        ViewBag.Id = leaseapplication.Id;
                        return View("Create");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(leaseapplication);

                    }
                }
                else
                {
                    return View(leaseapplication);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(leaseapplication);
            }
        }
        public async Task<JsonResult> GetDocumentChecklistDetails()
        {
            var data = await _leaseApplicationFormService.GetDocumentChecklistDetails(Convert.ToInt32(_configuration.GetSection("ServiceTypeIdLeaseAppForm").Value));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.ServiceTypeId,
                x.IsMandatory
            }));
        }

        public async Task<IActionResult> Print(int id)
        {
            Leaseapplication leaseapplication = new Leaseapplication();
            var Data = await _leaseApplicationFormService.FetchLeaseApplicationDetails(id);
            Data.Leasedocuments = await _leaseApplicationFormService.LeaseApplicationDocumentDetails(id);
            if (Data != null)
            {
                return View("Print", Data);
            }
            else
            {
                return View("Print", leaseapplication);
            }

        }

        #region Fetch workflow data for approval prrocess Added by Renu 16 March 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResult(2);
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion
    }
}