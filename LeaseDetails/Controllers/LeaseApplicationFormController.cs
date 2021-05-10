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
using Service.IApplicationService;
using System.Text;

namespace LeaseDetails.Controllers
{
    public class LeaseApplicationFormController : BaseController
    {

        private readonly ILeaseApplicationFormService _leaseApplicationFormService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserProfileService _userProfileService;

        public LeaseApplicationFormController(ILeaseApplicationFormService leaseApplicationFormService,
            IConfiguration configuration, IApprovalProccessService approvalproccessService,
            IWorkflowTemplateService workflowtemplateService, IHostingEnvironment hostingEnvironment,
            IUserProfileService userProfileService)
        {
            _leaseApplicationFormService = leaseApplicationFormService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
            _workflowtemplateService = workflowtemplateService;
            _hostingEnvironment = hostingEnvironment;
            _userProfileService = userProfileService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            var Msg = TempData["Message"] as string;
            if (Msg != null)
                ViewBag.Message = Msg;
            Leaseapplication leaseapplication = new Leaseapplication();
            leaseapplication.Documentchecklist = await _leaseApplicationFormService.GetDocumentChecklistDetails(Convert.ToInt32(_configuration.GetSection("ServiceTypeIdLeaseAppForm").Value));
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
                Random r = new Random();
                int num = r.Next();
                leaseapplication.RefNo = leaseapplication.RegistrationNo + num;
                string FilePath = _configuration.GetSection("FilePaths:LeaseApplicationForm:DocumentFilePath").Value.ToString();

                leaseapplication.Documentchecklist = await _leaseApplicationFormService.GetDocumentChecklistDetails(Convert.ToInt32(_configuration.GetSection("ServiceTypeIdLeaseAppForm").Value));
                //Is Mandatory check
                if (leaseapplication.DocumentName != null && leaseapplication.Mandatory != null)
                {
                    if (leaseapplication.DocumentName.Count > 0 && leaseapplication.Mandatory.Count > 0)
                    {
                        for (int i = 0; i < leaseapplication.DocumentName.Count; i++)
                        {
                            if (leaseapplication.IsMandatory[i] == 1)
                            {
                                if (leaseapplication.FileUploaded == null || leaseapplication.FileUploaded.Count <= i)
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
                    #region Approval Proccess At 1st level Check Initial Before Creating Record  Added by Renu 21 April 2021

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
                                    return View(leaseapplication);
                                }

                                leaseapplication.ApprovalZoneId = SiteContext.ZoneId;
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

                    FileHelper fileHelper = new FileHelper();
                    leaseapplication.CreatedBy = SiteContext.UserId;
                    //leaseapplication.ApprovedStatus = 0;
                    //leaseapplication.PendingAt = 1;
                    leaseapplication.IsActive = 1;
                    leaseapplication.Id = 0;
                    var result = await _leaseApplicationFormService.Create(leaseapplication);

                    if (result)
                    {
                        List<Leaseapplicationdocuments> leaseapplicationdocuments = new List<Leaseapplicationdocuments>();
                        for (int i = 0; i < leaseapplication.DocumentChecklistId.Count; i++)
                        {
                            string filename = null;
                            if (leaseapplication.FileUploaded != null && leaseapplication.FileUploaded.Count > 0)
                                filename = leaseapplication.FileUploaded != null ?
                                                                   leaseapplication.FileUploaded.Count <= i ? string.Empty :
                                                                   fileHelper.SaveFile1(FilePath, leaseapplication.FileUploaded[i]) :
                                                                   leaseapplication.FileUploaded[i] != null || leaseapplication.FileUploadedPath[i] != "" ?
                                                                   leaseapplication.FileUploadedPath[i] : string.Empty;
                            leaseapplicationdocuments.Add(new Leaseapplicationdocuments
                            {
                                DocumentChecklistId = leaseapplication.DocumentChecklistId.Count <= i ? 0 : leaseapplication.DocumentChecklistId[i],
                                LeaseApplicationId = leaseapplication.Id,
                                DocumentFileName = filename,
                                CreatedBy = SiteContext.UserId

                            });
                        }
                        if (leaseapplicationdocuments.Count > 0)
                            result = await _leaseApplicationFormService.SaveLeaseApplicationDocuments(leaseapplicationdocuments);

                    }
                    if (result == true)
                    {
                        #region Approval Proccess At 1st level start Added by Renu 21 April 2021
                        var workflowtemplatedata = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidLeaseApplicationForm").Value));
                        var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);

                        for (int i = 0; i < DataFlow.Count; i++)
                        {
                            if (!DataFlow[i].parameterSkip)
                            {
                                leaseapplication.ApprovedStatus = ApprovalStatus.Id;
                                leaseapplication.PendingAt = approvalproccess.SendTo;
                                result = await _leaseApplicationFormService.UpdateBeforeApproval(leaseapplication.Id, leaseapplication);  //Update Table details 
                                if (result)
                                {
                                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                    approvalproccess.ProcessGuid = (_configuration.GetSection("workflowPreccessGuidLeaseApplicationForm").Value);
                                    approvalproccess.ServiceId = leaseapplication.Id;
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
                                    approvalproccess.Remarks = "Record Added and Send for Approval";///May be Uncomment
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
                            bodyDTO.ApplicationName = "Lease Application";
                            bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                            bodyDTO.SenderName = senderUser.User.Name;
                            bodyDTO.Link = linkhref;
                            bodyDTO.AppRefNo = leaseapplication.RefNo;
                            bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                            bodyDTO.Remarks = approvalproccess.Remarks;
                            bodyDTO.path = path;
                            string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                            #endregion

                            string strMailSubject = "Pending Lease Application Approval Request Details ";
                            string strMailCC = "", strMailBCC = "", strAttachPath = "";
                            sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                            #endregion


                            #endregion
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
                            sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, leaseapplication.EmailId, strMailCC, strMailBCC, strAttachPath);
                            #endregion

                            if (sendMailResult)
                                ViewBag.Message = Alert.Show("Dear User,<br/>" + leaseapplication.Name + "  Your Request is sent for Approval and Related information is sent  on your Registered emailid and Mobile No", "", AlertType.Success);
                            else
                                ViewBag.Message = Alert.Show("Dear User,<br/>" + leaseapplication.Name + "  Your Request is sent for Approval but Unable to sent realted information on your emailid or Mobile No. due to network issue", "", AlertType.Info);


                            #endregion
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.AddAndApprovalRecordSuccess, "", AlertType.Success);
                            TempData["Message"] = Alert.Show(Messages.AddAndApprovalRecordSuccess, "", AlertType.Success);
                        }
                       
                        ViewBag.IsPrintable = 1;
                        ViewBag.Id = leaseapplication.Id;
                        return View("Create", leaseapplication);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(leaseapplication);

                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(leaseapplication);
                }
            }
            catch (Exception ex)
            {
                #region Roll Back of Transaction Added by Renu 26 April  2021 
                var deleteResult = false;
                if (leaseapplication.Id != 0)
                {
                    deleteResult = await _approvalproccessService.RollBackEntry((_configuration.GetSection("workflowPreccessGuidLeaseApplicationForm").Value), leaseapplication.Id);
                    deleteResult = await _leaseApplicationFormService.RollBackEntryDocument(leaseapplication.Id);
                    deleteResult = await _leaseApplicationFormService.RollBackEntry(leaseapplication.Id);
                }
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(leaseapplication);
                #endregion
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
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidLeaseApplicationForm").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion
    }
}