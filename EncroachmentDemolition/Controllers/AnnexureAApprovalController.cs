using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using System.IO;
using System.Linq;
using EncroachmentDemolition.Filters;
using Core.Enum;
using System.Data;
using Newtonsoft.Json;
using Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using Dto.Master;

namespace EncroachmentDemolition.Controllers
{
    public class AnnexureAApprovalController : BaseController
    {
        public readonly IAnnexureAApprovalService _annexureAApprovalService;
        public readonly IAnnexureAService _annexureAService;
        public IConfiguration _configuration;
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
        private readonly IWatchandwardService _watchandwardService;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserProfileService _userProfileService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserNotificationService _userNotificationService;

        string targetPhotoPathLayout = "";
        string targetReportfilePathLayout = "";
        string PhotoFilePath = "";
        string LocationMapFilePath = "";
        string FirfilePath = "";
        string ApprovalDocumentPath = "";
        string DocumentFilePath = "";

        public AnnexureAApprovalController(IEncroachmentRegisterationService encroachmentRegisterationService,
            IConfiguration configuration, IWatchandwardService watchandwardService,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IAnnexureAService annexureAService, IAnnexureAApprovalService annexureAApprovalService,
            IUserProfileService userProfileService, IHostingEnvironment hostingEnvironment,
            IUserNotificationService userNotificationService)
        {
            _encroachmentRegisterationService = encroachmentRegisterationService;
            _configuration = configuration;
            _watchandwardService = watchandwardService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _annexureAService = annexureAService;
            _annexureAApprovalService = annexureAApprovalService;
            _userProfileService = userProfileService;
            _hostingEnvironment = hostingEnvironment;
            _userNotificationService = userNotificationService;
            targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
            targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();
            PhotoFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:PhotoFilePath").Value.ToString();
            LocationMapFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:LocationMapFilePath").Value.ToString();
            FirfilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:FIRFilePath").Value.ToString();
            DocumentFilePath = _configuration.GetSection("FilePaths:FixingDemolitionFiles:DocumentFilePath").Value.ToString();
            ApprovalDocumentPath = _configuration.GetSection("FilePaths:FixingDemolitionFiles:ApprovalDocumentPath").Value.ToString();

        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var Msg = TempData["Message"] as string;
            if (Msg != null)
                ViewBag.Message = Msg;
            Fixingdemolition data = new Fixingdemolition();
            var dropdownValue = await GetApprovalStatusDropdownListAtIndex();
            int[] actions = Array.ConvertAll(dropdownValue, int.Parse);
            data.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(actions.Distinct().ToArray());

            return View(data);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AnnexureAApprovalSearchDto model)
        {
            var result = await _annexureAApprovalService.GetPagedAnnexureA(model, SiteContext.UserId, SiteContext.ZoneId ?? 0);
            ViewBag.IsApproved = model.StatusId;
            return PartialView("_List", result);
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _annexureAApprovalService.FetchSingleResult(id);
            ViewBag.Items = await _userProfileService.GetRole();
            await BindApprovalStatusDropdown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Fixingdemolition fixingdemolition)
        {
            var result = false;
            var IsApplicationPendingAtUserEnd = await _annexureAApprovalService.IsApplicationPendingAtUserEnd(id, SiteContext.UserId);
            if (IsApplicationPendingAtUserEnd)
            {
                var Data = await _annexureAApprovalService.FetchSingleResult(id);
                FileHelper fileHelper = new FileHelper();
                #region Approval Proccess At Further level start Added by Renu 16 march 2021
                var FirstApprovalProcessData = await _approvalproccessService.FirstApprovalProcessData((_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value), fixingdemolition.Id);
                var ApprovalProccessBackId = _approvalproccessService.GetPreviousApprovalId((_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value), fixingdemolition.Id);
                var ApprovalProcessBackData = await _approvalproccessService.FetchApprovalProcessDocumentDetails(ApprovalProccessBackId);
                var checkLastApprovalStatuscode = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalProcessBackData.Status));

                var DataFlow = await DataAsync(ApprovalProcessBackData.Version);

                Approvalproccess approvalproccess = new Approvalproccess();

                /*Check if zonewise then aprovee user must have zoneid*/
                if (fixingdemolition.ApprovalStatusCode == ((int)ApprovalActionStatus.Forward) && checkLastApprovalStatuscode.StatusCode != ((int)ApprovalActionStatus.QueryForward))
                {
                    for (int i = 0; i < DataFlow.Count; i++)
                    {
                        if (!DataFlow[i].parameterSkip)
                        {
                            if (i == ApprovalProcessBackData.Level - 1 && Convert.ToInt32(DataFlow[i].parameterLevel) == ApprovalProcessBackData.Level)
                            {
                                for (int d = i + 1; d < DataFlow.Count; d++)
                                {
                                    if (!DataFlow[d].parameterSkip)
                                    {
                                        if (DataFlow[d].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                                        {
                                            if (SiteContext.ZoneId == null)
                                            {
                                                ViewBag.Items = await _userProfileService.GetRole();
                                                await BindApprovalStatusDropdown(fixingdemolition);
                                                ViewBag.Message = Alert.Show("Your Zone is not available , Without zone application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                                return View(fixingdemolition);
                                            }

                                            fixingdemolition.ApprovalZoneId = SiteContext.ZoneId;
                                        }
                                        break;
                                    }
                                }
                            }

                        }
                    }
                }

                if (ApprovalProcessBackData.Level == FirstApprovalProcessData.Level && fixingdemolition.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))//check if revert available at first level
                {
                    result = false;
                    ViewBag.Items = await _userProfileService.GetRole();
                    await BindApprovalStatusDropdown(fixingdemolition);
                    ViewBag.Message = Alert.Show("Application cannot be Reverted at First Level", "", AlertType.Warning);
                    return View(fixingdemolition);
                }
                else
                {
                    /* Update last record pending status in Approval Process Table*/
                    result = true;
                    approvalproccess.PendingStatus = 0;
                    result = await _approvalproccessService.UpdatePreviousApprovalProccess(ApprovalProccessBackId, approvalproccess, SiteContext.UserId);

                    /*Now New row added in Approval process*/
                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                    approvalproccess.ProcessGuid = (_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value);
                    approvalproccess.ServiceId = fixingdemolition.Id;
                    approvalproccess.SendFrom = SiteContext.UserId.ToString();
                    approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
                    approvalproccess.PendingStatus = 1;
                    approvalproccess.Remarks = fixingdemolition.ApprovalRemarks; ///May be comment
                    approvalproccess.Status = Convert.ToInt32(fixingdemolition.ApprovalStatus);
                    approvalproccess.Version = ApprovalProcessBackData.Version;
                    approvalproccess.DocumentName = fixingdemolition.ApprovalDocument == null ? null : fileHelper.SaveFile1(ApprovalDocumentPath, fixingdemolition.ApprovalDocument);


                    if (checkLastApprovalStatuscode.StatusCode == ((int)ApprovalActionStatus.QueryForward)) // check islast approvalrow is of query type then return to the same user
                    {
                        approvalproccess.Level = ApprovalProcessBackData.Level;
                        approvalproccess.SendTo = ApprovalProcessBackData.SendFrom;
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

                        result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                        #region Insert Into usernotification table Added By Renu 18 June 2021
                        if (result == true && approvalproccess.SendTo != null)
                        {
                            var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidRequestDemolition").Value);
                            var user = await _userProfileService.GetUserById(SiteContext.UserId);
                            Usernotification usernotification = new Usernotification();
                            var replacement = notificationtemplate.Template.Replace("{proccess name}", "Request for Demolition").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                            usernotification.Message = replacement;
                            usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidRequestDemolition").Value);
                            usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                            usernotification.ServiceId = approvalproccess.ServiceId;
                            usernotification.SendFrom = approvalproccess.SendFrom;
                            usernotification.SendTo = approvalproccess.SendTo;
                            result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                        }
                        #endregion
                        if (result)
                        {
                            fixingdemolition.ApprovedStatus = Convert.ToInt32(fixingdemolition.ApprovalStatus);
                            fixingdemolition.PendingAt = ApprovalProcessBackData.SendFrom;
                            result = await _annexureAService.UpdateBeforeApproval(fixingdemolition.Id, fixingdemolition);  //Update Table details 
                        }
                    }
                    else
                    {
                        for (int i = 0; i < DataFlow.Count; i++)
                        {
                            if (!DataFlow[i].parameterSkip)
                            {
                                if (i == ApprovalProcessBackData.Level - 1 && Convert.ToInt32(DataFlow[i].parameterLevel) == ApprovalProcessBackData.Level)
                                {
                                    if (result)
                                    {
                                        if (fixingdemolition.ApprovalStatusCode == ((int)ApprovalActionStatus.QueryForward))
                                        {
                                            approvalproccess.Level = ApprovalProcessBackData.Level;
                                            approvalproccess.SendTo = fixingdemolition.ApprovalUserId.ToString();
                                        }
                                        else if (fixingdemolition.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))
                                        {
                                            /*Check previous level for revert */
                                            for (int d = i - 1; d >= 0; d--)
                                            {
                                                if (!DataFlow[d].parameterSkip)
                                                {
                                                    var CheckLastUserForRevert = await _approvalproccessService.CheckLastUserForRevert((_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value), fixingdemolition.Id, Convert.ToInt32(DataFlow[i].parameterLevel));
                                                    approvalproccess.SendTo = CheckLastUserForRevert == null ? null : CheckLastUserForRevert.SendFrom;
                                                    approvalproccess.Level = Convert.ToInt32(DataFlow[d].parameterLevel);

                                                    break;
                                                }
                                            }
                                        }
                                        else if (fixingdemolition.ApprovalStatusCode == ((int)ApprovalActionStatus.Rejected))
                                        {
                                            approvalproccess.Level = ApprovalProcessBackData.Level;
                                            approvalproccess.SendTo = null;
                                            approvalproccess.PendingStatus = 0;
                                        }
                                        else  // Forward Check
                                        {
                                            if (i == DataFlow.Count - 1)
                                            {
                                                approvalproccess.Level = 0;
                                                approvalproccess.SendTo = null;
                                                approvalproccess.PendingStatus = 0;
                                            }
                                            else
                                            {
                                                /*Conditional check and other role user wise checks*/
                                                for (int d = i + 1; d < DataFlow.Count; d++)
                                                {
                                                    if (!DataFlow[d].parameterSkip)
                                                    {
                                                        approvalproccess.Level = Convert.ToInt32(DataFlow[d].parameterLevel);
                                                        break;
                                                    }
                                                }
                                                approvalproccess.SendTo = fixingdemolition.ApprovalUserId.ToString();

                                            }
                                        }



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
                                        else if (approvalproccess.SendTo == null && (fixingdemolition.ApprovalStatusCode != ((int)ApprovalActionStatus.Rejected) && fixingdemolition.ApprovalStatusCode != ((int)ApprovalActionStatus.Approved)))
                                        {
                                            ViewBag.Items = await _userProfileService.GetRole();
                                            await BindApprovalStatusDropdown(fixingdemolition);
                                            ViewBag.Message = Alert.Show("No user found at the next approval level, In this case, the system is unable to process your request. Please contact to the system administrator.", "", AlertType.Warning);
                                            return View(fixingdemolition);
                                        }
                                        #endregion

                                        result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                                        #region Insert Into usernotification table Added By Renu 18 June 2021
                                        if (result == true && approvalproccess.SendTo != null)
                                        {
                                            var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidRequestDemolition").Value);
                                            var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                            Usernotification usernotification = new Usernotification();
                                            var replacement = notificationtemplate.Template.Replace("{proccess name}", "Request for Demolition").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                            usernotification.Message = replacement;
                                            usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidRequestDemolition").Value);
                                            usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                            usernotification.ServiceId = approvalproccess.ServiceId;
                                            usernotification.SendFrom = approvalproccess.SendFrom;
                                            usernotification.SendTo = approvalproccess.SendTo;
                                            result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                                        }
                                        #endregion

                                        if (result)
                                        {
                                            if (fixingdemolition.ApprovalStatusCode == ((int)ApprovalActionStatus.QueryForward))
                                            {
                                                fixingdemolition.ApprovedStatus = Convert.ToInt32(fixingdemolition.ApprovalStatus);
                                                fixingdemolition.PendingAt = approvalproccess.SendTo;
                                            }
                                            else if (fixingdemolition.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))
                                            {
                                                fixingdemolition.ApprovedStatus = Convert.ToInt32(fixingdemolition.ApprovalStatus);
                                                fixingdemolition.PendingAt = approvalproccess.SendTo;
                                            }
                                            else if (fixingdemolition.ApprovalStatusCode == ((int)ApprovalActionStatus.Rejected))
                                            {
                                                fixingdemolition.ApprovedStatus = Convert.ToInt32(fixingdemolition.ApprovalStatus);
                                                fixingdemolition.PendingAt = "0";
                                            }
                                            else
                                            {
                                                if (i == DataFlow.Count - 1)
                                                {
                                                    fixingdemolition.ApprovedStatus = Convert.ToInt32(fixingdemolition.ApprovalStatus);
                                                    fixingdemolition.PendingAt = "0";
                                                }
                                                else
                                                {
                                                    fixingdemolition.ApprovedStatus = Convert.ToInt32(fixingdemolition.ApprovalStatus);
                                                    fixingdemolition.PendingAt = approvalproccess.SendTo;
                                                }
                                            }
                                            result = await _annexureAService.UpdateBeforeApproval(fixingdemolition.Id, fixingdemolition);  //Update Table details 
                                        }
                                    }
                                    break;




                                }
                            }
                        }

                    }
                    var sendMailResult = false;

                    var DataApprovalSatatusMsg = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(fixingdemolition.ApprovalStatus));

                    if (approvalproccess.SendTo != null)
                    {
                        #region Mail Generate
                        //At successfull completion send mail and sms
                        Uri uri = new Uri("https://www.managemybusinessess.com/");
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
                        bodyDTO.ApplicationName = "Request for Demolition Application";
                        bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                        bodyDTO.SenderName = senderUser.User.Name;
                        bodyDTO.Link = linkhref;
                        bodyDTO.AppRefNo = fixingdemolition.RefNo;
                        bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                        bodyDTO.Remarks = fixingdemolition.ApprovalRemarks;
                        bodyDTO.path = path;
                        string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                        #endregion

                        //sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                        #region Common Mail Genration
                        SentMailGenerationDto maildto = new SentMailGenerationDto();
                        maildto.strMailSubject = "Pending Request for Demolition Application Approval Request Details ";
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
                    if (result)
                    {
                        if (sendMailResult)
                            ViewBag.Message = Alert.Show("Record " + DataApprovalSatatusMsg.SentStatusName + " Successfully  and Information Sent on emailid and Mobile No", "", AlertType.Success);
                        else if (approvalproccess.PendingStatus == 0)
                            ViewBag.Message = Alert.Show("Record " + DataApprovalSatatusMsg.SentStatusName + " Successfully", "", AlertType.Success);
                        else
                            ViewBag.Message = Alert.Show("Record " + DataApprovalSatatusMsg.SentStatusName + " Successfully  But Unable to Sent information on emailid or mobile no. due to network issue", "", AlertType.Info);

                        Fixingdemolition data = new Fixingdemolition();
                        var dropdownValue = await GetApprovalStatusDropdownListAtIndex();
                        int[] actions = Array.ConvertAll(dropdownValue, int.Parse);
                        data.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(actions.Distinct().ToArray());

                        return View("Index", data);
                    }
                    else
                    {
                        ViewBag.Items = await _userProfileService.GetRole();
                        await BindApprovalStatusDropdown(fixingdemolition);
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(fixingdemolition);
                    }


                }
                #endregion

            }
            else
            {
                ViewBag.Message = Alert.Show("Application Already Submited ", "", AlertType.Warning);
                TempData["Message"] = Alert.Show("Application Already Submited ", "", AlertType.Warning);

                return RedirectToAction("Index");
            }
        }

        #region Watch & Ward  Details
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
            string path = targetPhotoPathLayout + Data.PhotoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        #endregion

        #region EncroachmentRegisteration Details
        public async Task<PartialViewResult> EncroachmentRegisterView(int id)
        {
            var encroachmentRegisterations = await _encroachmentRegisterationService.FetchSingleResult(id);
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);

            return PartialView("_EncroachmentRegisterView", encroachmentRegisterations);
        }

        public async Task<JsonResult> DetailsOfRepeater(int? Id)
        {
            Id = Id ?? 0;
            var data = await _encroachmentRegisterationService.GetDetailsOfEncroachment(Convert.ToInt32(Id));
            return Json(data.Select(x => new { x.CountOfStructure, x.DateOfEncroachment, x.Area, x.NameOfStructure, x.ReferenceNoOnLocation, x.Type, x.ConstructionStatus, x.ReligiousStructure }));
        }

        public async Task<IActionResult> DownloadFirfile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentFirFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentFirFileDetails(Id);
            string filename = FirfilePath + Data.FirFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadLocationMapFile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentLocationMapFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentLocationMapFileDetails(Id);
            string filename = LocationMapFilePath + Data.LocationMapFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentPhotoFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentPhotoFileDetails(Id);
            string filename = PhotoFilePath + Data.PhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        #endregion

        #region AnnexureA Details
        public async Task<PartialViewResult> AnnexureADetails(int id)
        {
            var Data = await _annexureAService.FetchSingleResult(id);
            Data.Demolitionchecklist = await _annexureAService.GetDemolitionchecklist();
            Data.Demolitionprogram = await _annexureAService.GetDemolitionprogram();
            Data.Demolitiondocument = await _annexureAService.GetDemolitiondocument();
            return PartialView("_AnnexureAView", Data);
        }

        public async Task<FileResult> ViewDocumentAnnexureA(int Id)
        {
            FileHelper file = new FileHelper();
            Fixingdocument Data = await _annexureAService.GetAnnexureAfiledetails(Id);
            string path = DocumentFilePath +  Data.DocumentDetails;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        #endregion



        #region History Details Only For Approval Page Added By Renu 26 April 2021
        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _approvalproccessService.GetHistoryDetails((_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value), id);

            return PartialView("_HistoryDetails", Data);
        }

        #endregion

        #region Fetch workflow data for approval prrocess Added by Renu 26 April 2021
        private async Task<List<TemplateStructure>> DataAsync(string version)
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuidWithVersion((_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value), version);
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }

        public async Task<IActionResult> ViewDocumentApprovalProccess(int Id)
        {
            FileHelper file = new FileHelper();
            Approvalproccess Data = await _approvalproccessService.FetchApprovalProcessDocumentDetails(Id);
            string filename = ApprovalDocumentPath + Data.DocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }
        #endregion

        #region Approval Status Dropdown Bind on User rights Basis Code Added By Renu 26 April  2021
        async Task BindApprovalStatusDropdown(Fixingdemolition Data)
        {
            var dropdownValue = await GetApprovalStatusDropdownList(Data.Id);
            List<int> dropdownValue1 = ConvertStringListToIntList(dropdownValue);
            Data.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(dropdownValue1.ToArray());
            for (int i = 0; i < Data.ApprovalStatusList.Count; i++)
            {
                if (Data.ApprovalStatusList[i].StatusCode == (int)ApprovalActionStatus.Revert)
                    ViewBag.RevertCodeValue = Data.ApprovalStatusList[i].StatusCode;
                else if (Data.ApprovalStatusList[i].StatusCode == (int)ApprovalActionStatus.Approved)
                    ViewBag.ApprovedCodeValue = Data.ApprovalStatusList[i].StatusCode;
                else if (Data.ApprovalStatusList[i].StatusCode == (int)ApprovalActionStatus.Forward)
                    ViewBag.ForwardCodeValue = Data.ApprovalStatusList[i].StatusCode;
                else if (Data.ApprovalStatusList[i].StatusCode == (int)ApprovalActionStatus.QueryForward)
                    ViewBag.QueryForwardCodeValue = Data.ApprovalStatusList[i].StatusCode;
            }
        }
        public async Task<List<string>> GetApprovalStatusDropdownList(int serviceid)  //Bind Dropdown of Approval Status
        {
            List<string> dropdown = null;
            var ApprovalProccessBackId = _approvalproccessService.GetPreviousApprovalId((_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value), serviceid);
            var ApprovalProcessBackData = await _approvalproccessService.FetchApprovalProcessDocumentDetails(ApprovalProccessBackId);
            var checkLastApprovalStatuscode = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalProcessBackData.Status));

            var DataFlow = await DataAsync(ApprovalProcessBackData.Version);

            if (checkLastApprovalStatuscode.StatusCode != ((int)ApprovalActionStatus.QueryForward))
            {
                for (int i = 0; i < DataFlow.Count; i++)
                {
                    if (i == ApprovalProcessBackData.Level - 1 && Convert.ToInt32(DataFlow[i].parameterLevel) == ApprovalProcessBackData.Level)
                    {
                        dropdown = (List<string>)DataFlow[i].parameterAction;
                        return (dropdown);
                        //  break;
                    }
                }
            }
            else
            {
                var ApprovalStatusApproved = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);
                dropdown = new List<string>();
                dropdown.Add(ApprovalStatusApproved.Id.ToString());
            }
            return (List<string>)dropdown;
        }

        public List<int> ConvertStringListToIntList(List<string> list)
        {
            List<int> resultList = new List<int>();
            for (int i = 0; i < list.Count; i++)
                resultList.Add(Convert.ToInt32(list[i]));

            return resultList;
        }

        public async Task<string[]> GetApprovalStatusDropdownListAtIndex()  //Bind Dropdown of Approval Status
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<string> dropdown = null;
            int col = 0;
            var DataFlow = await _workflowtemplateService.GetWorkFlowDataOnGuid((_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value));

            for (int i = 0; i < DataFlow.Count; i++)
            {
                var template = DataFlow[i].Template;
                List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
                for (int j = 0; j < ObjList.Count; j++)
                {
                    for (int k = 0; k < ObjList[j].parameterAction.Count; k++)
                    {
                        if (col > 0)
                            stringBuilder.Append(",");
                        stringBuilder.Append(ObjList[j].parameterAction[k]);
                        col++;
                    }
                }

            }
            string[] stringArray = stringBuilder.ToString().Split(',').ToArray();
            return stringArray;
        }
        #endregion

        #region Approval Related changes Added By Renu 26 April  2021
        [HttpPost]
        public async Task<IActionResult> Back()
        {
            return Redirect(_configuration.GetSection("ApprovalProccessPath:SiteMaster").Value.ToString());
        }

        [HttpGet]
        public async Task<JsonResult> GetApprvoalStatus(string value)
        {
            int Id = Convert.ToInt32(value);
            var data = await _approvalproccessService.FetchSingleApprovalStatus(Id);
            return Json(data);
        }

        [HttpGet]
        public async Task<JsonResult> GetUserList(string value)
        {
            int RoleId = Convert.ToInt32(value);
            var data = await _userProfileService.GetUserSkippingItsOwnConcatedName(RoleId, SiteContext.UserId);
            return Json(data);
        }

        [HttpGet]
        public async Task<JsonResult> GetForwardedUserList(string value)
        {
            int serviceid = Convert.ToInt32(value);
            List<string> dropdown = null;
            var ApprovalProccessBackId = _approvalproccessService.GetPreviousApprovalId((_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value), serviceid);
            var ApprovalProcessBackData = await _approvalproccessService.FetchApprovalProcessDocumentDetails(ApprovalProccessBackId);
            var checkLastApprovalStatuscode = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalProcessBackData.Status));

            var DataFlow = await DataAsync(ApprovalProcessBackData.Version);

            List<string> JsonMsg = new List<string>();
            if (checkLastApprovalStatuscode.StatusCode != ((int)ApprovalActionStatus.QueryForward))
            {
                for (int i = 0; i < DataFlow.Count; i++)
                {
                    if (!DataFlow[i].parameterSkip)
                    {
                        if (i == ApprovalProcessBackData.Level - 1 && Convert.ToInt32(DataFlow[i].parameterLevel) == ApprovalProcessBackData.Level)
                        {
                            for (int d = i + 1; d < DataFlow.Count; d++)
                            {
                                if (!DataFlow[d].parameterSkip)
                                {
                                    if (DataFlow[d].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                                    {
                                        if (SiteContext.ZoneId == null)
                                        {
                                            JsonMsg.Add("false");
                                            JsonMsg.Add("ZoneId not available for next level, untill then Submittion is not possible");
                                            return Json(JsonMsg);
                                        }

                                    }
                                    if (DataFlow[d].parameterValue == (_configuration.GetSection("ApprovalRoleType").Value))
                                    {
                                        for (int b = 0; b < DataFlow[d].parameterName.Count; b++)
                                        {
                                            List<UserProfileInfoDetailsDto> UserListRoleBasis = null;
                                            if (DataFlow[d].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                                                UserListRoleBasis = await _userProfileService.GetUserOnRoleZoneBasisConcatedName(Convert.ToInt32(DataFlow[d].parameterName[b]), SiteContext.ZoneId ?? 0);
                                            else
                                                UserListRoleBasis = await _userProfileService.GetUserOnRoleBasisConcatedName(Convert.ToInt32(DataFlow[d].parameterName[b]));

                                            if (UserListRoleBasis.Count == 0)
                                            {
                                                JsonMsg.Add("false");
                                                JsonMsg.Add("No user found at the next approval level, In this case, the system is unable to process your request. Please contact to the system administrator.");
                                                return Json(JsonMsg);
                                            }
                                            else
                                            {
                                                return Json(UserListRoleBasis);
                                            }

                                        }
                                    }
                                    else
                                    {
                                        string SendTo = String.Join(",", (DataFlow[d].parameterName));
                                        if (DataFlow[d].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                                        {
                                            StringBuilder multouserszonewise = new StringBuilder();
                                            int col = 0;
                                            if (SendTo != null)
                                            {
                                                string[] multiTo = SendTo.Split(',');
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
                                                SendTo = multouserszonewise.ToString();
                                            }
                                        }

                                        if (SendTo != "")
                                        {
                                            int[] nums = Array.ConvertAll(SendTo.Split(','), int.Parse);
                                            var data = await _userProfileService.UserListSkippingmultiusersConcatedName(nums);
                                            return Json(data);
                                        }
                                        else
                                        {
                                            JsonMsg.Add("false");
                                            JsonMsg.Add("No user found at the next approval level, In this case, the system is unable to process your request. Please contact to the system administrator.");
                                            return Json(JsonMsg);
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                int[] nums = Array.ConvertAll(ApprovalProcessBackData.SendFrom.Split(','), int.Parse);
                var data = await _userProfileService.UserListSkippingmultiusersConcatedName(nums);
                return Json(data);
            }
            return Json(dropdown);
        }

        // [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _annexureAApprovalService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        #endregion

        //  [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> FixingdemolitionApprovalList()
        {
            var result = await _annexureAApprovalService.GetAllFixingdemolition();
            List<FixingdemolitionApprovalListDto> data = new List<FixingdemolitionApprovalListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new FixingdemolitionApprovalListDto()
                    {
                        Id = result[i].Id,
                        InspectionDate = Convert.ToDateTime(result[i].Encroachment.EncrochmentDate).ToString("dd-MMM-yyyy"),
                        LocalityVillage = result[i].Encroachment.Locality == null ? "" : result[i].Encroachment.Locality.Name,
                        KhasraNo = result[i].Encroachment.KhasraNo == null ? "" : result[i].Encroachment.KhasraNo,
                        Status = result[i].ApprovedStatusNavigation == null ? "" : result[i].ApprovedStatusNavigation.SentStatusName,

                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
