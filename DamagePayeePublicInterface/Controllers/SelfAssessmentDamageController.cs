using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System.Text;
using Service.IApplicationService;
using Dto.Master;
using Core.Enum;
using System.IO;
using Microsoft.AspNetCore.Hosting;



using Microsoft.AspNetCore.Http;



namespace DamagePayeePublicInterface.Controllers
{
    public class SelfAssessmentDamageController : BaseController
    {
        private readonly ISelfAssessmentDamageService _selfAssessmentDamageService;
        public IConfiguration _configuration;
        string DocumentFilePath = "";
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserProfileService _userProfileService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public object JsonRequestBehavior { get; private set; }
        public SelfAssessmentDamageController(ISelfAssessmentDamageService selfAssessmentDamageService,
            IConfiguration configuration, IHostingEnvironment en, IApprovalProccessService approvalproccessService,
            IWorkflowTemplateService workflowtemplateService, IUserProfileService userProfileService,
            IUserNotificationService userNotificationService)
        {
            _configuration = configuration;
            _selfAssessmentDamageService = selfAssessmentDamageService;
            _approvalproccessService = approvalproccessService;
            _workflowtemplateService = workflowtemplateService;
            _userProfileService = userProfileService;
            _userNotificationService = userNotificationService;
            _hostingEnvironment = en;
            DocumentFilePath = _configuration.GetSection("FilePaths:SA:DocumentFIlePath").Value.ToString();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagepayeeregistertempSearchDto model)
        {
            var result = await _selfAssessmentDamageService.GetPagedDamagepayeeregister(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Damagepayeeregister damagepayeeregistertemp)
        {
            damagepayeeregistertemp.LocalityList = await _selfAssessmentDamageService.GetLocalityList();
            damagepayeeregistertemp.DistrictList = await _selfAssessmentDamageService.GetDistrictList();
        }
        public async Task<IActionResult> Create()
        {
            Damagepayeeregister damagepayeeregistertemp = new Damagepayeeregister();
            var Data = await _selfAssessmentDamageService.FetchSelfAssessmentUserId(SiteContext.UserId);
            var value = await _selfAssessmentDamageService.GetRebateValue();
            if (value == null)
                ViewBag.RebateValue = 0;
            else
                ViewBag.RebateValue = Math.Round((value.RebatePercentage), 2);
            if (Data != null)
            {
                ViewBag.MainDamagePayeeId = Data.Id;
                await BindDropDown(Data);
                return View(Data);
            }
            else
            {
                ViewBag.MainDamagePayeeId = 0;
                await BindDropDown(damagepayeeregistertemp);
                return View(damagepayeeregistertemp);
            }

        }
        [HttpPost]

        public async Task<IActionResult> Create(Damagepayeeregister damagepayeeregistertemp)
        {
            await BindDropDown(damagepayeeregistertemp);
            string PhotoFilePathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:ATSGPADocument").Value.ToString();
            string RecieptDocumentPathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:RecieptDocument").Value.ToString();
            string AadharNoDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:AadharNoDocument").Value.ToString();
            string PanNoDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:PanNoDocument").Value.ToString();
            string PhotographPersonelDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:PhotographPersonelDocument").Value.ToString();
            string SignaturePersonelDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:SignaturePersonelDocument").Value.ToString();
            //string OtherDocDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:OtherDocument").Value.ToString();

            string PropertyPhotographLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:PropertyPhotograph").Value.ToString();
            string ShowCauseNoticeDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:ShowCauseNotice").Value.ToString();
            string FGFormDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:FGForm").Value.ToString();
            string BillDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:Bill").Value.ToString();
            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                damagepayeeregistertemp.DocumentName = damagepayeeregistertemp.DocumentIFormFile == null ? damagepayeeregistertemp.DocumentName : fileHelper.SaveFile1(DocumentFilePath, damagepayeeregistertemp.DocumentIFormFile);
                if (damagepayeeregistertemp.PropertyPhoto != null)
                {
                    damagepayeeregistertemp.PropertyPhotoPath = fileHelper.SaveFile(PropertyPhotographLayout, damagepayeeregistertemp.PropertyPhoto);
                }
                if (damagepayeeregistertemp.ShowCauseNotice != null)
                {
                    damagepayeeregistertemp.ShowCauseNoticePath = fileHelper.SaveFile(ShowCauseNoticeDocument, damagepayeeregistertemp.ShowCauseNotice);
                }
                if (damagepayeeregistertemp.Fgform != null)
                {
                    damagepayeeregistertemp.FgformPath = fileHelper.SaveFile(FGFormDocument, damagepayeeregistertemp.Fgform);
                }
                if (damagepayeeregistertemp.DocumentForFile != null)
                {
                    damagepayeeregistertemp.DocumentForFilePath = fileHelper.SaveFile(BillDocument, damagepayeeregistertemp.DocumentForFile);
                }
                damagepayeeregistertemp.UserId = SiteContext.UserId;
                if (damagepayeeregistertemp.Id == 0)
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
                                    return View(damagepayeeregistertemp);
                                }

                                damagepayeeregistertemp.ApprovalZoneId = SiteContext.ZoneId;
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

                    damagepayeeregistertemp.CreatedBy = SiteContext.UserId;
                    var result = await _selfAssessmentDamageService.Create(damagepayeeregistertemp);
                    if (result)
                    {
                        //****** code for saving  Damage payee personal info *****
                        if (damagepayeeregistertemp.payeeName != null &&
                          damagepayeeregistertemp.Gender != null &&
                          damagepayeeregistertemp.Address != null &&
                          damagepayeeregistertemp.MobileNo != null)
                        {
                            if (damagepayeeregistertemp.payeeName.Count > 0 &&
                        damagepayeeregistertemp.Gender.Count > 0 &&
                        damagepayeeregistertemp.Address.Count > 0 &&
                        damagepayeeregistertemp.MobileNo.Count > 0)
                            {
                                List<Damagepayeepersonelinfo> damagepayeepersonelinfotemp = new List<Damagepayeepersonelinfo>();
                                for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                                {
                                    damagepayeepersonelinfotemp.Add(new Damagepayeepersonelinfo
                                    {
                                        Name = damagepayeeregistertemp.payeeName.Count <= i ? string.Empty : damagepayeeregistertemp.payeeName[i],
                                        FatherName = damagepayeeregistertemp.payeeFatherName.Count <= i ? string.Empty : damagepayeeregistertemp.payeeFatherName[i],
                                        Gender = damagepayeeregistertemp.Gender.Count <= i ? "1" : damagepayeeregistertemp.Gender[i],
                                        Address = damagepayeeregistertemp.Address.Count <= i ? string.Empty : damagepayeeregistertemp.Address[i],
                                        MobileNo = damagepayeeregistertemp.MobileNo.Count <= i ? string.Empty : damagepayeeregistertemp.MobileNo[i],
                                        EmailId = damagepayeeregistertemp.EmailId.Count <= i ? string.Empty : damagepayeeregistertemp.EmailId[i],
                                        DamagePayeeRegisterTempId = damagepayeeregistertemp.Id,
                                        AadharNo = damagepayeeregistertemp.AadharNo.Count <= i ? string.Empty : damagepayeeregistertemp.AadharNo[i],
                                        PanNo = damagepayeeregistertemp.PanNo.Count <= i ? string.Empty : damagepayeeregistertemp.PanNo[i],
                                        AadharNoFilePath = damagepayeeregistertemp.Aadhar != null ?
                                                                damagepayeeregistertemp.Aadhar.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(AadharNoDocument, damagepayeeregistertemp.Aadhar[i]) :
                                                                damagepayeeregistertemp.AadharNoFilePath[i] != null || damagepayeeregistertemp.AadharNoFilePath[i] != "" ?
                                                                damagepayeeregistertemp.AadharNoFilePath[i] : string.Empty,
                                        PanNoFilePath = damagepayeeregistertemp.Pan != null ?
                                                                damagepayeeregistertemp.Pan.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PanNoDocument, damagepayeeregistertemp.Pan[i]) :
                                                                damagepayeeregistertemp.PanNoFilePath[i] != null || damagepayeeregistertemp.PanNoFilePath[i] != "" ?
                                                                damagepayeeregistertemp.PanNoFilePath[i] : string.Empty,
                                        PhotographPath = damagepayeeregistertemp.Photograph != null ?
                                                                damagepayeeregistertemp.Photograph.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotographPersonelDocument, damagepayeeregistertemp.Photograph[i]) :
                                                                damagepayeeregistertemp.PhotographFilePath[i] != null || damagepayeeregistertemp.PhotographFilePath[i] != "" ?
                                                                damagepayeeregistertemp.PhotographFilePath[i] : string.Empty,
                                        SignaturePath = damagepayeeregistertemp.SignatureFile != null ?
                                                                damagepayeeregistertemp.SignatureFile.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(SignaturePersonelDocument, damagepayeeregistertemp.SignatureFile[i]) :
                                                                damagepayeeregistertemp.SignatureFilePath[i] != null || damagepayeeregistertemp.SignatureFilePath[i] != "" ?
                                                                damagepayeeregistertemp.SignatureFilePath[i] : string.Empty,
                                        //OtherDocPath = damagepayeeregistertemp.OtherDocFile != null ?
                                        //                            damagepayeeregistertemp.OtherDocFile.Count <= i ? string.Empty :
                                        //                            fileHelper.SaveFile(OtherDocDocument, damagepayeeregistertemp.OtherDocFile[i]) :
                                        //                            damagepayeeregistertemp.OtherDocFilePath[i] != null || damagepayeeregistertemp.OtherDocFilePath[i] != "" ?
                                        //                            damagepayeeregistertemp.OtherDocFilePath[i] : string.Empty


                                    });
                                }
                                foreach (var item in damagepayeepersonelinfotemp)
                                {
                                    result = await _selfAssessmentDamageService.SavePayeePersonalInfoTemp(item);
                                }
                            }
                        }

                        //****** code for saving  Allotte Type *****
                        if (damagepayeeregistertemp.Name != null &&
                         damagepayeeregistertemp.FatherName != null &&
                         damagepayeeregistertemp.Date != null)
                        {
                            if (
                             damagepayeeregistertemp.Name.Count > 0 &&
                             damagepayeeregistertemp.FatherName.Count > 0 &&
                             damagepayeeregistertemp.Date.Count > 0
                             )
                            {
                                List<Allottetype> allottetypetemp = new List<Allottetype>();
                                for (int i = 0; i < damagepayeeregistertemp.Name.Count; i++)
                                {
                                    allottetypetemp.Add(new Allottetype
                                    {
                                        Name = damagepayeeregistertemp.Name.Count <= i ? string.Empty : damagepayeeregistertemp.Name[i],
                                        FatherName = damagepayeeregistertemp.FatherName.Count <= i ? string.Empty : damagepayeeregistertemp.FatherName[i],
                                        Date = damagepayeeregistertemp.Date.Count <= i ? DateTime.Now : damagepayeeregistertemp.Date[i],
                                        DamagePayeeRegisterTempId = damagepayeeregistertemp.Id,
                                        AtsgpadocumentPath = damagepayeeregistertemp.ATSGPA != null ?
                                                                damagepayeeregistertemp.ATSGPA.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotoFilePathLayout, damagepayeeregistertemp.ATSGPA[i]) :
                                                                damagepayeeregistertemp.ATSGPAFilePath[i] != null || damagepayeeregistertemp.ATSGPAFilePath[i] != "" ?
                                                                damagepayeeregistertemp.ATSGPAFilePath[i] : string.Empty
                                    });
                                }
                                result = await _selfAssessmentDamageService.SaveAllotteTypeTemp(allottetypetemp);
                            }
                        }

                        //****** code for saving  Damage payment history *****

                        if (damagepayeeregistertemp.PaymntName != null &&
                              damagepayeeregistertemp.RecieptNo != null &&
                              damagepayeeregistertemp.PaymentMode != null &&
                              damagepayeeregistertemp.PaymentDate != null &&
                              damagepayeeregistertemp.Amount != null)
                        {
                            if (
                                 damagepayeeregistertemp.PaymntName.Count > 0 &&
                                 damagepayeeregistertemp.RecieptNo.Count > 0 &&
                                 damagepayeeregistertemp.PaymentMode.Count > 0 &&
                                 damagepayeeregistertemp.PaymentDate.Count > 0 &&
                                 damagepayeeregistertemp.Amount.Count > 0
                                 )

                            {
                                List<Damagepaymenthistory> damagepaymenthistorytemp = new List<Damagepaymenthistory>();
                                for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                                {
                                    damagepaymenthistorytemp.Add(new Damagepaymenthistory
                                    {
                                        Name = damagepayeeregistertemp.PaymntName.Count <= i ? string.Empty : damagepayeeregistertemp.PaymntName[i],
                                        RecieptNo = damagepayeeregistertemp.RecieptNo.Count <= i ? string.Empty : damagepayeeregistertemp.RecieptNo[i],
                                        PaymentMode = damagepayeeregistertemp.PaymentMode.Count <= i ? string.Empty : damagepayeeregistertemp.PaymentMode[i],
                                        PaymentDate = damagepayeeregistertemp.PaymentDate.Count <= i ? DateTime.Now : damagepayeeregistertemp.PaymentDate[i],
                                        Amount = damagepayeeregistertemp.Amount.Count <= i ? 0 : damagepayeeregistertemp.Amount[i],
                                        RecieptDocumentPath = damagepayeeregistertemp.Reciept != null ?
                                                                damagepayeeregistertemp.Reciept.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregistertemp.Reciept[i]) :
                                                                damagepayeeregistertemp.RecieptFilePath[i] != null || damagepayeeregistertemp.RecieptFilePath[i] != "" ?
                                                                damagepayeeregistertemp.RecieptFilePath[i] : string.Empty,
                                        DamagePayeeRegisterTempId = damagepayeeregistertemp.Id
                                    });
                                }

                                result = await _selfAssessmentDamageService.SavePaymentHistoryTemp(damagepaymenthistorytemp);

                            }
                        }
                        if (result)
                        {
                            var isApprovalStart = _approvalproccessService.CheckIsApprovalStart((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value), damagepayeeregistertemp.Id);
                            if (isApprovalStart == 0 && damagepayeeregistertemp.PendingAt != "0")
                            {
                                #region Approval Proccess At 1st level start Added by Renu 21 April 2021
                                var workflowtemplatedata = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value));
                                var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);

                                for (int i = 0; i < DataFlow.Count; i++)
                                {
                                    if (!DataFlow[i].parameterSkip)
                                    {
                                        damagepayeeregistertemp.ApprovedStatus = ApprovalStatus.Id;
                                        damagepayeeregistertemp.PendingAt = approvalproccess.SendTo;
                                        result = await _selfAssessmentDamageService.UpdateBeforeApproval(damagepayeeregistertemp.Id, damagepayeeregistertemp);  //Update Table details 
                                        if (result)
                                        {
                                            approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                            approvalproccess.ProcessGuid = (_configuration.GetSection("workflowPreccessGuidDamagePayee").Value);
                                            approvalproccess.ServiceId = damagepayeeregistertemp.Id;
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

                                            #region Insert Into usernotification table Added By Renu 18 June 2021
                                            if (result == true && approvalproccess.SendTo != null)
                                            {
                                                var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidDamagePayee").Value);
                                                var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                                Usernotification usernotification = new Usernotification();
                                                var replacement = notificationtemplate.Template.Replace("{proccess name}", "Damage Payee Register").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                                usernotification.Message = replacement;
                                                usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidDamagePayee").Value);
                                                usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                                usernotification.ServiceId = approvalproccess.ServiceId;
                                                usernotification.SendFrom = approvalproccess.SendFrom;
                                                usernotification.SendTo = approvalproccess.SendTo;
                                                result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                                            }
                                            #endregion

                                        }

                                        break;
                                    }
                                }

                                #endregion

                                #region Approval Proccess  Mail Generation Added by Renu 08 May 2021
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
                                    bodyDTO.ApplicationName = "Damage Payee Register Application";
                                    bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                                    bodyDTO.SenderName = senderUser.User.Name;
                                    bodyDTO.Link = linkhref;
                                    bodyDTO.AppRefNo = damagepayeeregistertemp.RefNo;
                                    bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                                    bodyDTO.Remarks = approvalproccess.Remarks;
                                    bodyDTO.path = path;
                                    string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                                    #endregion


                                    //sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                                    #region Common Mail Genration
                                    SentMailGenerationDto maildto = new SentMailGenerationDto();
                                    maildto.strMailSubject = "Pending Damage Payee Register Application Approval Request Details ";
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
                            }

                        }
                        ViewBag.Message = Alert.Show(Messages.AddAndApprovalRecordSuccess, "", AlertType.Success);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(damagepayeeregistertemp);
                    }
                }
                else
                {
                    damagepayeeregistertemp.ModifiedBy = SiteContext.UserId;
                    var result = await _selfAssessmentDamageService.Update(damagepayeeregistertemp);
                    if (result)
                    {
                        //****** code for saving  Damage payee personal info *****

                        if (damagepayeeregistertemp.payeeName != null &&
                          damagepayeeregistertemp.Gender != null &&
                          damagepayeeregistertemp.Address != null &&
                          damagepayeeregistertemp.MobileNo != null)
                        {
                            if (damagepayeeregistertemp.payeeName.Count > 0 &&
                        damagepayeeregistertemp.Gender.Count > 0 &&
                        damagepayeeregistertemp.Address.Count > 0 &&
                        damagepayeeregistertemp.MobileNo.Count > 0)

                            {
                                List<Damagepayeepersonelinfo> damagepayeepersonelinfotemp = new List<Damagepayeepersonelinfo>();
                                result = await _selfAssessmentDamageService.DeletePayeePersonalInfoTemp(damagepayeeregistertemp.Id);
                                for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                                {
                                    damagepayeepersonelinfotemp.Add(new Damagepayeepersonelinfo
                                    {
                                        Name = damagepayeeregistertemp.payeeName.Count <= i ? string.Empty : damagepayeeregistertemp.payeeName[i],
                                        FatherName = damagepayeeregistertemp.payeeFatherName.Count <= i ? string.Empty : damagepayeeregistertemp.payeeFatherName[i],
                                        Gender = damagepayeeregistertemp.Gender.Count <= i ? "1" : damagepayeeregistertemp.Gender[i],
                                        Address = damagepayeeregistertemp.Address.Count <= i ? string.Empty : damagepayeeregistertemp.Address[i],
                                        MobileNo = damagepayeeregistertemp.MobileNo.Count <= i ? string.Empty : damagepayeeregistertemp.MobileNo[i],
                                        EmailId = damagepayeeregistertemp.EmailId.Count <= i ? string.Empty : damagepayeeregistertemp.EmailId[i],
                                        DamagePayeeRegisterTempId = damagepayeeregistertemp.Id,
                                        AadharNo = damagepayeeregistertemp.AadharNo.Count <= i ? string.Empty : damagepayeeregistertemp.AadharNo[i],
                                        PanNo = damagepayeeregistertemp.PanNo.Count <= i ? string.Empty : damagepayeeregistertemp.PanNo[i],
                                        AadharNoFilePath = damagepayeeregistertemp.Aadhar != null ?
                                                                damagepayeeregistertemp.Aadhar.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(AadharNoDocument, damagepayeeregistertemp.Aadhar[i]) :
                                                                damagepayeeregistertemp.AadharNoFilePath[i] != null || damagepayeeregistertemp.AadharNoFilePath[i] != "" ?
                                                                damagepayeeregistertemp.AadharNoFilePath[i] : string.Empty,
                                        PanNoFilePath = damagepayeeregistertemp.Pan != null ?
                                                                damagepayeeregistertemp.Pan.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PanNoDocument, damagepayeeregistertemp.Pan[i]) :
                                                                damagepayeeregistertemp.PanNoFilePath[i] != null || damagepayeeregistertemp.PanNoFilePath[i] != "" ?
                                                                damagepayeeregistertemp.PanNoFilePath[i] : string.Empty,
                                        PhotographPath = damagepayeeregistertemp.Photograph != null ?
                                                                damagepayeeregistertemp.Photograph.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotographPersonelDocument, damagepayeeregistertemp.Photograph[i]) :
                                                                damagepayeeregistertemp.PhotographFilePath[i] != null || damagepayeeregistertemp.PhotographFilePath[i] != "" ?
                                                                damagepayeeregistertemp.PhotographFilePath[i] : string.Empty,
                                        SignaturePath = damagepayeeregistertemp.SignatureFile != null ?
                                                                damagepayeeregistertemp.SignatureFile.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(SignaturePersonelDocument, damagepayeeregistertemp.SignatureFile[i]) :
                                                                damagepayeeregistertemp.SignatureFilePath[i] != null || damagepayeeregistertemp.SignatureFilePath[i] != "" ?
                                                                damagepayeeregistertemp.SignatureFilePath[i] : string.Empty,
                                        //OtherDocPath = damagepayeeregistertemp.OtherDocFile != null ?
                                        //                            damagepayeeregistertemp.OtherDocFile.Count <= i ? string.Empty :
                                        //                            fileHelper.SaveFile(OtherDocDocument, damagepayeeregistertemp.OtherDocFile[i]) :
                                        //                            damagepayeeregistertemp.OtherDocFilePath[i] != null || damagepayeeregistertemp.OtherDocFilePath[i] != "" ?
                                        //                            damagepayeeregistertemp.OtherDocFilePath[i] : string.Empty

                                    });
                                }
                                foreach (var item in damagepayeepersonelinfotemp)
                                {
                                    result = await _selfAssessmentDamageService.SavePayeePersonalInfoTemp(item);
                                }
                            }
                        }

                        //****** code for saving  Allotte Type *****
                        if (damagepayeeregistertemp.Name != null &&
                         damagepayeeregistertemp.FatherName != null &&
                         damagepayeeregistertemp.Date != null)
                        {
                            if (
                             damagepayeeregistertemp.Name.Count > 0 &&
                             damagepayeeregistertemp.FatherName.Count > 0 &&
                             damagepayeeregistertemp.Date.Count > 0
                             )
                            {
                                List<Allottetype> allottetypetemp = new List<Allottetype>();
                                result = await _selfAssessmentDamageService.DeleteAllotteTypeTemp(damagepayeeregistertemp.Id);
                                for (int i = 0; i < damagepayeeregistertemp.Name.Count; i++)
                                {
                                    allottetypetemp.Add(new Allottetype
                                    {
                                        Name = damagepayeeregistertemp.Name.Count <= i ? string.Empty : damagepayeeregistertemp.Name[i],
                                        FatherName = damagepayeeregistertemp.FatherName.Count <= i ? string.Empty : damagepayeeregistertemp.FatherName[i],
                                        Date = damagepayeeregistertemp.Date.Count <= i ? DateTime.Now : damagepayeeregistertemp.Date[i],
                                        DamagePayeeRegisterTempId = damagepayeeregistertemp.Id,
                                        AtsgpadocumentPath = damagepayeeregistertemp.ATSGPA != null ?
                                                                damagepayeeregistertemp.ATSGPA.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotoFilePathLayout, damagepayeeregistertemp.ATSGPA[i]) :
                                                                damagepayeeregistertemp.ATSGPAFilePath[i] != null || damagepayeeregistertemp.ATSGPAFilePath[i] != "" ?
                                                                damagepayeeregistertemp.ATSGPAFilePath[i] : string.Empty
                                    });
                                }
                                result = await _selfAssessmentDamageService.SaveAllotteTypeTemp(allottetypetemp);
                            }
                        }

                        //****** code for saving  Damage payment history *****

                        if (damagepayeeregistertemp.PaymntName != null &&
                              damagepayeeregistertemp.RecieptNo != null &&
                              damagepayeeregistertemp.PaymentMode != null &&
                              damagepayeeregistertemp.PaymentDate != null &&
                              damagepayeeregistertemp.Amount != null)
                        {
                            if (
                                 damagepayeeregistertemp.PaymntName.Count > 0 &&
                                 damagepayeeregistertemp.RecieptNo.Count > 0 &&
                                 damagepayeeregistertemp.PaymentMode.Count > 0 &&
                                 damagepayeeregistertemp.PaymentDate.Count > 0 &&
                                 damagepayeeregistertemp.Amount.Count > 0
                                 )

                            {
                                List<Damagepaymenthistory> damagepaymenthistorytemp = new List<Damagepaymenthistory>();
                                result = await _selfAssessmentDamageService.DeletePaymentHistoryTemp(damagepayeeregistertemp.Id);

                                for (int i = 0; i < damagepayeeregistertemp.PaymntName.Count; i++)
                                {
                                    damagepaymenthistorytemp.Add(new Damagepaymenthistory
                                    {
                                        Name = damagepayeeregistertemp.PaymntName.Count <= i ? string.Empty : damagepayeeregistertemp.PaymntName[i],
                                        RecieptNo = damagepayeeregistertemp.RecieptNo.Count <= i ? string.Empty : damagepayeeregistertemp.RecieptNo[i],
                                        PaymentMode = damagepayeeregistertemp.PaymentMode.Count <= i ? string.Empty : damagepayeeregistertemp.PaymentMode[i],
                                        PaymentDate = damagepayeeregistertemp.PaymentDate.Count <= i ? DateTime.Now : damagepayeeregistertemp.PaymentDate[i],
                                        Amount = damagepayeeregistertemp.Amount.Count <= i ? 0 : damagepayeeregistertemp.Amount[i],
                                        RecieptDocumentPath = damagepayeeregistertemp.Reciept != null ?
                                                                damagepayeeregistertemp.Reciept.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregistertemp.Reciept[i]) :
                                                                damagepayeeregistertemp.RecieptFilePath[i] != null || damagepayeeregistertemp.RecieptFilePath[i] != "" ?
                                                                damagepayeeregistertemp.RecieptFilePath[i] : string.Empty,
                                        DamagePayeeRegisterTempId = damagepayeeregistertemp.Id
                                    });
                                }

                                result = await _selfAssessmentDamageService.SavePaymentHistoryTemp(damagepaymenthistorytemp);

                            }
                        }
                        if (result)
                        {
                            var isApprovalStart = _approvalproccessService.CheckIsApprovalStart((_configuration.GetSection("workflowPreccessIdDamagePayee").Value), damagepayeeregistertemp.Id);
                            if (isApprovalStart == 0 && damagepayeeregistertemp.ApprovedStatus != 1)
                            {
                                //#region Approval Proccess At 1st level start Added by Renu 26 Nov 2020
                                //var DataFlow = await dataAsync();
                                //for (int i = 0; i < DataFlow.Count; i++)
                                //{
                                //    if (!DataFlow[i].parameterSkip)
                                //    {
                                //        damagepayeeregistertemp.ApprovedStatus = 0;
                                //        damagepayeeregistertemp.PendingAt = Convert.ToInt32(DataFlow[i].parameterName);
                                //        damagepayeeregistertemp.ModifiedBy = SiteContext.UserId;
                                //        result = await _selfAssessmentDamageService.UpdateBeforeApproval(damagepayeeregistertemp.Id, damagepayeeregistertemp);  //Update Table details 
                                //        if (result)
                                //        {
                                //            Approvalproccess approvalproccess = new Approvalproccess();
                                //            approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                //            approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessIdDamagePayee").Value);
                                //            approvalproccess.ServiceId = damagepayeeregistertemp.Id;
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
                            }

                        }
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(damagepayeeregistertemp);
                    }
                }

               

                var LocalityCode = _selfAssessmentDamageService.GetLocalityName(damagepayeeregistertemp.LocalityId);
                var paymentLink = "https://online.dda.org.in/onlinepmt/Forms/landspmt.aspx?FileNo=" + damagepayeeregistertemp.FileNo + "&Locality=" + LocalityCode.Trim() + "&Amount=" + damagepayeeregistertemp.TotalValueWithInterest + "&Interest=" + damagepayeeregistertemp.InterestDueAmountCompund;
                ViewBag.MainDamagePayeeId = damagepayeeregistertemp.Id;
                if (damagepayeeregistertemp.IsMutaionYes == 1)
                    return View(damagepayeeregistertemp);
                else if (damagepayeeregistertemp.IsMutaionYes == 2)
                    return Redirect(paymentLink);
                else
                    return RedirectToAction("Create", "SubstitutionMutationDetails", new { id = damagepayeeregistertemp.Id });
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregistertemp);
            }
        }

        public async Task<JsonResult> GetDetailspersonelinfotemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _selfAssessmentDamageService.GetPersonalInfoTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.FatherName,
                x.Gender,
                x.Address,
                x.MobileNo,
                x.EmailId,
                x.AadharNoFilePath,
                x.PanNoFilePath,
                x.PhotographPath,
                x.SignaturePath,
                //x.OtherDocPath,
                x.AadharNo,
                x.PanNo
            }));
        }
        public async Task<JsonResult> GetDetailsAllottetypetemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _selfAssessmentDamageService.GetAllottetypeTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.FatherName,
                Date = Convert.ToDateTime(x.Date).ToString("yyyy-MM-dd"),
                x.AtsgpadocumentPath
            }));
        }
        public async Task<JsonResult> GetDetailspaymenthistorytemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _selfAssessmentDamageService.GetPaymentHistoryTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.RecieptNo,
                x.PaymentMode,
                PaymentDate = Convert.ToDateTime(x.PaymentDate).ToString("yyyy-MM-dd"),
                x.Amount,
                x.RecieptDocumentPath
            }));
        }

        public async Task<FileResult> ViewPersonelInfoAadharFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.AadharNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPanFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.PanNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.PhotographPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoSignautreFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.SignaturePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewOtherDocumentFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.OtherDocPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewATSGPAFile(int Id)
        {
            FileHelper file = new FileHelper();
            Allottetype Data = await _selfAssessmentDamageService.GetAllotteTypeSingleResult(Id);
            string path = Data.AtsgpadocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewRecieptFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepaymenthistory Data = await _selfAssessmentDamageService.GetPaymentHistorySingleResult(Id);
            string path = Data.RecieptDocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        //******************  download files **************************
        public async Task<IActionResult> DownloadPropertyPhoto(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.PropertyPhotoPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadShowCauseNotice(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.ShowCauseNoticePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadFgform(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.FgformPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadBillfile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.DocumentForFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        public async Task<IActionResult> Print()
        {
            Damagepayeeregister damagepayeeregister = new Damagepayeeregister();
            var Data = await _selfAssessmentDamageService.FetchSelfAssessmentUserId(SiteContext.UserId);
            var value = await _selfAssessmentDamageService.GetRebateValue();
            if (value == null)
                ViewBag.RebateValue = 0;
            else
                ViewBag.RebateValue = Math.Round((value.RebatePercentage), 2);
            if (Data != null)
            {
                ViewBag.MainDamagePayeeId = Data.Id;
                await BindDropDown(Data);
                return View("Print", Data);
            }
            else
            {
                ViewBag.MainDamagePayeeId = 0;
                await BindDropDown(damagepayeeregister);
                return View("Print",damagepayeeregister);
            }

        }
        public async Task<PartialViewResult> GetDetailspersonelinfoView(int id)
        {
            var Data = await _selfAssessmentDamageService.GetPersonalInfoTemp(id);
            return PartialView("_PersonelInfoDamageAssessee", Data);
        }
        public async Task<PartialViewResult> GetDetailsAllottetypeInfoView(int id)
        {
            var Data = await _selfAssessmentDamageService.GetAllottetypeTemp(id);
            return PartialView("_PreviousDamageAssessee", Data);
        }
        public async Task<PartialViewResult> GetDetailspaymenthistoryInfoView(int id)
        {
            var Data = await _selfAssessmentDamageService.GetPaymentHistoryTemp(id);
            return PartialView("_PaymentHistory", Data);
        }

        #region Fetch workflow data for approval prrocess Added by Renu 1 May 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion
        public async Task<IActionResult> ViewUploadedDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string filename = DocumentFilePath + Data.DocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }


        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            DocumentFilePath = _configuration.GetSection("FilePaths:SA:DocumentFIlePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePath = _configuration.GetSection("FilePaths:SA:DocumentFIlePath").Value.ToString();
                string FilePath = Path.Combine(DocumentFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(DocumentFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DocumentFilePath);// Try to create the directory.
                    }
                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    files.CopyTo(stream);

                                }

                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(FilePath);
                                oPdfReader.Close();
                                fullpath = _configuration.GetSection("FilePaths:SA:DocumentFIlePath").Value.ToString();

                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                IsImg = false;
                            }

                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        IsImg = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Json(IsImg, JsonRequestBehavior);
        }





    }
}
