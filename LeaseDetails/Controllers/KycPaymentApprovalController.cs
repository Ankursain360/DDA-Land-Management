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
using System.Net.Http;
using System.Text.Json;

namespace LeaseDetails.Controllers
{
    public class KycPaymentApprovalController : BaseController
    {
        private readonly IKycPaymentApprovalService _kycPaymentApprovalService;
        public IConfiguration _configuration;
        private readonly IUserProfileService _userProfileService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IKycformApprovalService _kycformApprovalService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IKycformService _kycformService;
        private readonly IDemandDetailsService _demandDetailsService;

        string ApprovalDocumentPath = "";
        string AadharDoc = "";
        string LetterDoc = "";
        string ApplicantDoc = "";
        public KycPaymentApprovalController(IConfiguration configuration,
            IKycPaymentApprovalService kycPaymentApprovalService,
             IUserNotificationService userNotificationService,
               IKycformService KycformService,
            IUserProfileService userProfileService,
             IApprovalProccessService approvalproccessService,
             IKycformApprovalService kycformApprovalService,
             IHostingEnvironment hostingEnvironment,
             IDemandDetailsService demandDetailsService)

        {
            _configuration = configuration;
            _kycPaymentApprovalService = kycPaymentApprovalService;
            _kycformService = KycformService;
            _userProfileService = userProfileService;
            _approvalproccessService = approvalproccessService;
            _kycformApprovalService = kycformApprovalService;
            ApprovalDocumentPath = _configuration.GetSection("FilePaths:KYCApplicationForm:ApprovalDocumentPath").Value.ToString();
            _userNotificationService = userNotificationService;
            _hostingEnvironment = hostingEnvironment;
            _demandDetailsService = demandDetailsService;

            AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
            LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
            ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();
        }
        public async Task<IActionResult> Index()
        {
            Kycdemandpaymentdetails payment = new Kycdemandpaymentdetails();
            var dropdownValue = await GetApprovalStatusDropdownListAtIndex();
            int[] actions = Array.ConvertAll(dropdownValue, int.Parse);
            payment.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(actions.Distinct().ToArray());

            return View(payment);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] KycPaymentApprovalSearchDto model)
        {
            var result = await _kycPaymentApprovalService.GetPagedKycPaymentDetails(model, SiteContext.UserId);
            ViewBag.IsApproved = model.StatusId;
            return PartialView("_List", result);
        }
        public async Task<JsonResult> GetChallanDetails(int? Id)
        {
            Id = Id ?? 0;
            var data = await _kycPaymentApprovalService.GetAllChallan(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.KycId,
                x.DemandPaymentId,
                x.PaymentType,
                x.Period,
                x.ChallanNo,
                x.Amount,
                Date = Convert.ToDateTime(x.DateofPaymentByAllottee).ToString("yyyy-MM-dd"),
               // x.DateofPaymentByAllottee,
                x.Proofinpdf,
                x.Ddabankcredit
                
            }));
        }
        public async Task<PartialViewResult> PaymentDetails(int Id)
        {

            var result = await _demandDetailsService.GetPaymentDetails(Id);

            return PartialView("_PaymentDetails", result);
        }
        public async Task<PartialViewResult> PaymentFromBhoomi(string FileNo)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration.GetSection("BhoomiApi").Value + FileNo))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var data1 = JsonSerializer.Deserialize<ApiResponseBhoomiApiFileWise>(apiResponse);
                        return PartialView("_PaymentFromBhoomi", data1);

                    }
                    else
                    {
                        ApiResponseBhoomiApiFileWise data1 = new ApiResponseBhoomiApiFileWise();
                        List<BhoomiApiFileNowiseDto> cargo = new List<BhoomiApiFileNowiseDto>();
                        data1.cargo = cargo;
                        return PartialView("_PaymentFromBhoomi", data1);
                    }

                }
            }

        }

        public  PartialViewResult ApplicantChallanDetails(int Id)
        {

            // var result = await _demandDetailsService.GetPaymentDetails(Id);

            // return PartialView("_PaymentDetails", result);
            return PartialView("_AllotteeChallanDetails");
        }
        

        // [AuthorizeContext(ViewAction.Add)]

        public async Task<IActionResult> Create(int id)
        {
            var Data = await _kycPaymentApprovalService.FetchSingleResult(id);
            var Data2 = await _kycformService.FetchSingleResult(Data.KycId);
            Data.FileNo = Data2.FileNo;
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
        public async Task<IActionResult> Create(int id, Kycdemandpaymentdetails payment)
        {
            var result = false;
            var IsApplicationPendingAtUserEnd = await _kycPaymentApprovalService.IsApplicationPendingAtUserEnd(id, SiteContext.UserId);
            if (IsApplicationPendingAtUserEnd)
            {
                var Data = await _kycPaymentApprovalService.FetchSingleResult(id);
                FileHelper fileHelper = new FileHelper();
                var Msgddl = payment.ApprovalStatus;


                #region Approval Proccess At Further level start Added by ishu 30 july 2021

                var FirstApprovalProcessData = await _approvalproccessService.FirstkycApprovalProcessData((_configuration.GetSection("workflowProccessGuidKYCPayment").Value), payment.Id);
                var ApprovalProccessBackId = _approvalproccessService.GetPreviouskycApprovalId((_configuration.GetSection("workflowProccessGuidKYCPayment").Value), payment.Id);
                var ApprovalProcessBackData = await _approvalproccessService.FetchKYCApprovalProcessDocumentDetails(ApprovalProccessBackId);
                var checkLastApprovalStatuscode = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalProcessBackData.Status));


                var DataFlow = await DataAsync(ApprovalProcessBackData.Version);

                Kycapprovalproccess approvalproccess = new Kycapprovalproccess();

                /*Check if zonewise then aprovee user must have zoneid*/

                if (payment.ApprovalStatusCode == ((int)ApprovalActionStatus.Forward) && checkLastApprovalStatuscode.StatusCode != ((int)ApprovalActionStatus.QueryForward))
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
                                                await BindApprovalStatusDropdown(payment);
                                                ViewBag.Message = Alert.Show("Your Zone is not available , Without zone application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                                return View(payment);
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

                if (ApprovalProcessBackData.Level == FirstApprovalProcessData.Level && payment.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))//check if revert available at first level
                {
                    result = false;
                    ViewBag.Items = await _userProfileService.GetRole();
                    await BindApprovalStatusDropdown(payment);
                    ViewBag.Message = Alert.Show("Application cannot be Reverted at First Level", "", AlertType.Warning);
                    return View(payment);
                }
                else
                {
                    /* Update last record pending status in kycApprovalProcess Table*/
                    result = true;
                    approvalproccess.PendingStatus = 0;
                  
                    result = await _approvalproccessService.UpdatePreviouskycApprovalProccess(ApprovalProccessBackId, approvalproccess, SiteContext.UserId);

                    /*Now New row added in kycApprovalprocess table*/

                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                    approvalproccess.ProcessGuid = (_configuration.GetSection("workflowProccessGuidKYCPayment").Value);
                    approvalproccess.ServiceId = payment.Id;
                    approvalproccess.SendFrom = SiteContext.UserId.ToString();
                    approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
                    approvalproccess.PendingStatus = 1;
                    approvalproccess.Remarks = payment.ApprovalRemarks; ///May be comment
                    approvalproccess.Status = Convert.ToInt32(payment.ApprovalStatus);
                    approvalproccess.Version = ApprovalProcessBackData.Version;
                    approvalproccess.DocumentName = payment.ApprovalDocument == null ? null : fileHelper.SaveFile1(ApprovalDocumentPath, payment.ApprovalDocument);


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
                            payment.ApprovedStatus = Convert.ToInt32(payment.ApprovalStatus);
                            payment.PendingAt = ApprovalProcessBackData.SendFrom;
                            result = await _kycPaymentApprovalService.UpdateBeforeApproval(payment.Id, payment);  //Update Kycdemandpaymentdetails  Table details 
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
                                        if (payment.ApprovalStatusCode == ((int)ApprovalActionStatus.QueryForward))
                                        {
                                            approvalproccess.Level = ApprovalProcessBackData.Level;
                                            approvalproccess.SendTo = payment.ApprovalUserId.ToString();
                                        }
                                        else if (payment.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))
                                        {
                                            /*Check previous level for revert */
                                            for (int d = i - 1; d >= 0; d--)
                                            {
                                                if (!DataFlow[d].parameterSkip)
                                                {
                                                    var CheckLastUserForRevert = await _approvalproccessService.CheckLastKycUserForRevert((_configuration.GetSection("workflowProccessGuidKYCPayment").Value), payment.Id, Convert.ToInt32(DataFlow[i].parameterLevel));
                                                    approvalproccess.SendTo = CheckLastUserForRevert.SendFrom;
                                                    approvalproccess.Level = Convert.ToInt32(DataFlow[d].parameterLevel);
                                                    break;
                                                }
                                            }
                                        }
                                        else if (payment.ApprovalStatusCode == ((int)ApprovalActionStatus.Rejected))
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
                                                approvalproccess.SendTo = payment.ApprovalUserId.ToString();

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
                                        else if (approvalproccess.SendTo == null && (payment.ApprovalStatusCode != ((int)ApprovalActionStatus.Rejected) && payment.ApprovalStatusCode != ((int)ApprovalActionStatus.Approved)))
                                        {
                                            ViewBag.Items = await _userProfileService.GetRole();
                                            await BindApprovalStatusDropdown(payment);
                                            ViewBag.Message = Alert.Show("No user found at the next approval level, In this case, the system is unable to process your request. Please contact to the system administrator.", "", AlertType.Warning);
                                            return View(payment);
                                        }
                                        #endregion

                                         result = await _kycformService.CreatekycApproval(approvalproccess, SiteContext.UserId); //Create a row in kycapprovalproccess Table

                                        #region Insert Into usernotification table Added By Renu 18 June 2021
                                        if (result)
                                        {
                                            var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidKycPayment").Value);
                                            var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                            Usernotification usernotification = new Usernotification();
                                            var replacement = notificationtemplate.Template.Replace("{proccess name}", "Kyc Payment").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                            usernotification.Message = replacement;
                                            usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidKycPayment").Value);
                                            usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                            usernotification.ServiceId = approvalproccess.ServiceId;
                                            usernotification.SendFrom = approvalproccess.SendFrom;
                                            usernotification.SendTo = approvalproccess.SendTo;
                                            result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                                        }
                                        #endregion


                                        if (result)
                                        {
                                            if (payment.ApprovalStatusCode == ((int)ApprovalActionStatus.QueryForward))
                                            {
                                                payment.ApprovedStatus = Convert.ToInt32(payment.ApprovalStatus);
                                                payment.PendingAt = approvalproccess.SendTo;
                                            }
                                            else if (payment.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))
                                            {
                                                payment.ApprovedStatus = Convert.ToInt32(payment.ApprovalStatus);
                                                payment.PendingAt = approvalproccess.SendTo;
                                            }
                                            else if (payment.ApprovalStatusCode == ((int)ApprovalActionStatus.Rejected))
                                            {
                                                payment.ApprovedStatus = Convert.ToInt32(payment.ApprovalStatus);
                                                payment.PendingAt = "0";
                                            }
                                            else
                                            {
                                                if (i == DataFlow.Count - 1)
                                                {
                                                    payment.ApprovedStatus = Convert.ToInt32(payment.ApprovalStatus);
                                                    payment.PendingAt = "0";
                                                }
                                                else
                                                {
                                                    payment.ApprovedStatus = Convert.ToInt32(payment.ApprovalStatus);
                                                    payment.PendingAt = approvalproccess.SendTo;
                                                }
                                            }
                                            result = await _kycPaymentApprovalService.UpdateBeforeApproval(payment.Id, payment);  //Update Table details 
                                        }
                                    }
                                    break;


                                }
                            }
                        }

                    }
                    var sendMailResult = false;

                    var DataApprovalSatatusMsg = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(payment.ApprovalStatus));

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

                        #region Mail Generation Added By Ishu

                        MailSMSHelper mailG = new MailSMSHelper();

                        #region HTML Body Generation
                        ApprovalMailBodyDto bodyDTO = new ApprovalMailBodyDto();
                        bodyDTO.ApplicationName = "KYC Payment Application";
                        bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                        bodyDTO.SenderName = senderUser.User.Name;
                        bodyDTO.Link = linkhref;
                        bodyDTO.AppRefNo = payment.Id.ToString();
                        bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                        bodyDTO.Remarks = payment.ApprovalRemarks;
                        bodyDTO.path = path;
                        string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                        #endregion

                        //string strMailSubject = "Pending Lease Application Approval Request Details ";
                        //string strMailCC = "", strMailBCC = "", strAttachPath = "";
                        //sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                        #region Common Mail Genration
                        SentMailGenerationDto maildto = new SentMailGenerationDto();
                        maildto.strMailSubject = "Pending KYC Payment Application Approval Request Details ";
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

                        Kycdemandpaymentdetails data = new Kycdemandpaymentdetails();
                        var dropdownValue = await GetApprovalStatusDropdownListAtIndex();
                        int[] actions = Array.ConvertAll(dropdownValue, int.Parse);
                        data.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(actions.Distinct().ToArray());

                        return View("Index", data);
                    }
                    else
                    {
                        ViewBag.Items = await _userProfileService.GetRole();
                        await BindApprovalStatusDropdown(payment);
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(payment);
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



        #region Approval Status Dropdown Bind on User rights Basis Code Added By ishu 29 july 2021
        async Task BindApprovalStatusDropdown(Kycdemandpaymentdetails Data)
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
        public async Task<List<string>> GetApprovalStatusDropdownList(int serviceid)  //Bind Dropdown of Approval Status 27 july 2021 ishu
        {
            List<string> dropdown = null;
            var ApprovalProccessBackId = _approvalproccessService.GetPreviouskycApprovalId((_configuration.GetSection("workflowProccessGuidKYCPayment").Value), serviceid);
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
            var DataFlow = await _kycPaymentApprovalService.GetWorkFlowDataOnGuid((_configuration.GetSection("workflowProccessGuidKYCPayment").Value));

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
            var Data = await _kycformApprovalService.FetchSingleResultOnProcessGuidWithVersion((_configuration.GetSection("workflowProccessGuidKYCPayment").Value), version);
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

        #region History Details Only For Approval Page Added by ishu 16 march 2021
        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _approvalproccessService.GetKYCHistoryDetails((_configuration.GetSection("workflowProccessGuidKYCPayment").Value), id);

            return PartialView("_HistoryDetails", Data);
        }
        #endregion


        #region KYCApplicationForm Details 
        //show kyc form data in accordian
        public async Task<PartialViewResult> KYCFormView(int id)
        {
            var Data = await _kycformService.FetchKYCSingleResult(id);
            Data.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            Data.BranchList = await _kycformService.GetAllBranchList();
            Data.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            Data.ZoneList = await _kycformService.GetAllZoneList();
           Data.LocalityList = await _kycformService.GetLocalityList(Data.ZoneId);
            // Data.Leasedocuments = await _leaseApplicationFormService.LeaseApplicationDocumentDetails(id);
            return PartialView("_KYCFormView", Data);
        }

        //***************** Download kyc form accordian Files  ********************

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
        #endregion
    }
}
