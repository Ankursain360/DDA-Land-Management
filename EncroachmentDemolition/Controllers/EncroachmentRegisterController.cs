using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.Entity;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;
using EncroachmentDemolition.Filters;
using Core.Enum;
using Dto.Master;
using Service.IApplicationService;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace EncroachmentDemolition.Controllers
{
    public class EncroachmentRegisterController : BaseController
    {
        public IConfiguration _configuration;
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
        private readonly IWatchandwardService _watchandwardService;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserProfileService _userProfileService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserNotificationService _userNotificationService;
        public EncroachmentRegisterController(IEncroachmentRegisterationService encroachmentRegisterationService,
            IConfiguration configuration, IWatchandwardService watchandwardService,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IUserProfileService userProfileService, IHostingEnvironment hostingEnvironment,
            IUserNotificationService userNotificationService)
        {
            _encroachmentRegisterationService = encroachmentRegisterationService;
            _configuration = configuration;
            _watchandwardService = watchandwardService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _userProfileService = userProfileService;
            _hostingEnvironment = hostingEnvironment;
            _userNotificationService = userNotificationService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] EncroachmentRegisterationDto model)
        {
            var result = await _encroachmentRegisterationService.GetPagedEncroachmentRegisteration(model, (int)ApprovalActionStatus.Approved, SiteContext.ZoneId ?? 0);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            EncroachmentRegisteration encroachmentRegisterations = new EncroachmentRegisteration();
            encroachmentRegisterations.WatchWardId = id;
            ViewBag.PrimaryId = 0;
            encroachmentRegisterations.Id = 0;
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);
            return View(encroachmentRegisterations);
        }

        public async Task<PartialViewResult> WatchWardView(int id)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            if (Data != null)
                Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();


            return PartialView("_WatchWard", Data);
        }

        public async Task<FileResult> ViewDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string path = Data.PhotoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(EncroachmentRegisteration encroachmentRegisterations)
        {

            ViewBag.PrimaryId = 0;
            try
            {
                encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
                encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
                encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
                encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
                encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);
                encroachmentRegisterations.Zone = await _encroachmentRegisterationService.FetchSingleResultOnZoneList(Convert.ToInt32(encroachmentRegisterations.ZoneId));
                string PhotoFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:PhotoFilePath").Value.ToString();
                string LocationMapFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:LocationMapFilePath").Value.ToString();
                string FirfilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:FIRFilePath").Value.ToString();
                Random r = new Random();
                int num = r.Next();
                encroachmentRegisterations.RefNo = DateTime.Now.Year.ToString() + encroachmentRegisterations.Zone.Code + num.ToString();

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
                                    ViewBag.Message = Alert.Show("Without Zone application cannot be submitted, Please Contact System Administrator", "", AlertType.Warning);
                                    return View(encroachmentRegisterations);
                                }

                                encroachmentRegisterations.ApprovalZoneId = SiteContext.ZoneId;
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
                                            var UserProfile = await _userProfileService.GetUserByIdZoneConcatedName(Convert.ToInt32(MultiUserId), SiteContext.ZoneId ?? 0);
                                            if (UserProfile != null)
                                            {
                                                if (col > 0)
                                                    multouserszonewise.Append(",");
                                                multouserszonewise.Append(UserProfile.UserId);
                                            }
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
                    encroachmentRegisterations.IsActive = 1;
                    encroachmentRegisterations.WatchWardId = encroachmentRegisterations.WatchWardId == 0 ? null : encroachmentRegisterations.WatchWardId;
                    encroachmentRegisterations.CreatedBy = SiteContext.UserId;
                    encroachmentRegisterations.OtherDepartment = encroachmentRegisterations.OtherDepartment == 0 ? null : encroachmentRegisterations.OtherDepartment;
                    var result = await _encroachmentRegisterationService.Create(encroachmentRegisterations);
                    if (result)
                    {
                        FileHelper fileHelper = new FileHelper();
                        if (encroachmentRegisterations.NameOfStructure != null &&
                            encroachmentRegisterations.AreaApprox != null &&
                            encroachmentRegisterations.Type != null &&
                            encroachmentRegisterations.DateOfEncroachment != null &&
                            encroachmentRegisterations.CountOfStructure != null &&
                            encroachmentRegisterations.ReferenceNoOnLocation != null &&
                            encroachmentRegisterations.ConstructionStatus != null &&
                            encroachmentRegisterations.NameOfStructure.Count > 0 &&
                            encroachmentRegisterations.AreaApprox.Count > 0 &&
                            encroachmentRegisterations.Type.Count > 0 &&
                            encroachmentRegisterations.DateOfEncroachment.Count > 0 &&
                            encroachmentRegisterations.CountOfStructure.Count > 0 &&
                            encroachmentRegisterations.ConstructionStatus.Count > 0 &&
                            encroachmentRegisterations.ReferenceNoOnLocation.Count > 0)
                        {
                            List<DetailsOfEncroachment> detailsOfEncroachment = new List<DetailsOfEncroachment>();
                            for (int i = 0; i < encroachmentRegisterations.NameOfStructure.Count; i++)
                            {
                                detailsOfEncroachment.Add(new DetailsOfEncroachment
                                {
                                    Area = encroachmentRegisterations.AreaApprox.Count <= i ? 0 : encroachmentRegisterations.AreaApprox[i],
                                    CountOfStructure = encroachmentRegisterations.CountOfStructure.Count <= i ? 0 : encroachmentRegisterations.CountOfStructure[i],
                                    DateOfEncroachment = encroachmentRegisterations.DateOfEncroachment.Count <= i ? 0 : encroachmentRegisterations.DateOfEncroachment[i],
                                    ReligiousStructure = encroachmentRegisterations.ReligiousStructure.Count <= i ? "" : encroachmentRegisterations.ReligiousStructure[i],
                                    ConstructionStatus = encroachmentRegisterations.ConstructionStatus.Count <= i ? "" : encroachmentRegisterations.ConstructionStatus[i],
                                    NameOfStructure = encroachmentRegisterations.NameOfStructure.Count <= i ? "" : encroachmentRegisterations.NameOfStructure[i],
                                    ReferenceNoOnLocation = encroachmentRegisterations.ReferenceNoOnLocation.Count <= i ? "" : encroachmentRegisterations.ReferenceNoOnLocation[i],
                                    Type = encroachmentRegisterations.Type.Count <= i ? "" : encroachmentRegisterations.Type[i],
                                    EncroachmentRegisterationId = encroachmentRegisterations.Id
                                });
                            }
                            foreach (var item in detailsOfEncroachment)
                            {
                                result = await _encroachmentRegisterationService.SaveDetailsOfEncroachment(item);
                            }
                        }
                        if (encroachmentRegisterations.Firfile != null && encroachmentRegisterations.Firfile.Count > 0)
                        {
                            List<EncroachmentFirFileDetails> encroachmentFirFileDetails = new List<EncroachmentFirFileDetails>();
                            for (int i = 0; i < encroachmentRegisterations.Firfile.Count; i++)
                            {
                                string FilePath = fileHelper.SaveFile(FirfilePath, encroachmentRegisterations.Firfile[i]);
                                encroachmentFirFileDetails.Add(new EncroachmentFirFileDetails
                                {
                                    EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                    FirFilePath = FilePath
                                });
                            }
                            foreach (var item in encroachmentFirFileDetails)
                            {
                                result = await _encroachmentRegisterationService.SaveEncroachmentFirFileDetails(item);
                            }
                        }
                        if (encroachmentRegisterations.PhotoFile != null && encroachmentRegisterations.PhotoFile.Count > 0)
                        {
                            List<EncroachmentPhotoFileDetails> encroachmentPhotoFileDetails = new List<EncroachmentPhotoFileDetails>();
                            for (int i = 0; i < encroachmentRegisterations.PhotoFile.Count; i++)
                            {
                                string FilePath = fileHelper.SaveFile(PhotoFilePath, encroachmentRegisterations.PhotoFile[i]);
                                encroachmentPhotoFileDetails.Add(new EncroachmentPhotoFileDetails
                                {
                                    EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                    PhotoFilePath = FilePath
                                });
                            }
                            foreach (var item in encroachmentPhotoFileDetails)
                            {
                                result = await _encroachmentRegisterationService.SaveEncroachmentPhotoFileDetails(item);
                            }
                        }
                        if (encroachmentRegisterations.LocationMapFile != null && encroachmentRegisterations.LocationMapFile.Count > 0)
                        {
                            List<EncroachmentLocationMapFileDetails> encroachmentLocationMapFileDetails = new List<EncroachmentLocationMapFileDetails>();
                            for (int i = 0; i < encroachmentRegisterations.LocationMapFile.Count; i++)
                            {
                                string FilePath = fileHelper.SaveFile(LocationMapFilePath, encroachmentRegisterations.LocationMapFile[i]);
                                encroachmentLocationMapFileDetails.Add(new EncroachmentLocationMapFileDetails
                                {
                                    EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                    LocationMapFilePath = FilePath
                                });
                            }
                            foreach (var item in encroachmentLocationMapFileDetails)
                            {
                                result = await _encroachmentRegisterationService.SaveEncroachmentLocationMapFileDetails(item);
                            }
                        }
                        if (result)
                        {
                            #region Approval Proccess At 1st level start Added by Renu 21 April 2021
                            var workflowtemplatedata = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidInspection").Value));
                            var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);
                            for (int i = 0; i < DataFlow.Count; i++)
                            {
                                if (!DataFlow[i].parameterSkip)
                                {
                                    encroachmentRegisterations.ApprovedStatus = ApprovalStatus.Id;
                                    encroachmentRegisterations.PendingAt = approvalproccess.SendTo;
                                    result = await _encroachmentRegisterationService.UpdateBeforeApproval(encroachmentRegisterations.Id, encroachmentRegisterations);  //Update Table details 
                                    if (result)
                                    {
                                        approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                        approvalproccess.ProcessGuid = (_configuration.GetSection("workflowPreccessGuidInspection").Value);
                                        approvalproccess.ServiceId = encroachmentRegisterations.Id;
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
                                            var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidInspection").Value);
                                            var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                            Usernotification usernotification = new Usernotification();
                                            var replacement = notificationtemplate.Template.Replace("{proccess name}", "Inspection/Encroachment Register").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                            usernotification.Message = replacement;
                                            usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidInspection").Value);
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
                                bodyDTO.ApplicationName = "Inspection/Encroachment Register Application";
                                bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                                bodyDTO.SenderName = senderUser.User.Name;
                                bodyDTO.Link = linkhref;
                                bodyDTO.AppRefNo = encroachmentRegisterations.RefNo;
                                bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                                bodyDTO.Remarks = approvalproccess.Remarks;
                                bodyDTO.path = path;
                                string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                                #endregion

                                //sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                                #region Common Mail Genration
                                SentMailGenerationDto maildto = new SentMailGenerationDto();
                                maildto.strMailSubject = "Pending Inspection/Encroachment Register Application Approval Request Details ";
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
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            var result1 = await _encroachmentRegisterationService.GetAllEncroachmentRegisteration();
                            return View("Index", result1);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(encroachmentRegisterations);
                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(encroachmentRegisterations);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(encroachmentRegisterations);
                }
            }
            catch (Exception ex)
            {
                #region Roll Back of Transaction Added by Renu 26 April  2021 
                var deleteResult = false;
                if (encroachmentRegisterations.Id != 0)
                {
                    deleteResult = await _userNotificationService.RollBackEntry((_configuration.GetSection("workflowPreccessGuidWatchWard").Value), encroachmentRegisterations.Id);
                    deleteResult = await _approvalproccessService.RollBackEntry((_configuration.GetSection("workflowPreccessGuidWatchWard").Value), encroachmentRegisterations.Id);
                    deleteResult = await _encroachmentRegisterationService.RollBackEntryDetailsofEncroachmentRepeater(encroachmentRegisterations.Id);
                    deleteResult = await _encroachmentRegisterationService.RollBackEntryEncroachmentFirFileDetails(encroachmentRegisterations.Id);
                    deleteResult = await _encroachmentRegisterationService.RollBackEntryEncroachmentPhotoFileDetails(encroachmentRegisterations.Id);
                    deleteResult = await _encroachmentRegisterationService.RollBackEntryEncroachmentLocationMapFileDetails(encroachmentRegisterations.Id);
                    deleteResult = await _encroachmentRegisterationService.RollBackEntry(encroachmentRegisterations.Id);
                }
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(encroachmentRegisterations);
                #endregion
            }

        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var encroachmentRegisterations = await _encroachmentRegisterationService.FetchSingleResult(id);
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);
            if (encroachmentRegisterations == null)
            {
                return NotFound();
            }
            return View(encroachmentRegisterations);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var encroachmentRegisterations = await _encroachmentRegisterationService.FetchSingleResult(id);
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);
            if (encroachmentRegisterations == null)
            {
                return NotFound();
            }
            return View(encroachmentRegisterations);
        }
        public async Task<JsonResult> DetailsOfRepeater(int? Id)
        {
            Id = Id ?? 0;
            var data = await _encroachmentRegisterationService.GetDetailsOfEncroachment(Convert.ToInt32(Id));
            return Json(data.Select(x => new { x.CountOfStructure, x.DateOfEncroachment, x.Area, x.NameOfStructure, x.ReferenceNoOnLocation, x.Type, x.ConstructionStatus, x.ReligiousStructure }));
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, EncroachmentRegisteration encroachmentRegisterations)
        {
            var Data = await _encroachmentRegisterationService.FetchSingleResult(id);
            Data.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            Data.ZoneList = await _encroachmentRegisterationService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(Data.ZoneId);
            Data.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(Data.DivisionId);
            Data.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(Data.LocalityId);
            string PhotoFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:PhotoFilePath").Value.ToString();
            string LocationMapFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:LocationMapFilePath").Value.ToString();
            string FirfilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:FIRFilePath").Value.ToString();
            if (ModelState.IsValid)
            {
                encroachmentRegisterations.ModifiedBy = SiteContext.UserId;
                var result = await _encroachmentRegisterationService.Update(id, encroachmentRegisterations);
                if (result)
                {
                    FileHelper fileHelper = new FileHelper();
                    if (encroachmentRegisterations.NameOfStructure != null && encroachmentRegisterations.AreaApprox != null && encroachmentRegisterations.Type != null && encroachmentRegisterations.DateOfEncroachment != null && encroachmentRegisterations.CountOfStructure != null && encroachmentRegisterations.ReferenceNoOnLocation != null && encroachmentRegisterations.NameOfStructure.Count > 0 && encroachmentRegisterations.AreaApprox.Count > 0 && encroachmentRegisterations.Type.Count > 0 && encroachmentRegisterations.DateOfEncroachment.Count > 0 && encroachmentRegisterations.CountOfStructure.Count > 0 && encroachmentRegisterations.ReferenceNoOnLocation.Count > 0)
                    {
                        List<DetailsOfEncroachment> detailsOfEncroachment = new List<DetailsOfEncroachment>();
                        result = await _encroachmentRegisterationService.DeleteDetailsOfEncroachment(id);
                        for (int i = 0; i < encroachmentRegisterations.NameOfStructure.Count; i++)
                        {
                            detailsOfEncroachment.Add(new DetailsOfEncroachment
                            {
                                Area = encroachmentRegisterations.AreaApprox[i],
                                CountOfStructure = encroachmentRegisterations.CountOfStructure[i],
                                ReligiousStructure = encroachmentRegisterations.ReligiousStructure[i],
                                DateOfEncroachment = encroachmentRegisterations.DateOfEncroachment[i],
                                ConstructionStatus = encroachmentRegisterations.ConstructionStatus[i],
                                NameOfStructure = encroachmentRegisterations.NameOfStructure[i],
                                ReferenceNoOnLocation = encroachmentRegisterations.ReferenceNoOnLocation[i],
                                Type = encroachmentRegisterations.Type[i],
                                EncroachmentRegisterationId = encroachmentRegisterations.Id
                            });
                        }
                        foreach (var item in detailsOfEncroachment)
                        {
                            result = await _encroachmentRegisterationService.SaveDetailsOfEncroachment(item);
                        }
                    }
                    if (encroachmentRegisterations.Firfile != null && encroachmentRegisterations.Firfile.Count > 0)
                    {
                        List<EncroachmentFirFileDetails> encroachmentFirFileDetails = new List<EncroachmentFirFileDetails>();
                        result = await _encroachmentRegisterationService.DeleteEncroachmentFirFileDetails(id);
                        for (int i = 0; i < encroachmentRegisterations.Firfile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(FirfilePath, encroachmentRegisterations.Firfile[i]);
                            encroachmentFirFileDetails.Add(new EncroachmentFirFileDetails
                            {
                                EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                FirFilePath = FilePath
                            });
                        }
                        foreach (var item in encroachmentFirFileDetails)
                        {
                            result = await _encroachmentRegisterationService.SaveEncroachmentFirFileDetails(item);
                        }
                    }
                    if (encroachmentRegisterations.PhotoFile != null && encroachmentRegisterations.PhotoFile.Count > 0)
                    {
                        List<EncroachmentPhotoFileDetails> encroachmentPhotoFileDetails = new List<EncroachmentPhotoFileDetails>();
                        result = await _encroachmentRegisterationService.DeleteEncroachmentPhotoFileDetails(id);
                        for (int i = 0; i < encroachmentRegisterations.PhotoFile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(PhotoFilePath, encroachmentRegisterations.PhotoFile[i]);
                            encroachmentPhotoFileDetails.Add(new EncroachmentPhotoFileDetails
                            {
                                EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                PhotoFilePath = FilePath
                            });
                        }
                        foreach (var item in encroachmentPhotoFileDetails)
                        {
                            result = await _encroachmentRegisterationService.SaveEncroachmentPhotoFileDetails(item);
                        }
                    }
                    if (encroachmentRegisterations.LocationMapFile != null && encroachmentRegisterations.LocationMapFile.Count > 0)
                    {
                        List<EncroachmentLocationMapFileDetails> encroachmentLocationMapFileDetails = new List<EncroachmentLocationMapFileDetails>();
                        result = await _encroachmentRegisterationService.DeleteEncroachmentLocationMapFileDetails(id);
                        for (int i = 0; i < encroachmentRegisterations.LocationMapFile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(LocationMapFilePath, encroachmentRegisterations.LocationMapFile[i]);
                            encroachmentLocationMapFileDetails.Add(new EncroachmentLocationMapFileDetails
                            {
                                EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                LocationMapFilePath = FilePath
                            });
                        }
                        foreach (var item in encroachmentLocationMapFileDetails)
                        {
                            result = await _encroachmentRegisterationService.SaveEncroachmentLocationMapFileDetails(item);
                        }
                    }
                    if (result)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _encroachmentRegisterationService.GetAllEncroachmentRegisteration();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(encroachmentRegisterations);
                    }

                }
                else
                {
                    return View(encroachmentRegisterations);
                }
            }
            else
            {
                return View(encroachmentRegisterations);
            }
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _encroachmentRegisterationService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var result1 = await _encroachmentRegisterationService.GetAllEncroachmentRegisteration();
            return View("Index", result1);
        }

        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _encroachmentRegisterationService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _encroachmentRegisterationService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _encroachmentRegisterationService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }
        public async Task<IActionResult> DownloadFirfile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentFirFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentFirFileDetails(Id);
            string filename = Data.FirFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadLocationMapFile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentLocationMapFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentLocationMapFileDetails(Id);
            string filename = Data.LocationMapFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentPhotoFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentPhotoFileDetails(Id);
            string filename = Data.PhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }


        #region Fetch workflow data for approval prrocess Added by Renu 29 April 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidInspection").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion


        public async Task<IActionResult> EncroachmentRegisterList()
        {
            var result = await _encroachmentRegisterationService.GetAllEncroachmentRegisterlist((int)ApprovalActionStatus.Approved);
            List<EncroachmentRegisterListDto> data = new List<EncroachmentRegisterListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new EncroachmentRegisterListDto()
                    {
                        Id = result[i].Id,
                        Date = Convert.ToDateTime(result[i].Date).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].Date).ToString("dd-MMM-yyyy"),

                        Loaclity = result[i].PrimaryListNoNavigation.LocalityId == null ? "" : result[i].PrimaryListNoNavigation.Locality.Name == null ? "" : result[i].PrimaryListNoNavigation.Locality.Name,
                        KhasraNo = result[i].PrimaryListNoNavigation.KhasraNo == null ? "" : result[i].PrimaryListNoNavigation.KhasraNo.ToString(),
                        PrimaryListNo = result[i].PrimaryListNoNavigation.PrimaryListNo == null ? "" : result[i].PrimaryListNoNavigation.PrimaryListNo,
                        StatusOnGround = result[i].StatusOnGround.ToString(),
                        Status = result[i].ApprovedStatusNavigation == null ? "" : result[i].ApprovedStatusNavigation.SentStatusName.ToString(),

                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
