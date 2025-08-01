﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Enum;
using LeaseDetails.Filters;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Dto.Search;
using System.IO;
using Dto.Master;
using Service.IApplicationService;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


namespace LeaseDetails.Controllers
{
    public class InternalKycFormController : BaseController
    {
        private readonly IKycformService _kycformService;
        public IConfiguration _configuration;
        private readonly IUserProfileService _userProfileService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IHostingEnvironment _hostingEnvironment;
        string AadharDoc = "";
        string LetterDoc = "";
        string ApplicantDoc = "";

        public object JsonRequestBehavior { get; private set; }

        public InternalKycFormController(IConfiguration configuration,
            IUserNotificationService userNotificationService,
           IKycformService KycformService,
           IUserProfileService userProfileService,
            IApprovalProccessService approvalproccessService,
            IHostingEnvironment hostingEnvironment)

        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _kycformService = KycformService;
            _userNotificationService = userNotificationService;
            AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
            LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
            ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();
            _userProfileService = userProfileService;
            _approvalproccessService = approvalproccessService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] KycformSearchDto model)
        {
            var result = await _kycformService.GetPagedKycformForInternalUser(model);
            return PartialView("_List", result);

        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
                Kycform kyc = new Kycform();
              
                kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();

                kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
                // kyc.BranchList = await _kycformService.GetAllBranchList();
                kyc.BranchList = await _kycformService.GetAllBranch(kyc.PropertyTypeId);
                kyc.ZoneList = await _kycformService.GetAllZoneList();
                kyc.LocalityList = await _kycformService.GetLocalityList(kyc.ZoneId);
                return View(kyc);         

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Kycform kyc)
        {
            try
            {

                kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();
                //kyc.BranchList = await _kycformService.GetAllBranchList();
                kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
                kyc.BranchList = await _kycformService.GetAllBranch(kyc.PropertyTypeId);
                kyc.ZoneList = await _kycformService.GetAllZoneList();
                kyc.LocalityList = await _kycformService.GetLocalityList(kyc.ZoneId);
                string AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
                string LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
                string ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();
                if (ModelState.IsValid)
                {

                                #region Approval Proccess At 1st level Check Initial Before Creating Record

                                Kycapprovalproccess approvalproccess = new Kycapprovalproccess();
                                var DataFlow = await dataAsync();
                                for (int i = 0; i < DataFlow.Count; i++)
                                {
                                    if (!DataFlow[i].parameterSkip)
                                    {
                                        if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                                        {
                                            if (kyc.BranchId == null)
                                            {
                                                ViewBag.Message = Alert.Show("Your Branch is not available , Without branch application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                                return View(kyc);
                                            }

                                            // leaseapplication.ApprovalZoneId = SiteContext.ZoneId;
                                        }
                                        if (DataFlow[i].parameterValue == (_configuration.GetSection("ApprovalRoleType").Value))
                                        {
                                            for (int j = 0; j < DataFlow[i].parameterName.Count; j++)
                                            {
                                                List<UserProfileDto> UserListRoleBasis = null;
                                                if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                                                    UserListRoleBasis = await _userProfileService.GetUserOnRoleBranchBasis(Convert.ToInt32(DataFlow[i].parameterName[j]), kyc.BranchId ?? 0);
                                                else
                                                    UserListRoleBasis = await _userProfileService.GetUserOnRoleBasis(Convert.ToInt32(DataFlow[i].parameterName[j]));
                                                if (UserListRoleBasis.Count == 0)
                                                {
                                                    ViewBag.Message = Alert.Show("No User is available for selected Branch , Without User application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                                    return View(kyc);
                                                }
                                                else
                                                {
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
                                        }
                                        else
                                        {
                                            approvalproccess.SendTo = String.Join(",", (DataFlow[i].parameterName));
                                            if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                                            {
                                                StringBuilder multousersbranchwise = new StringBuilder();
                                                int col = 0;
                                                if (approvalproccess.SendTo != null)
                                                {
                                                    string[] multiTo = approvalproccess.SendTo.Split(',');
                                                    foreach (string MultiUserId in multiTo)
                                                    {
                                                        if (col > 0)
                                                            multousersbranchwise.Append(",");
                                                        var UserProfile = await _userProfileService.GetUserByIdBranch(Convert.ToInt32(MultiUserId), kyc.BranchId ?? 0);
                                                        multousersbranchwise.Append(UserProfile.UserId);
                                                        col++;
                                                    }
                                                    approvalproccess.SendTo = multousersbranchwise.ToString();
                                                }
                                            }
                                        }


                                        break;
                                    }
                                }
                                #endregion



                                FileHelper fileHelper = new FileHelper();

                                if (kyc.Aadhar != null)
                                {
                                    kyc.AadhaarNoPath = fileHelper.SaveFile1(AadharDoc, kyc.Aadhar);
                                }
                                if (kyc.Letter != null)
                                {
                                    kyc.LetterPath = fileHelper.SaveFile1(LetterDoc, kyc.Letter);
                                }
                                if (kyc.ApplicantPan != null)
                                {
                                    kyc.AadhaarPanapplicantPath = fileHelper.SaveFile1(ApplicantDoc, kyc.ApplicantPan);
                                }

                             kyc.CreatedBy = SiteContext.UserId;
                                kyc.IsActive = 1;
                                kyc.KycStatus = "F";
                                var result = await _kycformService.Create(kyc);
                                if (result == true)
                                {

                                    #region Approval Proccess At 1st level start Added by ishu  20 july 2021
                                    var workflowtemplatedata = await _kycformService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProccessGuidKYCForm").Value));

                                    var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);

                                    for (int i = 0; i < DataFlow.Count; i++)
                                    {
                                        if (!DataFlow[i].parameterSkip)
                                        {
                                            kyc.ApprovedStatus = ApprovalStatus.Id;
                                            kyc.PendingAt = approvalproccess.SendTo;
                                            result = await _kycformService.UpdateBeforeApproval(kyc.Id, kyc);  //Update kycform Table details 
                                            if (result)
                                            {
                                                approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                                approvalproccess.ProcessGuid = (_configuration.GetSection("workflowProccessGuidKYCForm").Value);
                                                approvalproccess.ServiceId = kyc.Id;
                                                //approvalproccess.SendFrom = SiteContext.UserId.ToString();
                                                //approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
                                                approvalproccess.SendFrom = kyc.Id.ToString();
                                                approvalproccess.SendFromProfileId = "0";
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
                                                approvalproccess.Remarks = "Applicant Submitted For E-KYC Approval";///May be Uncomment
                                                //result = await _kycformService.CreatekycApproval(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                                                result = await _kycformService.CreatekycApproval(approvalproccess, kyc.Id); //Create a row in kycapprovalproccess Table

                                                #region Insert Into usernotification table Added By Renu 18 June 2021
                                                if (result)
                                                {
                                                    var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidKYCForm").Value);
                                                    //   var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                                    Usernotification usernotification = new Usernotification();
                                                    var replacement = notificationtemplate.Template.Replace("{proccess name}", "KYC Form").Replace("{from user}", kyc.Name).Replace("{datetime}", DateTime.Now.ToString());
                                                    usernotification.Message = replacement;
                                                    usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidKYCForm").Value);
                                                    usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                                    usernotification.ServiceId = approvalproccess.ServiceId;
                                                    usernotification.SendFrom = approvalproccess.SendFrom;
                                                    usernotification.SendTo = approvalproccess.SendTo;
                                                    //result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                                                    result = await _userNotificationService.Create(usernotification, kyc.Id);
                                                }
                                                #endregion


                                                if (result)//mail sent to applicant on successful submission of form
                                                {
                                                    var sendMailResult1 = false;
                                                    #region Mail Generate
                                                    //At successfull completion send mail and sms
                                                    Uri uri = new Uri("https://master.managemybusinessess.com/ApprovalProcess/Index");//this is correct
                                                    string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "ApplicantKycMailDetailsContent.html");
                                                    string link = "<a target=\"_blank\" href=\"" + uri + "\">Click Here</a>";
                                                    // string linkhref = "https://leaseallottee.managemybusinessess.com/PaymentofProperties/Create";
                                                    string linkhref = _configuration.GetSection("KycPaymentLink").Value;
                                                    // var senderUser = await _userProfileService.GetUserById(SiteContext.UserId);


                                                    #region Mail Generation Added By ishu

                                                    MailSMSHelper mailG = new MailSMSHelper();

                                                    #region HTML Body Generation
                                                    KycApplicantMailBodyDto bodyDTO = new KycApplicantMailBodyDto();
                                                    bodyDTO.ApplicantName = kyc.Name;
                                                    bodyDTO.Remarks = "Your KYC Application with Reference No.- " + kyc.Id + "  and File No - " + kyc.FileNo + "  is successfully sent for E-KYC Approval. To Check Application details you can login into the portal by clicking on the click here link below.";
                                                    bodyDTO.Link = linkhref;
                                                    bodyDTO.path = path;
                                                    string strBodyMsg = mailG.PopulateBodyApplicantMailDetails(bodyDTO);
                                                    #endregion

                                                    #region Common Mail Genration
                                                    SentMailGenerationDto maildto = new SentMailGenerationDto();
                                                    maildto.strMailSubject = "Applicant submitted for E-KYC Approval ";
                                                    maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
                                                    maildto.strBodyMsg = strBodyMsg;
                                                    maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
                                                    maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
                                                    maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
                                                    maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
                                                    maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

                                                    maildto.strMailTo = kyc.EmailId; /*multousermailId.ToString();*/
                                                    sendMailResult1 = mailG.SendMailWithAttachment(maildto);
                                                    #endregion
                                                    #endregion


                                                    #endregion
                                                }
                                            }

                                            break;
                                        }
                                    }

                                    #endregion
                                    #region Approval Proccess  Mail Generation Added by ishu 11sept 2021
                                    var sendMailResult = false;
                                    var DataApprovalSatatusMsg = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalStatus.Id));
                                    if (approvalproccess.SendTo != null)
                                    {
                                        #region Mail Generate
                                        //At successfull completion send mail and sms
                                        Uri uri = new Uri("https://master.managemybusinessess.com/ApprovalProcess/Index");
                                        string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "ApprovalMailDetailsContent.html");
                                        string link = "<a target=\"_blank\" href=\"" + uri + "\">Click Here</a>";
                                        string linkhref = (_configuration.GetSection("ApprovalProccessPath:SiteMaster").Value).ToString();

                                        //  var senderUser = await _userProfileService.GetUserById(SiteContext.UserId);
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

                                        #region Mail Generation Added By ishu

                                        MailSMSHelper mailG = new MailSMSHelper();

                                        #region HTML Body Generation
                                        ApprovalMailBodyDto bodyDTO = new ApprovalMailBodyDto();
                                        bodyDTO.ApplicationName = "KYC Approval";
                                        bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                                        bodyDTO.SenderName = kyc.Name;
                                        // bodyDTO.SenderName = senderUser.User.Name;
                                        bodyDTO.Link = linkhref;
                                        bodyDTO.AppRefNo = kyc.Id.ToString();
                                        bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                                        bodyDTO.Remarks = approvalproccess.Remarks;
                                        bodyDTO.path = path;
                                        string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                                        #endregion

                                        //string strMailSubject = "Pending Lease Application Approval Request Details ";
                                        //string strMailCC = "", strMailBCC = "", strAttachPath = "";
                                        //sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                                        #region Common Mail Genration
                                        SentMailGenerationDto maildto = new SentMailGenerationDto();
                                        maildto.strMailSubject = "Pending KYC Approval Request Details ";
                                        maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
                                        maildto.strBodyMsg = strBodyMsg;
                                        maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
                                        maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
                                        maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
                                        maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
                                        maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

                                        maildto.strMailTo = multousermailId.ToString();
                                        sendMailResult = mailG.SendMailWithAttachment(maildto);
                                        #endregion
                                        #endregion


                                        #endregion
                                    }
                                    #endregion

                                    ViewBag.Message = Alert.Show("Applicant Submitted For E-KYC Approval", "", AlertType.Success);

                                    var list = await _kycformService.GetAllKycform();
                                    return View("Index", list);
                                }
                                else
                                {
                                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                                    return View(kyc);

                                }                       
                        
                  
                }
                else
                {
                    return View(kyc);
                }
            }
            catch (Exception ex)
            {
                #region Roll Back of Transaction Added by Renu 26 April  2021 
                var deleteResult = false;
                if (kyc.Id != 0)
                {

                    deleteResult = await _userNotificationService.RollBackEntry((_configuration.GetSection("workflowProccessGuidKYCForm").Value), kyc.Id);
                    deleteResult = await _approvalproccessService.KycRollBackEntry((_configuration.GetSection("workflowProccessGuidKYCForm").Value), kyc.Id);
                    deleteResult = await _kycformService.RollBackEntry(kyc.Id);
                }
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(kyc);
                #endregion
            }

        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _kycformService.FetchSingleResult(id);
            Data.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            // Data.BranchList = await _kycformService.GetAllBranchList();
            Data.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            Data.ZoneList = await _kycformService.GetAllZoneList();
            Data.LocalityList = await _kycformService.GetLocalityList(Data.ZoneId);
            Data.BranchList = await _kycformService.GetAllBranch(Data.PropertyTypeId);
            var email = HttpContext.Session.GetString("Email");
            var name = HttpContext.Session.GetString("Name");
            ViewBag.Title = name;
            ViewBag.Title1 = email;

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Kycform kyc)
        {
         
            var email = HttpContext.Session.GetString("Email");
            var name = HttpContext.Session.GetString("Name");
            ViewBag.Title = name;
            ViewBag.Title1 = email;
            var Data = await _kycformService.FetchSingleResult(id);
            kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();         
            kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            kyc.BranchList = await _kycformService.GetAllBranch(kyc.PropertyTypeId);
            kyc.ZoneList = await _kycformService.GetAllZoneList();
            kyc.LocalityList = await _kycformService.GetLocalityList(kyc.ZoneId);
            string AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
            string LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
            string ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();


            if (ModelState.IsValid)
            {
                
                            FileHelper fileHelper = new FileHelper();

                            if (kyc.Aadhar != null)
                            {
                                kyc.AadhaarNoPath = fileHelper.SaveFile1(AadharDoc, kyc.Aadhar);
                            }
                            else
                            {
                                kyc.AadhaarNoPath = Data.AadhaarNoPath;
                            }

                            if (kyc.Letter != null)
                            {
                                kyc.LetterPath = fileHelper.SaveFile1(LetterDoc, kyc.Letter);
                            }
                            else
                            {
                                kyc.LetterPath = Data.LetterPath;
                            }
                            if (kyc.ApplicantPan != null)
                            {
                                kyc.AadhaarPanapplicantPath = fileHelper.SaveFile1(ApplicantDoc, kyc.ApplicantPan);
                            }
                            else
                            {
                                kyc.AadhaarPanapplicantPath = Data.AadhaarPanapplicantPath;
                            }

                            kyc.IsActive = 1;
                            kyc.ModifiedBy = kyc.Id;
                            var result = await _kycformService.Update(id, kyc);
                            if (result == true)
                            {
                                Kycapprovalproccess approvalproccess = new Kycapprovalproccess();
                                var DataFlow = await dataAsync();

                                #region Resubmitting form Approval Proccess At last level start Added by ishu  27 july 2021

                                var workflowtemplatedata = await _kycformService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProccessGuidKYCForm").Value));

                                var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);

                                for (int i = 0; i < DataFlow.Count; i++)
                                {
                                    if (!DataFlow[i].parameterSkip)
                                    {

                                        var CheckLastUserForRevert = await _approvalproccessService.KycUserResubmitForApproval((_configuration.GetSection("workflowProccessGuidKYCForm").Value), kyc.Id, Convert.ToInt32(DataFlow[i].parameterLevel));
                                        approvalproccess.SendTo = CheckLastUserForRevert.SendTo;
                                        // approvalproccess.Level = Convert.ToInt32(DataFlow[d].parameterLevel);
                                        approvalproccess.Level = 3;


                                        kyc.ApprovedStatus = ApprovalStatus.Id;
                                        kyc.PendingAt = approvalproccess.SendTo;
                                        result = await _kycformService.UpdateBeforeApproval(kyc.Id, kyc);  //Update kycform Table details 
                                        if (result)
                                        {


                                            /* Update last record pending status in Approval Process Table*/
                                            var ApprovalProccessBackId = _approvalproccessService.GetPreviouskycApprovalId((_configuration.GetSection("workflowProccessGuidKYCForm").Value), kyc.Id);
                                            approvalproccess.PendingStatus = 0;
                                            result = await _approvalproccessService.UpdatePreviouskycApprovalProccess(ApprovalProccessBackId, approvalproccess, kyc.Id);
                                            /*end of Code*/


                                            approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                            approvalproccess.ProcessGuid = (_configuration.GetSection("workflowProccessGuidKYCForm").Value);
                                            approvalproccess.ServiceId = kyc.Id;

                                            approvalproccess.SendFrom = kyc.Id.ToString();
                                            approvalproccess.SendFromProfileId = "0";
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
                                                                                           //approvalproccess.Level = i + 1;
                                            approvalproccess.Version = workflowtemplatedata.Version;
                                            approvalproccess.Remarks = "Record Resubmitted by Applicant";///May be Uncomment
                                            //result = await _kycformService.CreatekycApproval(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                                            result = await _kycformService.CreatekycApproval(approvalproccess, kyc.Id); //Create a row in approvalproccess Table

                                            #region Insert Into usernotification table Added By ishu 18 June 2021
                                            if (result)
                                            {
                                                var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidKYCForm").Value);
                                                // var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                                Usernotification usernotification = new Usernotification();
                                                var replacement = notificationtemplate.Template.Replace("{proccess name}", "KYC Form").Replace("{from user}", kyc.Name == "" ? "Applicant" : kyc.Name).Replace("{datetime}", DateTime.Now.ToString());
                                                usernotification.Message = replacement;
                                                usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidKYCForm").Value);
                                                usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                                usernotification.ServiceId = approvalproccess.ServiceId;
                                                usernotification.SendFrom = approvalproccess.SendFrom;
                                                usernotification.SendTo = approvalproccess.SendTo;
                                                //result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                                                result = await _userNotificationService.Create(usernotification, kyc.Id);
                                            }
                                            #endregion
                                        }

                                        break;
                                    }
                                }

                                #endregion

                                ViewBag.Message = Alert.Show(Messages.UpdateAndApprovalRecordSuccess, "", AlertType.Success);

                                var list = await _kycformService.GetAllKycform();
                                return View("Index", list);
                            }
               
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(kyc);

            }

            return View(kyc);
        }

        [AuthorizeContext(ViewAction.Delete)]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _kycformService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _kycformService.GetAllKycform();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _kycformService.GetAllKycform();
                return View("Index", result1);
            }
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var email = HttpContext.Session.GetString("Email");
            var name = HttpContext.Session.GetString("Name");
            ViewBag.Title = name;
            ViewBag.Title1 = email;
            var Data = await _kycformService.FetchSingleResult(id);
            Data.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            //Data.BranchList = await _kycformService.GetAllBranchList();
            Data.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            Data.ZoneList = await _kycformService.GetAllZoneList();
            Data.BranchList = await _kycformService.GetAllBranch(Data.PropertyTypeId);
            Data.LocalityList = await _kycformService.GetLocalityList(Data.ZoneId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        //***************** Download Files  ********************

        public async Task<IActionResult> DownloadAadharNo(int Id)
        {
            FileHelper file = new FileHelper();
            Kycform Data = await _kycformService.FetchSingleResult(Id);
            string filename = AadharDoc + Data.AadhaarNoPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadLetter(int Id)
        {
            FileHelper file = new FileHelper();
            Kycform Data = await _kycformService.FetchSingleResult(Id);
            string filename = LetterDoc + Data.LetterPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadPANofApplicant(int Id)
        {
            FileHelper file = new FileHelper();
            Kycform Data = await _kycformService.FetchSingleResult(Id);
            string filename = ApplicantDoc + Data.AadhaarPanapplicantPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }


        //get kycworkflowtemplate table data
        #region Fetch workflow data for approval prrocess Added by Renu 16 March 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _kycformService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProccessGuidKYCForm").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion

        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? zoneid)
        {
            zoneid = zoneid ?? 0;
            return Json(await _kycformService.GetLocalityList(Convert.ToInt32(zoneid)));
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> KycFormDetailsList()
        {
            var mobile = HttpContext.Session.GetString("Mobile");
            var result = await _kycformService.GetAlldownloadKycform(mobile);
            List<KycformListDto> data = new List<KycformListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new KycformListDto()
                    {
                        Id = result[i].Id,
                        PlotNo = result[i].PlotNo,
                        Area = result[i].Area == null ? "" : result[i].Area.ToString(),
                        PropertyType = result[i].Property == null ? "" : result[i].Property.ToString(),
                        NatureOfProperty = result[i].PropertyType == null ? "" : result[i].PropertyType.Name.ToString(),
                        FileNo = result[i].FileNo,
                        Branch = result[i].Branch == null ? "" : result[i].Branch.Name.ToString(),
                        Zone = result[i].Zone == null ? "" : result[i].Zone.Name.ToString(),

                        DateofAllotmentLetter = Convert.ToDateTime(result[i].AllotmentLetterDate).ToString("dd-MMM-yyyy"),
                        DateofPossession = Convert.ToDateTime(result[i].PossessionDate).ToString("dd-MMM-yyyy"),
                        //Status = result[i].ApprovedStatus == 1 ? se : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }



        public IActionResult Logout()
        {
            HttpContext.Session.SetString("Mobile", "");
            HttpContext.Session.SetString("Email", "");

            ViewBag.Message = Alert.Show("Logout Successfully", "", AlertType.Warning);


            // return RedirectToAction("Create", "SignupForm");
            return View("../SignupForm/CreateLogin");
        }

        [HttpGet]
        public async Task<JsonResult> GetBranchList(int? propertyTypeId)
        {
            propertyTypeId = propertyTypeId ?? 0;
            return Json(await _kycformService.GetAllBranch(Convert.ToInt32(propertyTypeId)));
        }



      

    }
}
