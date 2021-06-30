using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using Utility.Helper;
using EncroachmentDemolition.Filters;
using Core.Enum;
using System.Data;
using Service.IApplicationService;
using Dto.Master;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace EncroachmentDemolition.Controllers
{
    public class AnnexureAController : BaseController
    {
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
        public readonly IAnnexureAService _annexureAService;
        private readonly IWatchandwardService _watchandwardService;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        public IConfiguration _configuration;
        private readonly IUserProfileService _userProfileService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserNotificationService _userNotificationService;

        string targetPhotoPathLayout = "";
        string targetReportfilePathLayout = "";
        string PhotoFilePath = "";
        string LocationMapFilePath = "";
        string FirfilePath = "";
        string DocumentFilePath = "";

        public AnnexureAController(IEncroachmentRegisterationService encroachmentRegisterationService,
            IAnnexureAService annexureAService, IWatchandwardService watchandwardService, IConfiguration configuration,
            IWorkflowTemplateService workflowtemplateService, IApprovalProccessService approvalproccessService,
            IUserProfileService userProfileService, IHostingEnvironment hostingEnvironment,
            IUserNotificationService userNotificationService)
        {
            _encroachmentRegisterationService = encroachmentRegisterationService;
            _annexureAService = annexureAService;
            _configuration = configuration;
            _watchandwardService = watchandwardService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _userProfileService = userProfileService;
            _hostingEnvironment = hostingEnvironment;
            _userNotificationService = userNotificationService;
            targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
            targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();
            PhotoFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:PhotoFilePath").Value.ToString();
            LocationMapFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:LocationMapFilePath").Value.ToString();
            FirfilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:FIRFilePath").Value.ToString();
            DocumentFilePath = _configuration.GetSection("FilePaths:FixingDemolitionFiles:DocumentFilePath").Value.ToString();

        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AnnexureASearchDto model)
        {
            var result = await _annexureAService.GetPagedDetails(model, (int)ApprovalActionStatus.Approved, SiteContext.ZoneId ?? 0);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            Fixingdemolition Model = new Fixingdemolition();
            var Data = await _encroachmentRegisterationService.FetchSingleResult(id);
            Data.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            Data.ZoneList = await _encroachmentRegisterationService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(Data.ZoneId);
            Data.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(Data.ZoneId);
            Data.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(Data.LocalityId);
            Model.Demolitionchecklist = await _annexureAService.GetDemolitionchecklist();
            Model.Demolitionprogram = await _annexureAService.GetDemolitionprogram();
            Model.Demolitiondocument = await _annexureAService.GetDemolitiondocument();
            Model.Encroachment = Data;
            if (Model.Encroachment.WatchWardId == null)
                Model.Encroachment.WatchWardId = 0;
            return View(Model);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Fixingdemolition fixingdemolition)
        {
            try
            {
                var encroachment = await _encroachmentRegisterationService.FetchSingleResult(fixingdemolition.EncroachmentId);
                Random r = new Random();
                int num = r.Next();
                fixingdemolition.RefNo = DateTime.Now.Year.ToString()+ encroachment.Zone.Code + num.ToString();
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
                                return View(fixingdemolition);
                            }

                            fixingdemolition.ApprovalZoneId = SiteContext.ZoneId;
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
                var Data = await _encroachmentRegisterationService.FetchSingleResult(id);
                fixingdemolition.Demolitionprogram = await _annexureAService.GetDemolitionprogram();

                fixingdemolition.Encroachment = Data;
                fixingdemolition.Id = 0;
                if (fixingdemolition.EncroachmentId == 0)
                {
                    return NotFound();
                }
                fixingdemolition.EncroachmentId = fixingdemolition.Encroachment.Id;
                var result = await _annexureAService.Create(fixingdemolition);

                List<Fixingprogram> fixingprogram = new List<Fixingprogram>();
                for (int i = 0; i < fixingdemolition.DemolitionProgramId.Count(); i++)
                {
                    fixingprogram.Add(new Fixingprogram
                    {

                        DemolitionProgramId = (int)fixingdemolition.DemolitionProgramId[i],
                        ItemsDetails = fixingdemolition.ItemsDetails[i],
                        FixingdemolitionId = fixingdemolition.Id
                    });
                }
                foreach (var item in fixingprogram)
                {
                    result = await _annexureAService.SaveFixingprogram(item);
                }
                List<Fixingchecklist> fixingchecklist = new List<Fixingchecklist>();
                for (int i = 0; i < fixingdemolition.DemolitionChecklistId.Count(); i++)
                {
                    fixingchecklist.Add(new Fixingchecklist
                    {

                        DemolitionChecklistId = (int)fixingdemolition.DemolitionChecklistId[i],
                        ChecklistDetails = fixingdemolition.ChecklistDetails[i],
                        FixingdemolitionId = fixingdemolition.Id
                    });
                }
                foreach (var item in fixingchecklist)
                {
                    result = await _annexureAService.Savefixingchecklist(item);
                }
                FileHelper fileHelper = new FileHelper();

                List<Fixingdocument> fixingdocument = new List<Fixingdocument>();
                for (int i = 0; i < fixingdemolition.DemolitionDocumentId.Count; i++)
                {
                    string FilePath = null;
                    if (fixingdemolition.DocumentDetails != null && fixingdemolition.DocumentDetails.Count > 0)
                        FilePath = fileHelper.SaveFile1(DocumentFilePath, fixingdemolition.DocumentDetails[i]);
                    fixingdocument.Add(new Fixingdocument
                    {
                        DemolitionDocumentId = (int)fixingdemolition.DemolitionDocumentId[i],
                        DocumentDetails = FilePath,
                        FixingdemolitionId = fixingdemolition.Id
                    });
                }
                foreach (var item in fixingdocument)
                {
                    result = await _annexureAService.SaveFixingdocument(item);
                }

                if (result)
                {
                    #region Approval Proccess At 1st level start Added by Renu 21 April 2021
                    var workflowtemplatedata = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value));
                    var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);
                    for (int i = 0; i < DataFlow.Count; i++)
                    {
                        if (!DataFlow[i].parameterSkip)
                        {
                            fixingdemolition.ApprovedStatus = ApprovalStatus.Id;
                            fixingdemolition.PendingAt = approvalproccess.SendTo;
                            result = await _annexureAService.UpdateBeforeApproval(fixingdemolition.Id, fixingdemolition);  //Update Table details 
                            if (result)
                            {
                                approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                approvalproccess.ProcessGuid = (_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value);
                                approvalproccess.ServiceId = fixingdemolition.Id;
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

                            }

                            break;
                        }
                    }

                    #endregion

                    #region Approval Proccess  Mail Generation Added by Renu 10 May 2021
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
                        bodyDTO.ApplicationName = "Request for Demolition Application";
                        bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                        bodyDTO.SenderName = senderUser.User.Name;
                        bodyDTO.Link = linkhref;
                        bodyDTO.AppRefNo = fixingdemolition.RefNo;
                        bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                        bodyDTO.Remarks = approvalproccess.Remarks;
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
                    #endregion
                    ViewBag.Message = Alert.Show(Messages.AddAndApprovalRecordSuccess, "", AlertType.Success);
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(fixingdemolition);
                }
            }
            catch (Exception ex)
            {
                #region Roll Back of Transaction Added by Renu 26 April  2021 
                var deleteResult = false;
                if (fixingdemolition.Id != 0)
                {
                    deleteResult = await _userNotificationService.RollBackEntry((_configuration.GetSection("workflowPreccessGuidWatchWard").Value), fixingdemolition.Id);
                    deleteResult = await _approvalproccessService.RollBackEntry((_configuration.GetSection("workflowPreccessGuidWatchWard").Value), fixingdemolition.Id);
                    deleteResult = await _annexureAService.RollBackEntryFixingprogram(fixingdemolition.Id);
                    deleteResult = await _annexureAService.RollBackEntryFixingchecklist(fixingdemolition.Id);
                    deleteResult = await _annexureAService.RollBackEntryFixingdocument(fixingdemolition.Id);
                    deleteResult = await _annexureAService.RollBackEntry(fixingdemolition.Id);
                }
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(fixingdemolition);
                #endregion
            }
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            List<Fixingdemolition> list = await _annexureAService.GetFixingdemolition(id);
            return View(list);
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

        #region Fetch workflow data for approval prrocess Added by Renu 30 April 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidRequestDemolition").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion

        public async Task<IActionResult> RequestForFixingDemolitionProgrammeList()
        {
            var result = await _annexureAService.GetAllRequestForFixingDemolitionList((int)ApprovalActionStatus.Approved);
            List<RequestForFixingDemolitionProgrammeListDto> data = new List<RequestForFixingDemolitionProgrammeListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new RequestForFixingDemolitionProgrammeListDto()
                    {
                        Id = result[i].Id,
                        Date = Convert.ToDateTime(result[i].EncrochmentDate).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].EncrochmentDate).ToString("dd-MMM-yyyy"),

                        Loaclity = result[i].Locality == null ? "" :  result[i].Locality.Name,
                        KhasraNo = result[i].KhasraNo == null ? "" : result[i].KhasraNo,
                        PoliceStation = result[i].PoliceStation,
                        SecurityGuardOnDuty = result[i].SecurityGuardOnDuty,
                        Status = result[i].ApprovedStatusNavigation == null ? "" : result[i].ApprovedStatusNavigation.SentStatusName.ToString(),

                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


    }
}