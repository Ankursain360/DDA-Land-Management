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
using LeaseDetails.Filters;
using Core.Enum;
using Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using Dto.Master;
using System.Text;
namespace LeaseDetails.Controllers
{
    public class KycFormApprovalController : BaseController
    {
        private readonly IKycformService _kycformService;
        public IConfiguration _configuration;
        private readonly IUserProfileService _userProfileService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IKycformApprovalService _kycformApprovalService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IHostingEnvironment _hostingEnvironment;

        string ApprovalDocumentPath = "";
        public KycFormApprovalController(IConfiguration configuration,
            IKycformService KycformService,
             IUserNotificationService userNotificationService,
            IUserProfileService userProfileService,
             IApprovalProccessService approvalproccessService,
             IKycformApprovalService kycformApprovalService,
             IHostingEnvironment hostingEnvironment)

        {
            _configuration = configuration;
            _kycformService = KycformService;
            _userProfileService = userProfileService;
            _approvalproccessService = approvalproccessService;
            _kycformApprovalService = kycformApprovalService;
            ApprovalDocumentPath = _configuration.GetSection("FilePaths:KYCApplicationForm:ApprovalDocumentPath").Value.ToString();
            _userNotificationService = userNotificationService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
             Kycform kyc = new Kycform();
            var dropdownValue = await GetApprovalStatusDropdownListAtIndex();
            int[] actions = Array.ConvertAll(dropdownValue, int.Parse);
            kyc.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(actions.Distinct().ToArray());

            return View(kyc);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] KycFormApprovalSearchDto model)
        {
            var result = await _kycformApprovalService.GetPagedKycFormDetails(model, SiteContext.UserId);
            ViewBag.IsApproved = model.StatusId;
            return PartialView("_List", result);
        }

       // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _kycformApprovalService.FetchSingleResult(id);
            ViewBag.Items = await _userProfileService.GetRole();
            await BindApprovalStatusDropdown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
       // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Kycform kyc)
        {
            var result = false;
            var IsApplicationPendingAtUserEnd = await _kycformApprovalService.IsApplicationPendingAtUserEnd(id, SiteContext.UserId);
            if (IsApplicationPendingAtUserEnd)
            {
                var Data = await _kycformApprovalService.FetchSingleResult(id);
                FileHelper fileHelper = new FileHelper();
                var Msgddl = kyc.ApprovalStatus;

                #region Approval Proccess At Further level start Added by ishu 22 july 2021


                var FirstApprovalProcessData = await _approvalproccessService.FirstkycApprovalProcessData((_configuration.GetSection("workflowProccessGuidKYCForm").Value), kyc.Id);
                var ApprovalProccessBackId = _approvalproccessService.GetPreviouskycApprovalId((_configuration.GetSection("workflowProccessGuidKYCForm").Value), kyc.Id);
                var ApprovalProcessBackData = await _approvalproccessService.FetchKYCApprovalProcessDocumentDetails(ApprovalProccessBackId);
                var checkLastApprovalStatuscode = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalProcessBackData.Status));

                var DataFlow = await DataAsync(ApprovalProcessBackData.Version);

                Kycapprovalproccess approvalproccess = new Kycapprovalproccess();

                /*Check if zonewise then aprovee user must have zoneid*/
                if (kyc.ApprovalStatusCode == ((int)ApprovalActionStatus.Forward) && checkLastApprovalStatuscode.StatusCode != ((int)ApprovalActionStatus.QueryForward))
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
                                        if (DataFlow[d].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                                        {
                                            if (SiteContext.BranchId == null)
                                            {
                                                ViewBag.Items = await _userProfileService.GetRole();
                                                await BindApprovalStatusDropdown(kyc);
                                                ViewBag.Message = Alert.Show("Your branch is not available , Without branch application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                                return View(kyc);
                                            }

                                           // leaseapplication.ApprovalZoneId = SiteContext.ZoneId;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (ApprovalProcessBackData.Level == FirstApprovalProcessData.Level && kyc.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))//check if revert available at first level
                {
                    result = false;
                    ViewBag.Items = await _userProfileService.GetRole();
                    await BindApprovalStatusDropdown(kyc);
                    ViewBag.Message = Alert.Show("Application cannot be Reverted at First Level", "", AlertType.Warning);
                    return View(kyc);
                }
                else
                {
                    /* Update last record pending status in Approval Process Table*/
                    result = true;
                    approvalproccess.PendingStatus = 0;
                    result = await _approvalproccessService.UpdatePreviouskycApprovalProccess(ApprovalProccessBackId, approvalproccess, SiteContext.UserId);

                    /*Now New row added in Approval process*/
                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                    approvalproccess.ProcessGuid = (_configuration.GetSection("workflowProccessGuidKYCForm").Value);
                    approvalproccess.ServiceId = kyc.Id;
                    approvalproccess.SendFrom = SiteContext.UserId.ToString();
                    approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
                    approvalproccess.PendingStatus = 1;
                    approvalproccess.Remarks = kyc.ApprovalRemarks; ///May be comment
                    approvalproccess.Status = Convert.ToInt32(kyc.ApprovalStatus);
                    approvalproccess.Version = ApprovalProcessBackData.Version;
                    approvalproccess.DocumentName = kyc.ApprovalDocument == null ? null : fileHelper.SaveFile1(ApprovalDocumentPath, kyc.ApprovalDocument);


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

                       result = await _kycformService.CreatekycApproval(approvalproccess, SiteContext.UserId); //Create a row in kycapprovalproccess Table

                        if (result)
                        {
                            kyc.ApprovedStatus = Convert.ToInt32(kyc.ApprovalStatus);
                            kyc.PendingAt = ApprovalProcessBackData.SendFrom;
                            result = await _kycformService.UpdateBeforeApproval(kyc.Id, kyc);  //Update kyc  Table details 
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
                                        if (kyc.ApprovalStatusCode == ((int)ApprovalActionStatus.QueryForward))
                                        {
                                            approvalproccess.Level = ApprovalProcessBackData.Level;
                                            approvalproccess.SendTo = kyc.ApprovalUserId.ToString();
                                        }
                                        else if (kyc.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))
                                        {
                                            /*Check previous level for revert */
                                            for (int d = i - 1; d >= 0; d--)
                                            {
                                                if (!DataFlow[d].parameterSkip)
                                                {
                                                    var CheckLastUserForRevert = await _approvalproccessService.CheckLastKycUserForRevert((_configuration.GetSection("workflowProccessGuidKYCForm").Value), kyc.Id, Convert.ToInt32(DataFlow[i].parameterLevel));
                                                    approvalproccess.SendTo = CheckLastUserForRevert.SendFrom;
                                                    approvalproccess.Level = Convert.ToInt32(DataFlow[d].parameterLevel);
                                                    break;
                                                }
                                            }
                                        }
                                        else if (kyc.ApprovalStatusCode == ((int)ApprovalActionStatus.Rejected))
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
                                                approvalproccess.SendTo = kyc.ApprovalUserId.ToString();

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
                                        else if (approvalproccess.SendTo == null && (kyc.ApprovalStatusCode != ((int)ApprovalActionStatus.Rejected) && kyc.ApprovalStatusCode != ((int)ApprovalActionStatus.Approved)))
                                        {
                                            ViewBag.Items = await _userProfileService.GetRole();
                                            await BindApprovalStatusDropdown(kyc);
                                            ViewBag.Message = Alert.Show("No user found at the next approval level, In this case, the system is unable to process your request. Please contact to the system administrator.", "", AlertType.Warning);
                                            return View(kyc);
                                        }
                                        #endregion

                                        result = await _kycformService.CreatekycApproval(approvalproccess, SiteContext.UserId); //Create a row in kycapprovalproccess Table

                                        #region Insert Into usernotification table Added By Renu 18 June 2021
                                        if (result)
                                        {
                                            var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidKYCForm").Value);
                                            var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                            Usernotification usernotification = new Usernotification();
                                            var replacement = notificationtemplate.Template.Replace("{proccess name}", "KYC Form").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                            usernotification.Message = replacement;
                                            usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidKYCForm").Value);
                                            usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                            usernotification.ServiceId = approvalproccess.ServiceId;
                                            usernotification.SendFrom = approvalproccess.SendFrom;
                                            usernotification.SendTo = approvalproccess.SendTo;
                                            result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                                        }
                                        #endregion


                                        if (result)
                                        {
                                            if (kyc.ApprovalStatusCode == ((int)ApprovalActionStatus.QueryForward))
                                            {
                                                kyc.ApprovedStatus = Convert.ToInt32(kyc.ApprovalStatus);
                                                kyc.PendingAt = approvalproccess.SendTo;
                                               
                                            }
                                            else if (kyc.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))
                                            {
                                                kyc.ApprovedStatus = Convert.ToInt32(kyc.ApprovalStatus);
                                                kyc.PendingAt = approvalproccess.SendTo;
                                            }
                                            else if (kyc.ApprovalStatusCode == ((int)ApprovalActionStatus.Rejected))
                                            {
                                                kyc.ApprovedStatus = Convert.ToInt32(kyc.ApprovalStatus);
                                                kyc.PendingAt = "0";
                                            }
                                            else
                                            {
                                                if (i == DataFlow.Count - 1)
                                                {
                                                    kyc.ApprovedStatus = Convert.ToInt32(kyc.ApprovalStatus);
                                                    kyc.PendingAt = "0";
                                                    kyc.KycStatus = "T";
                                                }
                                                else
                                                {
                                                    kyc.ApprovedStatus = Convert.ToInt32(kyc.ApprovalStatus);
                                                    kyc.PendingAt = approvalproccess.SendTo;
                                                }
                                            }
                                            result = await _kycformService.UpdateBeforeApproval(kyc.Id, kyc);  //Update kycform Table details 
                                        }
                                    }
                                    break;


                                }
                            }
                        }

                    }
                    var sendMailResult = false;

                    var DataApprovalSatatusMsg = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(kyc.ApprovalStatus));

                    if (approvalproccess.SendTo != null)
                    {
                        #region Mail Generate
                        //At successfull completion send mail and sms
                        Uri uri = new Uri("https://master.managemybusinessess.com/ApprovalProcess/Index");//this is correct
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
                        bodyDTO.ApplicationName = "KYC Form Application";
                        bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                        bodyDTO.SenderName = senderUser.User.Name;
                        bodyDTO.Link = linkhref;
                        bodyDTO.AppRefNo = kyc.Id.ToString();
                        bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                        bodyDTO.Remarks = kyc.ApprovalRemarks;
                        bodyDTO.path = path;
                        string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                        #endregion

                        //string strMailSubject = "Pending Lease Application Approval Request Details ";
                        //string strMailCC = "", strMailBCC = "", strAttachPath = "";
                        //sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                        #region Common Mail Genration
                        SentMailGenerationDto maildto = new SentMailGenerationDto();
                        maildto.strMailSubject = "Pending KYC Form Application Approval Request Details ";
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

                        Kycform data = new Kycform();
                        var dropdownValue = await GetApprovalStatusDropdownListAtIndex();
                        int[] actions = Array.ConvertAll(dropdownValue, int.Parse);
                        data.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(actions.Distinct().ToArray());

                        return View("Index", data);
                    }
                    else
                    {
                        ViewBag.Items = await _userProfileService.GetRole();
                        await BindApprovalStatusDropdown(kyc);
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(kyc);
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



        #region Approval Status Dropdown Bind on User rights Basis Code Added By ishu 22 july 2021
        async Task BindApprovalStatusDropdown(Kycform Data)
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
        public async Task<List<string>> GetApprovalStatusDropdownList(int serviceid)  //Bind Dropdown of Approval Status 22 july 2021 ishu
        {
            List<string> dropdown = null;
            var ApprovalProccessBackId = _approvalproccessService.GetPreviouskycApprovalId((_configuration.GetSection("workflowProccessGuidKYCForm").Value), serviceid);
            var ApprovalProcessBackData = await _approvalproccessService.FetchKYCApprovalProcessDocumentDetails(ApprovalProccessBackId);
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
            var DataFlow = await _kycformService.GetWorkFlowDataOnGuid((_configuration.GetSection("workflowProccessGuidKYCForm").Value));

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


        #region Fetch workflow data for approval prrocess Added by ishu 22 july 2021
        private async Task<List<TemplateStructure>> DataAsync(string version)
        {
            var Data = await _kycformApprovalService.FetchSingleResultOnProcessGuidWithVersion((_configuration.GetSection("workflowProccessGuidKYCForm").Value), version);
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
            Kycapprovalproccess Data = await _approvalproccessService.FetchKYCApprovalProcessDocumentDetails(Id);
            string filename = ApprovalDocumentPath + Data.DocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
            //string filename = Data.DocumentFileName;
            //return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
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
            var data = await _userProfileService.GetkycUserSkippingItsOwnConcatedName(RoleId, SiteContext.UserId);
            return Json(data);
        }

        [HttpGet]
        public async Task<JsonResult> GetForwardedUserList(string value)
        {
            int serviceid = Convert.ToInt32(value);
            List<string> dropdown = null;
            var ApprovalProccessBackId = _approvalproccessService.GetPreviouskycApprovalId((_configuration.GetSection("workflowProccessGuidKYCForm").Value), serviceid);
            var ApprovalProcessBackData = await _approvalproccessService.FetchKYCApprovalProcessDocumentDetails(ApprovalProccessBackId);
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
                                    if (DataFlow[d].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                                    {
                                        if (SiteContext.BranchId == null)
                                        {
                                            JsonMsg.Add("false");
                                            JsonMsg.Add("Branch Id not available for next level, untill then Submittion is not possible");
                                            return Json(JsonMsg);
                                        }

                                    }
                                    if (DataFlow[d].parameterValue == (_configuration.GetSection("ApprovalRoleType").Value))
                                    {
                                        for (int b = 0; b < DataFlow[d].parameterName.Count;b++)
                                        {
                                            List<kycUserProfileInfoDetailsDto> UserListRoleBasis = null;
                                            if (DataFlow[d].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                                                UserListRoleBasis = await _userProfileService.kycGetUserOnRoleZoneBasisConcatedName(Convert.ToInt32(DataFlow[d].parameterName[b]), SiteContext.BranchId ?? 0);
                                            else
                                                UserListRoleBasis = await _userProfileService.GetkycUserOnRoleBasisConcatedName(Convert.ToInt32(DataFlow[d].parameterName[b]));

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
                                        if (DataFlow[d].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                                        {
                                            StringBuilder multouserszonewise = new StringBuilder();
                                            int col = 0;
                                            if (SendTo != null)
                                            {
                                                string[] multiTo = SendTo.Split(',');
                                                foreach (string MultiUserId in multiTo)
                                                {
                                                    var UserProfile = await _userProfileService.GetUserByIdBranchConcatedName(Convert.ToInt32(MultiUserId), SiteContext.BranchId ?? 0);
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
                var data = await _userProfileService.kycUserListSkippingmultiusersConcatedName(nums);
                return Json(data);
            }
            return Json(dropdown);
        }

        //[AuthorizeContext(ViewAction.View)]
        //public async Task<IActionResult> View(int id)
        //{
        //    var Data = await _leaseApplicationFormApprovalService.FetchSingleResult(id);
        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}
        #endregion
    }
}
