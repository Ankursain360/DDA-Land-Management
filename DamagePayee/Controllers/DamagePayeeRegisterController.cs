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
using System.IO;
using Unidecode.NET;
using System.Net;
using DamagePayee.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Dto.Master;
using Core.Enum;
using DamagePayee.Filters;
using Service.IApplicationService;
using System.Text;

namespace DamagePayee.Controllers
{
    public class DamagePayeeRegisterController : BaseController
    {
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public IConfiguration _configuration;
        string DocumentFilePath = "";
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserProfileService _userProfileService;
        private readonly IUserNotificationService _userNotificationService;
        public DamagePayeeRegisterController(IDamagepayeeregisterService damagepayeeregisterService,
            IConfiguration configuration, IHostingEnvironment en, IApprovalProccessService approvalproccessService,
            IWorkflowTemplateService workflowtemplateService, IUserProfileService userProfileService,
            IUserNotificationService userNotificationService)
        {
            _configuration = configuration;
            _damagepayeeregisterService = damagepayeeregisterService;
            _hostingEnvironment = en;
            _approvalproccessService = approvalproccessService;
            _workflowtemplateService = workflowtemplateService;
            _userProfileService = userProfileService;
            _userNotificationService = userNotificationService;
            DocumentFilePath = _configuration.GetSection("FilePaths:Dm:DocumentFIlePath").Value.ToString();
        }



        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ViewBag.LocalityList = await _damagepayeeregisterService.GetLocalityList();
            return View();
        }



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagepayeeregistertempSearchDto model)
        
        
        
        
        
        {
            var result = await _damagepayeeregisterService.GetPagedDamagepayeeregister(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Damagepayeeregister damagepayeeregister)
        {
            damagepayeeregister.LocalityList = await _damagepayeeregisterService.GetLocalityList();
            damagepayeeregister.DistrictList = await _damagepayeeregisterService.GetDistrictList();
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Damagepayeeregister damagepayeeregister = new Damagepayeeregister();

            await BindDropDown(damagepayeeregister);
            return View(damagepayeeregister);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Damagepayeeregister damagepayeeregister)
        {
            ViewBag.LocalityList = await _damagepayeeregisterService.GetLocalityList();
            await BindDropDown(damagepayeeregister);
            try
            {
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

                string ATSDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:ATS").Value.ToString();
                string GPADocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:GPA").Value.ToString();
                string MutationDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:Mutation").Value.ToString();
                string WillDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:Will").Value.ToString();

                Random r = new Random();
                int num = r.Next();
                damagepayeeregister.RefNo = DateTime.Now.Year.ToString() + num.ToString();
                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    damagepayeeregister.DocumentName = damagepayeeregister.DocumentIFormFile == null ? damagepayeeregister.DocumentName : fileHelper.SaveFile1(DocumentFilePath, damagepayeeregister.DocumentIFormFile);

                    if (damagepayeeregister.PropertyPhoto != null)
                    {
                        damagepayeeregister.PropertyPhotoPath = fileHelper.SaveFile(PropertyPhotographLayout, damagepayeeregister.PropertyPhoto);
                    }
                    if (damagepayeeregister.ShowCauseNotice != null)
                    {
                        damagepayeeregister.ShowCauseNoticePath = fileHelper.SaveFile(ShowCauseNoticeDocument, damagepayeeregister.ShowCauseNotice);
                    }
                    if (damagepayeeregister.Fgform != null)
                    {
                        damagepayeeregister.FgformPath = fileHelper.SaveFile(FGFormDocument, damagepayeeregister.Fgform);
                    }
                    if (damagepayeeregister.DocumentForFile != null)
                    {
                        damagepayeeregister.DocumentForFilePath = fileHelper.SaveFile(BillDocument, damagepayeeregister.DocumentForFile);
                    }

                    if (damagepayeeregister.ATSFile != null)
                    {
                        damagepayeeregister.AtsfilePath = fileHelper.SaveFile(ATSDocument, damagepayeeregister.ATSFile);
                    }
                    if (damagepayeeregister.GPAFile != null)
                    {
                        damagepayeeregister.GpafilePath = fileHelper.SaveFile(GPADocument, damagepayeeregister.GPAFile);
                    }
                    if (damagepayeeregister.MutationFile != null)
                    {
                        damagepayeeregister.MutationFilePath = fileHelper.SaveFile(MutationDocument, damagepayeeregister.MutationFile);
                    }
                    if (damagepayeeregister.WillFile != null)
                    {
                        damagepayeeregister.WillFilePath = fileHelper.SaveFile(WillDocument, damagepayeeregister.WillFile);
                    }




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
                                    return View(damagepayeeregister);
                                }

                                damagepayeeregister.ApprovalZoneId = SiteContext.ZoneId;
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

                    var result = await _damagepayeeregisterService.Create(damagepayeeregister);


                    if (result)
                    {
                        //******* creating damage payee user ******

                        var resultpassword = await _damagepayeeregisterService.CreateUser(damagepayeeregister);
                        if (!resultpassword.Equals("False"))
                        {
                            //At successfull completion send mail and sms
                            string DisplayName = damagepayeeregister.payeeName[0].ToString();
                            string EmailID = damagepayeeregister.EmailId[0].ToString();
                            string Id = damagepayeeregister.Id.ToString().Unidecode();
                            string LoginName = damagepayeeregister.payeeName[0].ToString();
                            string ContactNo = damagepayeeregister.MobileNo[0].ToString();
                            string Password = resultpassword;
                            string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "UserMailDetails.html");
                            string loginlink = "https://damage.managemybusinessess.com/DamagePayeeRegistration/Create";
                            #region Mail Generation Added By Renu

                            MailSMSHelper mailG = new MailSMSHelper();

                            #region HTML Body Generation
                            DamageRegisterBodyDTO bodyDTO = new DamageRegisterBodyDTO();
                            bodyDTO.displayName = DisplayName;
                            bodyDTO.loginName = LoginName;
                            bodyDTO.password = Password;
                            bodyDTO.path = path;
                            bodyDTO.emailId = EmailID;
                            bodyDTO.contactNo = ContactNo;
                            bodyDTO.Link = loginlink;
                            string strBodyMsg = mailG.PopulateBodyRegister(bodyDTO);
                            #endregion
                                                        
                            //var sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, EmailID, strMailCC, strMailBCC, strAttachPath);
                            #region Common Mail Genration
                            SentMailGenerationDto maildto = new SentMailGenerationDto();
                            maildto.strMailSubject = "Damage Payee User Details ";
                            maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
                            maildto.strBodyMsg = strBodyMsg;
                            maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
                            maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
                            maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
                            maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
                            maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

                            maildto.strMailTo = EmailID;
                            var sendMailResult = mailG.SendMailWithAttachment(maildto);
                            #endregion
                            #endregion
                        }


                        //****** code for saving  Damage payee personal info *****

                        if (damagepayeeregister.payeeName != null &&
                          damagepayeeregister.Gender != null &&
                          damagepayeeregister.Address != null &&
                          damagepayeeregister.MobileNo != null)
                        {
                            if (damagepayeeregister.payeeName.Count > 0 &&
                        damagepayeeregister.Gender.Count > 0 &&
                        damagepayeeregister.Address.Count > 0 &&
                        damagepayeeregister.MobileNo.Count > 0)

                            {
                                List<Damagepayeepersonelinfo> damagepayeepersonelinfo = new List<Damagepayeepersonelinfo>();
                                for (int i = 0; i < damagepayeeregister.payeeName.Count; i++)
                                {
                                    damagepayeepersonelinfo.Add(new Damagepayeepersonelinfo
                                    {
                                        Name = damagepayeeregister.payeeName.Count <= i ? string.Empty : damagepayeeregister.payeeName[i],
                                        FatherName = damagepayeeregister.payeeFatherName.Count <= i ? string.Empty : damagepayeeregister.payeeFatherName[i],
                                        Gender = damagepayeeregister.Gender.Count <= i ? "1" : damagepayeeregister.Gender[i],
                                        Address = damagepayeeregister.Address.Count <= i ? string.Empty : damagepayeeregister.Address[i],
                                        MobileNo = damagepayeeregister.MobileNo.Count <= i ? string.Empty : damagepayeeregister.MobileNo[i],
                                        EmailId = damagepayeeregister.EmailId.Count <= i ? string.Empty : damagepayeeregister.EmailId[i],
                                        DamagePayeeRegisterTempId = damagepayeeregister.Id,
                                        AadharNo = damagepayeeregister.AadharNo.Count <= i ? string.Empty : damagepayeeregister.AadharNo[i],
                                        PanNo = damagepayeeregister.PanNo.Count <= i ? string.Empty : damagepayeeregister.PanNo[i],

                                        AadharNoFilePath = damagepayeeregister.Aadhar != null ?
                                                                    damagepayeeregister.Aadhar.Count <= i ? string.Empty :
                                                                    fileHelper.SaveFile(AadharNoDocument, damagepayeeregister.Aadhar[i]) :
                                                                    damagepayeeregister.AadharNoFilePath[i] != null || damagepayeeregister.AadharNoFilePath[i] != "" ?
                                                                    damagepayeeregister.AadharNoFilePath[i] : string.Empty,
                                        PanNoFilePath = damagepayeeregister.Pan != null ?
                                                                    damagepayeeregister.Pan.Count <= i ? string.Empty :
                                                                    fileHelper.SaveFile(PanNoDocument, damagepayeeregister.Pan[i]) :
                                                                    damagepayeeregister.PanNoFilePath[i] != null || damagepayeeregister.PanNoFilePath[i] != "" ?
                                                                    damagepayeeregister.PanNoFilePath[i] : string.Empty,
                                        PhotographPath = damagepayeeregister.Photograph != null ?
                                                                    damagepayeeregister.Photograph.Count <= i ? string.Empty :
                                                                    fileHelper.SaveFile(PhotographPersonelDocument, damagepayeeregister.Photograph[i]) :
                                                                    damagepayeeregister.PhotographFilePath[i] != null || damagepayeeregister.PhotographFilePath[i] != "" ?
                                                                    damagepayeeregister.PhotographFilePath[i] : string.Empty,
                                        SignaturePath = damagepayeeregister.SignatureFile != null ?
                                                                    damagepayeeregister.SignatureFile.Count <= i ? string.Empty :
                                                                    fileHelper.SaveFile(SignaturePersonelDocument, damagepayeeregister.SignatureFile[i]) :
                                                                    damagepayeeregister.SignatureFilePath[i] != null || damagepayeeregister.SignatureFilePath[i] != "" ?
                                                                    damagepayeeregister.SignatureFilePath[i] : string.Empty,
                                        //OtherDocPath = damagepayeeregister.OtherDocFile != null ?
                                        //                            damagepayeeregister.OtherDocFile.Count <= i ? string.Empty :
                                        //                            fileHelper.SaveFile(OtherDocDocument, damagepayeeregister.OtherDocFile[i]) :
                                        //                            damagepayeeregister.OtherDocFilePath[i] != null || damagepayeeregister.OtherDocFilePath[i] != "" ?
                                        //                            damagepayeeregister.OtherDocFilePath[i] : string.Empty



                                    });
                                }
                                foreach (var item in damagepayeepersonelinfo)
                                {
                                    result = await _damagepayeeregisterService.SavePayeePersonalInfo(item);
                                }
                            }
                        }


                        //****** code for saving  Allotte Type *****
                        if (damagepayeeregister.Name != null &&
                         damagepayeeregister.FatherName != null &&
                         damagepayeeregister.Date != null)
                        {
                            if (
                             damagepayeeregister.Name.Count > 0 &&
                             damagepayeeregister.FatherName.Count > 0 &&
                             damagepayeeregister.Date.Count > 0
                             )
                            {
                                List<Allottetype> allottetype = new List<Allottetype>();
                                for (int i = 0; i < damagepayeeregister.Name.Count; i++)
                                {
                                    allottetype.Add(new Allottetype
                                    {
                                        Name = damagepayeeregister.Name.Count <= i ? string.Empty : damagepayeeregister.Name[i],
                                        FatherName = damagepayeeregister.FatherName.Count <= i ? string.Empty : damagepayeeregister.FatherName[i],
                                        Date = damagepayeeregister.Date.Count <= i ? DateTime.Now : damagepayeeregister.Date[i],
                                        DamagePayeeRegisterTempId = damagepayeeregister.Id,
                                        AtsgpadocumentPath = damagepayeeregister.ATSGPA != null ?
                                                                    damagepayeeregister.ATSGPA.Count <= i ? string.Empty :
                                                                    fileHelper.SaveFile(PhotoFilePathLayout, damagepayeeregister.ATSGPA[i]) :
                                                                    damagepayeeregister.ATSGPAFilePath[i] != null || damagepayeeregister.ATSGPAFilePath[i] != "" ?
                                                                    damagepayeeregister.ATSGPAFilePath[i] : string.Empty


                                    });
                                }
                                result = await _damagepayeeregisterService.SaveAllotteType(allottetype);
                            }
                        }

                        //****** code for saving  Damage payment history *****

                        if (damagepayeeregister.PaymntName != null &&
                              damagepayeeregister.RecieptNo != null &&
                              damagepayeeregister.PaymentMode != null &&
                              damagepayeeregister.PaymentDate != null &&
                              damagepayeeregister.Amount != null)
                        {


                            if (
                                 damagepayeeregister.PaymntName.Count > 0 &&
                                 damagepayeeregister.RecieptNo.Count > 0 &&
                                 damagepayeeregister.PaymentMode.Count > 0 &&
                                 damagepayeeregister.PaymentDate.Count > 0 &&
                                 damagepayeeregister.Amount.Count > 0
                                 )

                            {
                                List<Damagepaymenthistory> damagepaymenthistory = new List<Damagepaymenthistory>();
                                for (int i = 0; i < damagepayeeregister.payeeName.Count; i++)
                                {
                                    damagepaymenthistory.Add(new Damagepaymenthistory
                                    {
                                        Name = damagepayeeregister.PaymntName.Count <= i ? string.Empty : damagepayeeregister.PaymntName[i],
                                        RecieptNo = damagepayeeregister.RecieptNo.Count <= i ? string.Empty : damagepayeeregister.RecieptNo[i],
                                        PaymentMode = damagepayeeregister.PaymentMode.Count <= i ? string.Empty : damagepayeeregister.PaymentMode[i],
                                        PaymentDate = damagepayeeregister.PaymentDate.Count <= i ? DateTime.Now : damagepayeeregister.PaymentDate[i],
                                        Amount = damagepayeeregister.Amount.Count <= i ? 0 : damagepayeeregister.Amount[i],
                                        RecieptDocumentPath = damagepayeeregister.Reciept != null ?
                                                                    damagepayeeregister.Reciept.Count <= i ? string.Empty :
                                                                    fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregister.Reciept[i]) :
                                                                    damagepayeeregister.RecieptFilePath[i] != null || damagepayeeregister.RecieptFilePath[i] != "" ?
                                                                    damagepayeeregister.RecieptFilePath[i] : string.Empty,
                                        DamagePayeeRegisterTempId = damagepayeeregister.Id
                                    });
                                }

                                result = await _damagepayeeregisterService.SavePaymentHistory(damagepaymenthistory);

                            }
                        }

                        if (result)
                        {
                            var isApprovalStart = _approvalproccessService.CheckIsApprovalStart((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value), damagepayeeregister.Id);
                            if (isApprovalStart == 0 && damagepayeeregister.PendingAt != "0")
                            {
                                #region Approval Proccess At 1st level start Added by Renu 21 April 2021
                                var workflowtemplatedata = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value));
                                var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);

                                for (int i = 0; i < DataFlow.Count; i++)
                                {
                                    if (!DataFlow[i].parameterSkip)
                                    {
                                        damagepayeeregister.ApprovedStatus = ApprovalStatus.Id;
                                        damagepayeeregister.PendingAt = approvalproccess.SendTo;
                                        result = await _damagepayeeregisterService.UpdateBeforeApproval(damagepayeeregister.Id, damagepayeeregister);  //Update Table details 
                                        if (result)
                                        {
                                            approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                            approvalproccess.ProcessGuid = (_configuration.GetSection("workflowPreccessGuidDamagePayee").Value);
                                            approvalproccess.ServiceId = damagepayeeregister.Id;
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
                                    bodyDTO.AppRefNo = damagepayeeregister.RefNo;
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

                     //   var result1 = await _damagepayeeregisterService.GetAllDamagepayeeregister();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(damagepayeeregister);
                    }

                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(damagepayeeregister);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregister);
            }
        }

        public async Task<JsonResult> GetDetailspersonelinfotemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _damagepayeeregisterService.GetPersonalInfo(Convert.ToInt32(Id));
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
            var data = await _damagepayeeregisterService.GetAllottetype(Convert.ToInt32(Id));
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
            var data = await _damagepayeeregisterService.GetPaymentHistory(Convert.ToInt32(Id));
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
        public async Task<IActionResult> View(int id)
        {
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);
            await BindDropDown(Data);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);
            await BindDropDown(Data);
           
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Damagepayeeregister damagepayeeregister)
        {
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);
            ViewBag.LocalityList = await _damagepayeeregisterService.GetLocalityList();

            await BindDropDown(damagepayeeregister);
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

            string ATSDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:ATS").Value.ToString();
            string GPADocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:GPA").Value.ToString();
            string MutationDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:Mutation").Value.ToString();
            string WillDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:Will").Value.ToString();

            if (ModelState.IsValid)

            {
                FileHelper fileHelper = new FileHelper();
                damagepayeeregister.DocumentName = damagepayeeregister.DocumentIFormFile == null ? damagepayeeregister.DocumentName : fileHelper.SaveFile1(DocumentFilePath, damagepayeeregister.DocumentIFormFile);

                if (damagepayeeregister.PropertyPhoto != null)
                {
                    damagepayeeregister.PropertyPhotoPath = fileHelper.SaveFile(PropertyPhotographLayout, damagepayeeregister.PropertyPhoto);
                }
                else
                {
                    damagepayeeregister.PropertyPhotoPath = Data.PropertyPhotoPath;
                }
                if (damagepayeeregister.ShowCauseNotice != null)
                {
                    damagepayeeregister.ShowCauseNoticePath = fileHelper.SaveFile(ShowCauseNoticeDocument, damagepayeeregister.ShowCauseNotice);
                }
                else
                {
                    damagepayeeregister.ShowCauseNoticePath = Data.ShowCauseNoticePath;
                }
                if (damagepayeeregister.Fgform != null)
                {
                    damagepayeeregister.FgformPath = fileHelper.SaveFile(FGFormDocument, damagepayeeregister.Fgform);
                }
                else
                {
                    damagepayeeregister.FgformPath = Data.FgformPath;
                }
                if (damagepayeeregister.DocumentForFile != null)
                {
                    damagepayeeregister.DocumentForFilePath = fileHelper.SaveFile(BillDocument, damagepayeeregister.DocumentForFile);
                }
                else
                {
                    damagepayeeregister.DocumentForFilePath = Data.DocumentForFilePath;
                }
                if (damagepayeeregister.ATSFile != null)
                {
                    damagepayeeregister.AtsfilePath = fileHelper.SaveFile(ATSDocument, damagepayeeregister.ATSFile);
                }
                else
                {
                    damagepayeeregister.AtsfilePath = Data.AtsfilePath;
                }
                if (damagepayeeregister.GPAFile != null)
                {
                    damagepayeeregister.GpafilePath = fileHelper.SaveFile(GPADocument, damagepayeeregister.GPAFile);
                }
                else
                {
                    damagepayeeregister.GpafilePath = Data.GpafilePath;
                }
                if (damagepayeeregister.MutationFile != null)
                {
                    damagepayeeregister.MutationFilePath = fileHelper.SaveFile(MutationDocument, damagepayeeregister.MutationFile);
                }
                else
                {
                    damagepayeeregister.MutationFilePath = Data.MutationFilePath;
                }
                if (damagepayeeregister.WillFile != null)
                {
                    damagepayeeregister.WillFilePath = fileHelper.SaveFile(WillDocument, damagepayeeregister.WillFile);
                }
                else
                {
                    damagepayeeregister.WillFilePath = Data.WillFilePath;
                }
                var result = await _damagepayeeregisterService.Update(id, damagepayeeregister);
                if (result)
                {


                    //****** code for saving  Damage payee personal info *****

                    if (damagepayeeregister.payeeName != null &&
                      damagepayeeregister.Gender != null &&
                      damagepayeeregister.Address != null &&
                      damagepayeeregister.MobileNo != null)
                    {
                        if (damagepayeeregister.payeeName.Count > 0 &&
                    damagepayeeregister.Gender.Count > 0 &&
                    damagepayeeregister.Address.Count > 0 &&
                    damagepayeeregister.MobileNo.Count > 0)

                        {
                            List<Damagepayeepersonelinfo> damagepayeepersonelinfo = new List<Damagepayeepersonelinfo>();
                            result = await _damagepayeeregisterService.DeletePayeePersonalInfo(id);
                            for (int i = 0; i < damagepayeeregister.payeeName.Count; i++)
                            {
                                damagepayeepersonelinfo.Add(new Damagepayeepersonelinfo
                                {
                                    Name = damagepayeeregister.payeeName.Count <= i ? string.Empty : damagepayeeregister.payeeName[i],
                                    FatherName = damagepayeeregister.payeeFatherName.Count <= i ? string.Empty : damagepayeeregister.payeeFatherName[i],
                                    Gender = damagepayeeregister.Gender.Count <= i ? "1" : damagepayeeregister.Gender[i],
                                    Address = damagepayeeregister.Address.Count <= i ? string.Empty : damagepayeeregister.Address[i],
                                    MobileNo = damagepayeeregister.MobileNo.Count <= i ? string.Empty : damagepayeeregister.MobileNo[i],
                                    EmailId = damagepayeeregister.EmailId.Count <= i ? string.Empty : damagepayeeregister.EmailId[i],
                                    DamagePayeeRegisterTempId = damagepayeeregister.Id,
                                    AadharNo = damagepayeeregister.AadharNo.Count <= i ? string.Empty : damagepayeeregister.AadharNo[i],
                                    PanNo = damagepayeeregister.PanNo.Count <= i ? string.Empty : damagepayeeregister.PanNo[i],

                                    AadharNoFilePath = damagepayeeregister.Aadhar != null ?
                                                                damagepayeeregister.Aadhar.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(AadharNoDocument, damagepayeeregister.Aadhar[i]) :
                                                                damagepayeeregister.AadharNoFilePath[i] != null || damagepayeeregister.AadharNoFilePath[i] != "" ?
                                                                damagepayeeregister.AadharNoFilePath[i] : string.Empty,
                                    PanNoFilePath = damagepayeeregister.Pan != null ?
                                                                damagepayeeregister.Pan.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PanNoDocument, damagepayeeregister.Pan[i]) :
                                                                damagepayeeregister.PanNoFilePath[i] != null || damagepayeeregister.PanNoFilePath[i] != "" ?
                                                                damagepayeeregister.PanNoFilePath[i] : string.Empty,
                                    PhotographPath = damagepayeeregister.Photograph != null ?
                                                                damagepayeeregister.Photograph.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotographPersonelDocument, damagepayeeregister.Photograph[i]) :
                                                                damagepayeeregister.PhotographFilePath[i] != null || damagepayeeregister.PhotographFilePath[i] != "" ?
                                                                damagepayeeregister.PhotographFilePath[i] : string.Empty,
                                    SignaturePath = damagepayeeregister.SignatureFile != null ?
                                                                damagepayeeregister.SignatureFile.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(SignaturePersonelDocument, damagepayeeregister.SignatureFile[i]) :
                                                                damagepayeeregister.SignatureFilePath[i] != null || damagepayeeregister.SignatureFilePath[i] != "" ?
                                                                damagepayeeregister.SignatureFilePath[i] : string.Empty,
                                    //OtherDocPath = damagepayeeregister.OtherDocFile != null ?
                                    //                                damagepayeeregister.OtherDocFile.Count <= i ? string.Empty :
                                    //                                fileHelper.SaveFile(OtherDocDocument, damagepayeeregister.OtherDocFile[i]) :
                                    //                                damagepayeeregister.OtherDocFilePath[i] != null || damagepayeeregister.OtherDocFilePath[i] != "" ?
                                    //                                damagepayeeregister.OtherDocFilePath[i] : string.Empty

                                });


                            }
                            foreach (var item in damagepayeepersonelinfo)
                            {
                                result = await _damagepayeeregisterService.SavePayeePersonalInfo(item);
                            }
                        }
                    }

                    //****** code for saving  Allotte Type *****
                    if (damagepayeeregister.Name != null &&
                     damagepayeeregister.FatherName != null &&
                     damagepayeeregister.Date != null)
                    {
                        if (
                         damagepayeeregister.Name.Count > 0 &&
                         damagepayeeregister.FatherName.Count > 0 &&
                         damagepayeeregister.Date.Count > 0
                         )
                        {

                            List<Allottetype> allottetype = new List<Allottetype>();
                            result = await _damagepayeeregisterService.DeleteAllotteType(id);
                            for (int i = 0; i < damagepayeeregister.Name.Count; i++)
                            {
                                allottetype.Add(new Allottetype
                                {
                                    Name = damagepayeeregister.Name.Count <= i ? string.Empty : damagepayeeregister.Name[i],
                                    FatherName = damagepayeeregister.FatherName.Count <= i ? string.Empty : damagepayeeregister.FatherName[i],
                                    Date = damagepayeeregister.Date.Count <= i ? DateTime.Now : damagepayeeregister.Date[i],
                                    DamagePayeeRegisterTempId = damagepayeeregister.Id,
                                    AtsgpadocumentPath = damagepayeeregister.ATSGPA != null ?
                                                                damagepayeeregister.ATSGPA.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotoFilePathLayout, damagepayeeregister.ATSGPA[i]) :
                                                                damagepayeeregister.ATSGPAFilePath[i] != null || damagepayeeregister.ATSGPAFilePath[i] != "" ?
                                                                damagepayeeregister.ATSGPAFilePath[i] : string.Empty

                                });
                            }
                            result = await _damagepayeeregisterService.SaveAllotteType(allottetype);
                        }
                    }

                    //****** code for saving  Damage payment history *****

                    if (damagepayeeregister.PaymntName != null &&
                          damagepayeeregister.RecieptNo != null &&
                          damagepayeeregister.PaymentMode != null &&
                          damagepayeeregister.PaymentDate != null &&
                          damagepayeeregister.Amount != null)
                    {


                        if (
                             damagepayeeregister.PaymntName.Count > 0 &&
                             damagepayeeregister.RecieptNo.Count > 0 &&
                             damagepayeeregister.PaymentMode.Count > 0 &&
                             damagepayeeregister.PaymentDate.Count > 0 &&
                             damagepayeeregister.Amount.Count > 0
                             )

                        {
                            List<Damagepaymenthistory> damagepaymenthistory = new List<Damagepaymenthistory>();
                            result = await _damagepayeeregisterService.DeletePaymentHistory(id);
                            for (int i = 0; i < damagepayeeregister.payeeName.Count; i++)
                            {
                                damagepaymenthistory.Add(new Damagepaymenthistory
                                {
                                    Name = damagepayeeregister.PaymntName.Count <= i ? string.Empty : damagepayeeregister.PaymntName[i],
                                    RecieptNo = damagepayeeregister.RecieptNo.Count <= i ? string.Empty : damagepayeeregister.RecieptNo[i],
                                    PaymentMode = damagepayeeregister.PaymentMode.Count <= i ? string.Empty : damagepayeeregister.PaymentMode[i],
                                    PaymentDate = damagepayeeregister.PaymentDate.Count <= i ? DateTime.Now : damagepayeeregister.PaymentDate[i],
                                    Amount = damagepayeeregister.Amount.Count <= i ? 0 : damagepayeeregister.Amount[i],
                                    RecieptDocumentPath = damagepayeeregister.Reciept != null ?
                                                                damagepayeeregister.Reciept.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregister.Reciept[i]) :
                                                                damagepayeeregister.RecieptFilePath[i] != null || damagepayeeregister.RecieptFilePath[i] != "" ?
                                                                damagepayeeregister.RecieptFilePath[i] : string.Empty,
                                    DamagePayeeRegisterTempId = damagepayeeregister.Id



                                });
                            }

                            result = await _damagepayeeregisterService.SavePaymentHistory(damagepaymenthistory);

                        }
                    }
                    if (result)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _damagepayeeregisterService.GetAllDamagepayeeregister();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(damagepayeeregister);
                    }

                }

                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(damagepayeeregister);
                }

            }


            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregister);
            }

        }
        //******************  download files **************************
        public async Task<IActionResult> DownloadPropertyPhoto(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.PropertyPhotoPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadShowCauseNotice(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.ShowCauseNoticePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadFgform(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.FgformPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadBillfile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.DocumentForFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        public async Task<IActionResult> DownloadATS(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.AtsfilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadGPA(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.GpafilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadMutation(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.MutationFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadWill(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.WillFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        //**********************  download repeater files****************************
        public async Task<FileResult> ViewPersonelInfoAadharFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.AadharNoFilePath;
           
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPanFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.PanNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.PhotographPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoSignautreFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.SignaturePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        //public async Task<FileResult> ViewOtherDocumentFile(int Id)
        //{
        //    FileHelper file = new FileHelper();
        //    Damagepayeepersonelinfo Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
        //    string path = Data.OtherDocPath;
        //    byte[] FileBytes = System.IO.File.ReadAllBytes(path);
        //    return File(FileBytes, file.GetContentType(path));
        //}


        public async Task<FileResult> ViewATSFile(int Id)
        {
            FileHelper file = new FileHelper();
            Allottetype Data = await _damagepayeeregisterService.GetATSFilePath(Id);
            string path = Data.AtsgpadocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        public async Task<FileResult> ViewReceiptFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepaymenthistory Data = await _damagepayeeregisterService.GetReceiptFilePath(Id);
            string path = Data.RecieptDocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
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


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.LocalityList = await _damagepayeeregisterService.GetLocalityList();
            var result = await _damagepayeeregisterService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _damagepayeeregisterService.GetAllDamagepayeeregister();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _damagepayeeregisterService.GetAllDamagepayeeregister();
                return View("Index", result1);
            }
        }
      

        public async Task<IActionResult> DamagePayeeRegisterList()
        {
            var result = await _damagepayeeregisterService.GetAllDamagepayeeregister();
            List<DamagePayeeRegisterListDto> data = new List<DamagePayeeRegisterListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DamagePayeeRegisterListDto()
                    {
                        Id = result[i].Id,
                        FileNo = result[i].FileNo,
                        TypeOfDamageAssessee = result[i].TypeOfDamageAssessee,
                        PropertyNo = result[i].PropertyNo,
                        Locality = result[i].Locality == null ? "" : result[i].Locality.Name.ToString(),
                        IsDdadamagePayee = result[i].IsDdadamagePayee,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                        ApprovalStatus = result[i].ApprovedStatusNavigation == null ? "" : result[i].ApprovedStatusNavigation.SentStatusName.ToString(),
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
        //view file
        public FileResult ViewDocument(string path)
        {
            FileHelper file = new FileHelper();
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> ViewUploadedDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = DocumentFilePath + Data.DocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }

    }
}
