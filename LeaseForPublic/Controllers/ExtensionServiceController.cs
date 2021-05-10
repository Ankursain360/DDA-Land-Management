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
using Service.IApplicationService;
using Dto.Master;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace LeaseForPublic.Controllers
{
    public class ExtensionServiceController : BaseController
    {
        private readonly IExtensionService _extensionService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserProfileService _userProfileService;
        private readonly IHostingEnvironment _hostingEnvironment;

        string targetPathExtensionDocuments = "";
        public ExtensionServiceController(IExtensionService extensionService,
            IConfiguration configuration, IWorkflowTemplateService workflowtemplateService,
            IApprovalProccessService approvalproccessService, IUserProfileService userProfileService, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _extensionService = extensionService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _userProfileService = userProfileService;
            _hostingEnvironment = hostingEnvironment;
            targetPathExtensionDocuments = _configuration.GetSection("FilePaths:Extension:ExtensionFilePath").Value.ToString();

        }

        public async Task<IActionResult> Index()
        {
            var data = await _extensionService.IsNeedAddMore();
            var approvalstatusresult = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Approved);
            int statusid = approvalstatusresult == null ? 0 : approvalstatusresult.Id;
            if (data == null || data.ApprovedStatus == statusid)
                ViewBag.IsNeedAddMore = 1;
            else
                ViewBag.IsNeedAddMore = 0;

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ExtensionServiceSearchDto model)
        {
            var result = await _extensionService.GetPagedExtensionServiceDetails(model);
            var approvalstatusresult = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Approved);
            ViewBag.ApprovedStatus = approvalstatusresult == null ? 0 : approvalstatusresult.Id;
            return PartialView("_List", result);
        }

        //   [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Extension extension = new Extension();
            extension.IsActive = 1;
            extension.Documentchecklist = await _extensionService.GetDocumentChecklistDetails(Convert.ToInt32(_configuration.GetSection("ServiceTypeIdExtensionService").Value));
            return View(extension);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Extension extension)
        {
            try
            {
                extension.IsActive = 1;
                extension.ServiceTypeId = 5;
                extension.Documentchecklist = await _extensionService.GetDocumentChecklistDetails(Convert.ToInt32(_configuration.GetSection("ServiceTypeIdExtensionService").Value));

                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    extension.CreatedBy = SiteContext.UserId;
                    extension.IsActive = 1;
                    extension.Id = 0;
                    Random r = new Random();
                    int num = r.Next();
                    extension.RefNo = DateTime.Now.Year.ToString() +  num;

                    #region Approval Proccess At 1st level Check Initial Before Creating Record  Added by Renu 03 May 2021

                    Approvalproccess approvalproccess = new Approvalproccess();
                    var DataFlow = await dataAsync();
                    for (int i = 0; i < DataFlow.Count; i++)
                    {
                        if (!DataFlow[i].parameterSkip)
                        {
                            if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                            {
                                if (SiteContext.ZoneId == null)
                                {
                                    ViewBag.Message = Alert.Show("Your Zone is not available , Without zone application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                    return View(extension);
                                }

                                extension.ApprovalZoneId = SiteContext.ZoneId;
                            }
                            if (DataFlow[i].parameterValue == (_configuration.GetSection("ApprovalRoleType").Value))
                            {
                                for (int j = 0; j < DataFlow[i].parameterName.Count; j++)
                                {
                                    List<UserProfileDto> UserListRoleBasis = null;
                                    if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                                        UserListRoleBasis = await _userProfileService.GetUserOnRoleZoneBasis(Convert.ToInt32(DataFlow[i].parameterName[j]), SiteContext.ZoneId ?? 0);
                                    else
                                        UserListRoleBasis = await _userProfileService.GetUserOnRoleBasis(Convert.ToInt32(DataFlow[i].parameterName[j]));

                                    StringBuilder multouserszonewise = new StringBuilder();
                                    int col = 0;
                                    if (UserListRoleBasis != null)
                                    {
                                        for (int h = 0; h < UserListRoleBasis.Count; h++)
                                        {
                                            if (col > 0)
                                                multouserszonewise.Append(",");
                                            multouserszonewise.Append(UserListRoleBasis[h].UserId);
                                            col++;
                                        }
                                        approvalproccess.SendTo = multouserszonewise.ToString();
                                    }

                                }
                            }
                            else
                            {
                                approvalproccess.SendTo = String.Join(",", (DataFlow[i].parameterName));
                                if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                                {
                                    StringBuilder multouserszonewise = new StringBuilder();
                                    int col = 0;
                                    if (approvalproccess.SendTo != null)
                                    {
                                        string[] multiTo = approvalproccess.SendTo.Split(',');
                                        foreach (string MultiUserId in multiTo)
                                        {
                                            if (col > 0)
                                                multouserszonewise.Append(",");
                                            var UserProfile = await _userProfileService.GetUserByIdZone(Convert.ToInt32(MultiUserId), SiteContext.ZoneId ?? 0);
                                            multouserszonewise.Append(UserProfile.UserId);
                                            col++;
                                        }
                                        approvalproccess.SendTo = multouserszonewise.ToString();
                                    }
                                }
                            }


                            break;
                        }
                    }
                    #endregion

                    var result = await _extensionService.Create(extension);

                    if (result)
                    {
                        List<Allotteeservicesdocument> allotteeservicesdocuments = new List<Allotteeservicesdocument>();
                        for (int i = 0; i < extension.DocumentChecklistId.Count; i++)
                        {
                            string filename = null;
                            if (extension.FileUploaded != null && extension.FileUploaded.Count > 0)
                                filename = extension.FileUploaded != null ?
                                                                   extension.FileUploaded.Count <= i ? string.Empty :
                                                                   fileHelper.SaveFile1(targetPathExtensionDocuments, extension.FileUploaded[i]) :
                                                                   extension.FileUploaded[i] != null || extension.FileUploadedPath[i] != "" ?
                                                                   extension.FileUploadedPath[i] : string.Empty;
                            allotteeservicesdocuments.Add(new Allotteeservicesdocument
                            {
                                DocumentChecklistId = extension.DocumentChecklistId.Count <= i ? 0 : extension.DocumentChecklistId[i],
                                ServiceId = extension.Id,
                                ServiceTypeId = extension.ServiceTypeId,
                                DocumentFileName = filename,
                                CreatedBy = SiteContext.UserId

                            });
                        }
                        if (allotteeservicesdocuments.Count > 0)
                            result = await _extensionService.SaveAllotteeServiceDocuments(allotteeservicesdocuments);

                    }
                    if (result == true)
                    {
                        #region Approval Proccess At 1st level start Added by Renu 03 May 2021
                        var workflowtemplatedata = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProcessGuidExtensionService").Value));
                        var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);

                        for (int i = 0; i < DataFlow.Count; i++)
                        {
                            if (!DataFlow[i].parameterSkip)
                            {
                                extension.ApprovedStatus = ApprovalStatus.Id;
                                extension.PendingAt = approvalproccess.SendTo;
                                result = await _extensionService.UpdateBeforeApproval(extension.Id, extension);  //Update Table details 
                                if (result)
                                {
                                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                    approvalproccess.ProcessGuid = (_configuration.GetSection("workflowProcessGuidExtensionService").Value);
                                    approvalproccess.ServiceId = extension.Id;
                                    approvalproccess.SendFrom = SiteContext.UserId.ToString();
                                    approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
                                    #region set sendto and sendtoprofileid 
                                    StringBuilder multouserprofileid = new StringBuilder();
                                    int col = 0;
                                    if (approvalproccess.SendTo != null)
                                    {
                                        string[] multiTo = approvalproccess.SendTo.Split(',');
                                        foreach (string MultiUserId in multiTo)
                                        {
                                            if (col > 0)
                                                multouserprofileid.Append(",");
                                            var UserProfile = await _userProfileService.GetUserById(Convert.ToInt32(MultiUserId));
                                            multouserprofileid.Append(UserProfile.Id);
                                            col++;
                                        }
                                        approvalproccess.SendToProfileId = multouserprofileid.ToString();
                                    }
                                    #endregion
                                    approvalproccess.PendingStatus = 1;   //1
                                    approvalproccess.Status = ApprovalStatus.Id;   //1
                                    approvalproccess.Level = i + 1;
                                    approvalproccess.Version = workflowtemplatedata.Version;
                                    approvalproccess.Remarks = "Record Added and Sent for Approval";///May be Uncomment
                                    result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                                }

                                break;
                            }
                        }

                        #endregion 

                        #region Approval Proccess  Mail Generation Added by Renu 07 May 2021
                        var sendMailResult = false;
                        var DataApprovalSatatusMsg = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalStatus.Id));
                        if (approvalproccess.SendTo != null)
                        {
                            #region Mail Generate
                            //At successfull completion send mail and sms
                            Uri uri = new Uri("https://master.managemybusinessess.com/ApprovalProcess/Index");
                            string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "ApprovalMailDetailsContent.html");
                            string link = "<a target=\"_blank\" href=\"" + uri + "\">Click Here</a>";
                            string linkhref = "https://master.managemybusinessess.com/ApprovalProcess/Index";

                            var senderUser = await _userProfileService.GetUserById(SiteContext.UserId);
                            StringBuilder multousermailId = new StringBuilder();
                            if (approvalproccess.SendTo != null)
                            {
                                int col = 0;
                                string[] multiTo = approvalproccess.SendTo.Split(',');
                                foreach (string MultiUserId in multiTo)
                                {
                                    if (col > 0)
                                        multousermailId.Append(",");
                                    var RecevierUsers = await _userProfileService.GetUserById(Convert.ToInt32(MultiUserId));
                                    multousermailId.Append(RecevierUsers.User.Email);
                                    col++;
                                }
                            }

                            #region Mail Generation Added By Renu

                            MailSMSHelper mailG = new MailSMSHelper();

                            #region HTML Body Generation
                            ApprovalMailBodyDto bodyDTO = new ApprovalMailBodyDto();
                            bodyDTO.ApplicationName = "Extension Application";
                            bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                            bodyDTO.SenderName = senderUser.User.Name;
                            bodyDTO.Link = linkhref;
                            bodyDTO.AppRefNo = extension.RefNo;
                            bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                            bodyDTO.Remarks = approvalproccess.Remarks;
                            bodyDTO.path = path;
                            string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                            #endregion

                            string strMailSubject = "Pending Extension Application Approval Request Details ";
                            string strMailCC = "", strMailBCC = "", strAttachPath = "";
                            sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                            #endregion


                            #endregion
                        }
                        #endregion

                        var data = await _extensionService.IsNeedAddMore();
                        var approvalstatusresult = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Approved);
                        int statusid = approvalstatusresult == null ? 0 : approvalstatusresult.Id;
                        if (data == null || data.ApprovedStatus == statusid)
                            ViewBag.IsNeedAddMore = 1;
                        else
                            ViewBag.IsNeedAddMore = 0;
                        ViewBag.Message = Alert.Show(Messages.AddAndApprovalRecordSuccess, "", AlertType.Success);
                        return View("Index", data);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(extension);

                    }
                }
                else
                {
                    return View(extension);
                }
            }
            catch (Exception ex)
            {
                #region Roll Back of Transaction Added by Renu 26 April  2021 
                var deleteResult = false;
                if (extension.Id != 0)
                {
                    deleteResult = await _approvalproccessService.RollBackEntry((_configuration.GetSection("workflowProcessGuidExtensionService").Value), extension.Id);
                    deleteResult = await _extensionService.RollBackEntryDocument(extension.Id, Convert.ToInt32(_configuration.GetSection("ServiceTypeIdExtensionService").Value));
                    deleteResult = await _extensionService.RollBackEntry(extension.Id);
                }
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(extension);
                #endregion
            }
        }

        // [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _extensionService.FetchSingleResult(id);
            var Msg = TempData["Message"] as string;
            if (Msg != null)
                ViewBag.Message = Msg;
            Data.IsActive = 1;
            Data.AllotteeservicesdocumentList = await _extensionService.AlloteeDocumentListDetails(id, Convert.ToInt32(_configuration.GetSection("ServiceTypeIdExtensionService").Value));

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Extension extension)
        {
            try
            {
                extension.ServiceTypeId = 5;
                extension.AllotteeservicesdocumentList = await _extensionService.AlloteeDocumentListDetails(id, Convert.ToInt32(_configuration.GetSection("ServiceTypeIdExtensionService").Value));
                var result = false;
                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    if (extension.EditPosition == "NotComplete")
                    {
                        var Data = await _extensionService.FetchSingleResultDocument(extension.EditDocumentId);
                        Allotteeservicesdocument allotteeservicesdocuments = new Allotteeservicesdocument();
                        string filename = null;
                        filename = extension.EditFileUploaded != null ?
                                    fileHelper.SaveFile1(targetPathExtensionDocuments, extension.EditFileUploaded) :
                                    extension.EditDocumentFileName;
                        allotteeservicesdocuments.DocumentChecklistId = Data.DocumentChecklistId;
                        allotteeservicesdocuments.ServiceId = extension.Id;
                        allotteeservicesdocuments.ServiceTypeId = Data.ServiceTypeId;
                        allotteeservicesdocuments.DocumentFileName = filename;
                        allotteeservicesdocuments.CreatedBy = SiteContext.UserId;
                        result = await _extensionService.UpdateAllotteeServiceDocuments(extension.EditDocumentId, allotteeservicesdocuments);



                        if (result)
                            ViewBag.Message = Alert.Show("Document Updated Successfully", "", AlertType.Success);
                        else
                            ViewBag.Message = Alert.Show("Enable to update Document, please try again", "", AlertType.Warning);

                        // return RedirectToAction("Edit", id);
                        //ViewBag.Message = Alert.Show("Document Updated Successfully", "", AlertType.Success);
                        return View(extension);
                    }
                    else
                    {
                        extension.ModifiedBy = SiteContext.UserId;
                        extension.IsActive = 1;
                        result = await _extensionService.Update(id, extension);

                        if (result)
                        {
                            List<Allotteeservicesdocument> allotteeservicesdocuments = new List<Allotteeservicesdocument>();
                            for (int i = 0; i < extension.DocumentChecklistId.Count; i++)
                            {
                                string filename = null;
                                if (extension.FileUploaded != null && extension.FileUploaded.Count > 0)
                                    filename = extension.FileUploaded != null ?
                                                                       extension.FileUploaded.Count <= i ? string.Empty :
                                                                       fileHelper.SaveFile1(targetPathExtensionDocuments, extension.FileUploaded[i]) :
                                                                       extension.FileUploaded[i] != null || extension.FileUploadedPath[i] != "" ?
                                                                       extension.FileUploadedPath[i] : string.Empty;
                                else
                                    filename = extension.DocumentName[i];
                                allotteeservicesdocuments.Add(new Allotteeservicesdocument
                                {
                                    DocumentChecklistId = extension.DocumentChecklistId.Count <= i ? 0 : extension.DocumentChecklistId[i],
                                    ServiceId = extension.Id,
                                    ServiceTypeId = extension.ServiceTypeId,
                                    DocumentFileName = filename,
                                    CreatedBy = SiteContext.UserId,
                                    Id = extension.AllotteeDocumentId[i]

                                });
                            }
                            foreach (var item in allotteeservicesdocuments)
                            {
                                if (item.Id != 0)
                                    result = await _extensionService.UpdateAllotteeServiceDocuments(item.Id, item);
                                else
                                    result = await _extensionService.SaveAllotteeServiceDocumentsSingle(item);

                            }
                        }
                        if (result == true)
                        {
                            var data = await _extensionService.IsNeedAddMore();
                            var approvalstatusresult = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Approved);
                            int statusid = approvalstatusresult == null ? 0 : approvalstatusresult.Id;
                            if (data == null || data.ApprovedStatus == statusid)
                                ViewBag.IsNeedAddMore = 1;
                            else
                                ViewBag.IsNeedAddMore = 0;
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            return View("Index", data);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(extension);

                        }
                    }

                }
                else
                {
                    return View(extension);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(extension);
            }
        }

        //   [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _extensionService.FetchSingleResult(id);
            Data.IsActive = 1;
            Data.AllotteeservicesdocumentList = await _extensionService.AlloteeDocumentListDetails(id, Convert.ToInt32(_configuration.GetSection("ServiceTypeIdExtensionService").Value));

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        // [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _extensionService.Delete(id, SiteContext.UserId);
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
            var data = await _extensionService.IsNeedAddMore();
            var approvalstatusresult = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Approved);
            int statusid = approvalstatusresult == null ? 0 : approvalstatusresult.Id;
            if (data == null || data.ApprovedStatus == statusid)
                ViewBag.IsNeedAddMore = 1;
            else
                ViewBag.IsNeedAddMore = 0;
            return View("Index", data);
        }

        public async Task<FileResult> ViewExtensionDocument(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _extensionService.FetchSingleResultDocument(Id);
            string targetPhotoPathLayout = targetPathExtensionDocuments + Data.DocumentFileName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
            return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
        }

        [HttpGet]
        public async Task<JsonResult> EditDocument(int DocumentId)
        {
            return Json(await _extensionService.FetchSingleResultDocument(DocumentId));
        }

        [HttpGet]
        public async Task<JsonResult> GetOtherData()
        {
            return Json(await _extensionService.GetAllotteeDetails(SiteContext.UserId));
        }

        [HttpGet]
        public async Task<JsonResult> GetTimeLineExtensionFees()
        {
            return Json(await _extensionService.GetTimeLineExtensionFees());
        }

        #region Fetch workflow data for approval prrocess Added by Renu 3 May 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProcessGuidExtensionService").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion
    }
}
