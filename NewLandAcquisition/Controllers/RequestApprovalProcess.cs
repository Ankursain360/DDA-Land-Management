using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Drawing;
using System.Drawing.Imaging;
using NewLandAcquisition.Filters;
using Core.Enum;


using Dto.Common;
using Dto.Master;
using Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace NewLandAcquisition.Controllers
{
    public class RequestApprovalProcess : BaseController
    {
        public readonly IRequestApprovalProcessService _requestApprovalProcessService;
        private readonly IRequestService _requestService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly INewlandannexure2Service _newlandannexure2Service;
        private readonly IUserProfileService _userProfileService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserNotificationService _userNotificationService;

        string ApprovalDocumentPath = string.Empty;


        public RequestApprovalProcess(IRequestApprovalProcessService requestApprovalProcessService,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IConfiguration configuration, IRequestService requestService, INewlandannexure2Service newlandannexure2Service,
            IUserProfileService userProfileService, IHostingEnvironment hostingEnvironment,
            IUserNotificationService userNotificationService)
        {
            _workflowtemplateService = workflowtemplateService;
            _requestApprovalProcessService = requestApprovalProcessService;
            _requestService = requestService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
            _newlandannexure2Service = newlandannexure2Service;
            _userProfileService = userProfileService;
            _hostingEnvironment = hostingEnvironment;
            _userNotificationService = userNotificationService;
            ApprovalDocumentPath = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();

        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Request data = new Request();
            var dropdownValue = await GetApprovalStatusDropdownListAtIndex();
            int[] actions = Array.ConvertAll(dropdownValue, int.Parse);
            data.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(actions.Distinct().ToArray());

            var Msg = TempData["Message"] as string;
            if (Msg != null)
                ViewBag.Message = Msg;
            return View(data);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RequestApprovalSearchDto model)
        {
            try
            {
                var result = await _requestApprovalProcessService.GetPagedProcessRequest(model, SiteContext.UserId);
                ViewBag.IsApproved = model.StatusId;
                return PartialView("_List", result);
            }
            catch (Exception Ex)
            {
                return PartialView("_List", Ex);
            }
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _requestApprovalProcessService.FetchSingleResult(id);
            ViewBag.Items = await _userProfileService.GetRole();
            await BindApprovalStatusDropdown(Data);
            ViewBag.userid = SiteContext.UserId;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Request request)
        {
            var result = false;
            var IsApplicationPendingAtUserEnd = await _requestApprovalProcessService.IsApplicationPendingAtUserEnd(id, SiteContext.UserId);
            if (IsApplicationPendingAtUserEnd)
            {
                var Data = await _requestApprovalProcessService.FetchSingleResult(id);
                FileHelper fileHelper = new FileHelper();
                var Msgddl = request.ApprovalStatus;
                #region Approval Proccess At Further level start Added by Renu 16 march 2021
                var FirstApprovalProcessData = await _approvalproccessService.FirstApprovalProcessData((_configuration.GetSection("workflowProcessGuidRequestApproval").Value), request.Id);
                var ApprovalProccessBackId = _approvalproccessService.GetPreviousApprovalId((_configuration.GetSection("workflowProcessGuidRequestApproval").Value), request.Id);
                var ApprovalProcessBackData = await _approvalproccessService.FetchApprovalProcessDocumentDetails(ApprovalProccessBackId);
                var checkLastApprovalStatuscode = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalProcessBackData.Status));

                var DataFlow = await DataAsync(ApprovalProcessBackData.Version);

                Approvalproccess approvalproccess = new Approvalproccess();

                /*Check if zonewise then aprovee user must have zoneid*/
                if (request.ApprovalStatusCode == ((int)ApprovalActionStatus.Forward) && checkLastApprovalStatuscode.StatusCode != ((int)ApprovalActionStatus.QueryForward))
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
                                                await BindApprovalStatusDropdown(request);
                                                ViewBag.Message = Alert.Show("Your Zone is not available , Without zone application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                                return View(request);
                                            }

                                            request.ApprovalZoneId = SiteContext.ZoneId;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (ApprovalProcessBackData.Level == FirstApprovalProcessData.Level && request.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))//check if revert available at first level
                {
                    result = false;
                    ViewBag.Items = await _userProfileService.GetRole();
                    await BindApprovalStatusDropdown(request);
                    ViewBag.Message = Alert.Show("Application cannot be Reverted at First Level", "", AlertType.Warning);
                    return View(request);
                }
                else
                {
                    /* Update last record pending status in Approval Process Table*/
                    result = true;
                    approvalproccess.PendingStatus = 0;
                    result = await _approvalproccessService.UpdatePreviousApprovalProccess(ApprovalProccessBackId, approvalproccess, SiteContext.UserId);

                    /*Now New row added in Approval process*/
                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                    approvalproccess.ProcessGuid = (_configuration.GetSection("workflowProcessGuidRequestApproval").Value);
                    approvalproccess.ServiceId = request.Id;
                    approvalproccess.SendFrom = SiteContext.UserId.ToString();
                    approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
                    approvalproccess.PendingStatus = 1;
                    approvalproccess.Remarks = request.ApprovalRemarks; ///May be comment
                    approvalproccess.Status = Convert.ToInt32(request.ApprovalStatus);
                    approvalproccess.Version = ApprovalProcessBackData.Version;
                    approvalproccess.DocumentName = request.ApprovalDocument == null ? null : fileHelper.SaveFile1(ApprovalDocumentPath, request.ApprovalDocument);


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

                        if (result)
                        {
                            request.ApprovedStatus = Convert.ToInt32(request.ApprovalStatus);
                            request.PendingAt = ApprovalProcessBackData.SendFrom;
                            result = await _requestService.UpdateBeforeApproval(request.Id, request);  //Update Table details 
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
                                        if (request.ApprovalStatusCode == ((int)ApprovalActionStatus.QueryForward))
                                        {
                                            approvalproccess.Level = ApprovalProcessBackData.Level;
                                            approvalproccess.SendTo = request.ApprovalUserId.ToString();
                                        }
                                        else if (request.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))
                                        {
                                            /*Check previous level for revert */
                                            for (int d = i - 1; d >= 0; d--)
                                            {
                                                if (!DataFlow[d].parameterSkip)
                                                {
                                                    var CheckLastUserForRevert = await _approvalproccessService.CheckLastUserForRevert((_configuration.GetSection("workflowProcessGuidRequestApproval").Value), request.Id, Convert.ToInt32(DataFlow[i].parameterLevel));
                                                    approvalproccess.SendTo = CheckLastUserForRevert.SendFrom;
                                                    approvalproccess.Level = Convert.ToInt32(DataFlow[d].parameterLevel);
                                                    break;
                                                }
                                            }
                                        }
                                        else if (request.ApprovalStatusCode == ((int)ApprovalActionStatus.Rejected))
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
                                                approvalproccess.SendTo = request.ApprovalUserId.ToString();

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
                                        else if (approvalproccess.SendTo == null && (request.ApprovalStatusCode != ((int)ApprovalActionStatus.Rejected) && request.ApprovalStatusCode != ((int)ApprovalActionStatus.Approved)))
                                        {
                                            ViewBag.Items = await _userProfileService.GetRole();
                                            await BindApprovalStatusDropdown(request);
                                            ViewBag.Message = Alert.Show("No user found at the next approval level, In this case, the system is unable to process your request. Please contact to the system administrator.", "", AlertType.Warning);
                                            return View(request);
                                        }
                                        #endregion

                                        result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                                        
                                        #region Insert Into usernotification table Added By Renu 18 June 2021
                                        if (result == true && approvalproccess.SendTo != null)
                                        {
                                            var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidRequestService").Value);
                                            var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                            Usernotification usernotification = new Usernotification();
                                             var replacement = notificationtemplate.Template.Replace("{proccess name}", "Identification of land").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                            usernotification.Message = replacement;
                                            usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidRequestService").Value);
                                            usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                            usernotification.ServiceId = approvalproccess.ServiceId;
                                            usernotification.SendFrom = approvalproccess.SendFrom;
                                            usernotification.SendTo = approvalproccess.SendTo;
                                            result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                                        }
                                        #endregion

                                        if (result)
                                        {
                                            if (request.ApprovalStatusCode == ((int)ApprovalActionStatus.QueryForward))
                                            {
                                                request.ApprovedStatus = Convert.ToInt32(request.ApprovalStatus);
                                                request.PendingAt = approvalproccess.SendTo;
                                            }
                                            else if (request.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))
                                            {
                                                request.ApprovedStatus = Convert.ToInt32(request.ApprovalStatus);
                                                request.PendingAt = approvalproccess.SendTo;
                                            }
                                            else if (request.ApprovalStatusCode == ((int)ApprovalActionStatus.Rejected))
                                            {
                                                request.ApprovedStatus = Convert.ToInt32(request.ApprovalStatus);
                                                request.PendingAt = "0";
                                            }
                                            else
                                            {
                                                if (i == DataFlow.Count - 1)
                                                {
                                                    request.ApprovedStatus = Convert.ToInt32(request.ApprovalStatus);
                                                    request.PendingAt = "0";
                                                }
                                                else
                                                {
                                                    request.ApprovedStatus = Convert.ToInt32(request.ApprovalStatus);
                                                    request.PendingAt = approvalproccess.SendTo;
                                                }
                                            }
                                            result = await _requestService.UpdateBeforeApproval(request.Id, request);  //Update Table details 
                                        }
                                    }
                                    break;




                                }
                            }
                        }

                    }
                    var sendMailResult = false;

                    var DataApprovalSatatusMsg = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(request.ApprovalStatus));

                    if (approvalproccess.SendTo != null)
                    {
                        #region Mail Generate
                        //At successfull completion send mail and sms
                        Uri uri = new Uri("https://www.managemybusinessess.com/");
                        string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "ApprovalMailDetailsContent.html");
                        string link = "https://master.managemybusinessess.com/ApprovalProcess/Index";

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
                        bodyDTO.ApplicationName = "Identification of land Application";
                        bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                        bodyDTO.SenderName = senderUser.User.Name;
                        bodyDTO.Link = link;
                        bodyDTO.AppRefNo = request.ReferenceNo;
                        bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                        bodyDTO.Remarks = request.ApprovalRemarks;
                        bodyDTO.path = path;
                        string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                        #endregion

                        //  sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                        #region Common Mail Genration
                        SentMailGenerationDto maildto = new SentMailGenerationDto();
                        maildto.strMailSubject = "Pending Identification of land Approval Details ";
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


                        Request data = new Request();
                        var dropdownValue = await GetApprovalStatusDropdownListAtIndex();
                        int[] actions = Array.ConvertAll(dropdownValue, int.Parse);
                        data.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(actions.Distinct().ToArray());

                        return View("Index", data);
                    }
                    else
                    {
                        ViewBag.Items = await _userProfileService.GetRole();
                        await BindApprovalStatusDropdown(request);
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(request);
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

        public async Task<PartialViewResult> RequestView(int id)
        {

            var Data = await _requestService.FetchSingleResult(id);
            return PartialView("_RequestView", Data);
        }

        public async Task<JsonResult> RequestIdHistory(int id)
        {
            int ReqId = 0;
            int CreatedBy = 0;
            int UserId = SiteContext.UserId;
            var ReqExists = await _newlandannexure2Service.CheckReqExists(id);
            var CheckReqExistancy = ReqExists.Select(a => new { a.Id });
            if ((ReqExists.Count == 0) || (ReqExists == null))
            {
                return Json(ReqExists.Select(a => new { a.Id, a.ReqId, a.CreatedBy }));
            }
            else
            {
                var Data = await _newlandannexure2Service.FetchSingleResultForReqId(id, UserId);

                if (Data.Count == 0)
                { return Json(ReqExists.Select(a => new { a.Id, ReqId, CreatedBy })); }

                else
                {
                    int UserCheck = Data[0].CreatedBy;
                    if (UserCheck == UserId)
                    {
                        return Json(Data.Select(x => new
                        {
                            x.Id,
                            ReqId,
                            x.CreatedBy

                        }));
                    }
                    else
                    {
                        return Json(Data.Select(x => new
                        {
                            x.Id,
                            x.ReqId,
                            x.CreatedBy

                        }));
                    }


                }
            }
        }

        //[HttpGet]
        //public async Task<JsonResult> getannexuredetails()  //Bind Dropdown of Approval Status
        //{
        //    var result = false;
        //    var DataFlow = await DataAsync();

        //    for (int i = 0; i < DataFlow.Count; i++)
        //    {
        //        if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
        //        {
        //            result = true;
        //            if (result)
        //            {

        //                if (i == DataFlow.Count - 1)
        //                {


        //                    return Json(ViewBag.data = true);
        //                }
        //                else
        //                {
        //                    ViewBag.data = 0;
        //                }
        //                break;
        //            }
        //        }
        //    }

        //    return Json(DataFlow);
        //}

        public async Task<FileResult> ViewLetter(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _requestService.FetchSingleResult(Id);
                string targetPhotoPathLayout = ApprovalDocumentPath+Data.LayoutPlan;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _requestService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.LayoutPlan;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }
        public async Task<IActionResult> RequestApprovalProcessList([FromBody] RequestApprovalSearchDto model)
        {
            var result = await _requestApprovalProcessService.GetAllProcessRequestList(model,SiteContext.UserId);
            List<RequestApprovalProcessListDto> data = new List<RequestApprovalProcessListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new RequestApprovalProcessListDto()
                    {
                        Id = result[i].Id,
                        ProposalName = result[i].PproposalName,
                        Fileno = result[i].PfileNo,
                        RequiringBody = result[i].RequiringBody,
                        Area = result[i].AreaLocality,
                        ApprovedStatus = result[i].ApprovedStatusNavigation == null ? "" : result[i].ApprovedStatusNavigation.SentStatusName,
                        IsActive = result[i].IsActive == 0 ? "InActive" : "Active"
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }
        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        #region History Details Only For Approval Page Added by Renu 16 march 2021
        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _approvalproccessService.GetHistoryDetails((_configuration.GetSection("workflowProcessGuidRequestApproval").Value), id);

            return PartialView("_HistoryDetails", Data);
        }
        #endregion

        #region Fetch workflow data for approval prrocess Added by Renu 16 march 2021
        private async Task<List<TemplateStructure>> DataAsync(string version)
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuidWithVersion((_configuration.GetSection("workflowProcessGuidRequestApproval").Value), version);
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }

        [HttpGet]
        public async Task<JsonResult> GetApprovalDropdownList()  //Bind Dropdown of Approval Status but not in use now after new approval changes
        {
            var DataFlow = await DataAsync("242");

            for (int i = 0; i < DataFlow.Count; i++)
            {
                if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
                {
                    var dropdown = DataFlow[i].parameterAction;
                    return Json(dropdown);
                    break;
                }

            }
            return Json(DataFlow);
        }
        public async Task<IActionResult> ViewDocumentApprovalProccess(int Id)
        {
            FileHelper file = new FileHelper();
            Approvalproccess Data = await _approvalproccessService.FetchApprovalProcessDocumentDetails(Id);
            string filename = ApprovalDocumentPath + Data.DocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
            //string filename = Data.DocumentFileName;
            //return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        #endregion

        #region Approval Status Dropdown Bind on User rights Basis Code Added By Renu 16 march 2021
        async Task BindApprovalStatusDropdown(Request Data)
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
            var ApprovalProccessBackId = _approvalproccessService.GetPreviousApprovalId((_configuration.GetSection("workflowProcessGuidRequestApproval").Value), serviceid);
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
            var DataFlow = await _workflowtemplateService.GetWorkFlowDataOnGuid((_configuration.GetSection("workflowProcessGuidRequestApproval").Value));

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
            var ApprovalProccessBackId = _approvalproccessService.GetPreviousApprovalId((_configuration.GetSection("workflowProcessGuidRequestApproval").Value), serviceid);
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
            var Data = await _requestApprovalProcessService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        #endregion

    }
}
