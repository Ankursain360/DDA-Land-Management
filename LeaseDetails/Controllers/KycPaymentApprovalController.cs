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
using System.Net;
using System.Xml.Serialization;
using System.Globalization;

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
        private readonly IKycdemandpaymentdetailstableaService _kycdemandpaymentdetailstableaService;
        private readonly IKycdemandpaymentdetailstablecService _kycdemandpaymentdetailstablecService;
        string ApprovalDocumentPath = "";
        string AadharDoc = "";
        string LetterDoc = "";
        string ApplicantDoc = "";
        string ChallanProof = "";
        string OustandingDeusDoc = "";
        public KycPaymentApprovalController(IConfiguration configuration,
            IKycPaymentApprovalService kycPaymentApprovalService,
             IUserNotificationService userNotificationService,
               IKycformService KycformService,
            IUserProfileService userProfileService,
             IApprovalProccessService approvalproccessService,
             IKycformApprovalService kycformApprovalService,
             IHostingEnvironment hostingEnvironment,
              IKycdemandpaymentdetailstableaService kycdemandpaymentdetailstableaService,
              IKycdemandpaymentdetailstablecService kycdemandpaymentdetailstablecService,
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
            _kycdemandpaymentdetailstableaService = kycdemandpaymentdetailstableaService;
            _kycdemandpaymentdetailstablecService = kycdemandpaymentdetailstablecService;
            AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
            LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
            ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();
            ChallanProof = _configuration.GetSection("FilePaths:KycFiles:ChallanProofDocument").Value.ToString();
            OustandingDeusDoc = _configuration.GetSection("FilePaths:KycFiles:OustandingDuesDocUploadedByAAOAccounts").Value.ToString();
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
        public async Task<PartialViewResult> GetChallanDetails(int Id)
        {
            var result = await _kycPaymentApprovalService.GetAllChallan(Id);
            ViewBag.Role = SiteContext.RoleId;
            return PartialView("_AllotteeChallanDetails", result);
        }
        public async Task<PartialViewResult> PaymentDetails(int Id)
        {
            var result = await _kycdemandpaymentdetailstableaService.FetchResultOnDemandId(Id);

            return PartialView("_PaymentDetails", result);
        }
        public async Task<PartialViewResult> PaymentFromBhoomi(string FileNo)
        {
            try
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
            catch (Exception ex)
            {
                ApiResponseBhoomiApiFileWise data1 = new ApiResponseBhoomiApiFileWise();
                List<BhoomiApiFileNowiseDto> cargo = new List<BhoomiApiFileNowiseDto>();
                data1.cargo = cargo;
                return PartialView("_PaymentFromBhoomi", data1);
            }

        }

        public PartialViewResult ApplicantChallanDetails(int Id)
        {
            ViewBag.Role = SiteContext.RoleId;
            // var result = await _demandDetailsService.GetPaymentDetails(Id);

            // return PartialView("_PaymentDetails", result);
            return PartialView("_AllotteeChallanDetails");
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _kycPaymentApprovalService.FetchSingleResult(id);
            var Data2 = await _kycformService.FetchSingleResult(Data.KycId);
            Data.FileNo = Data2.FileNo;
            ViewBag.Items = await _userProfileService.GetRole();
            ViewBag.Role = SiteContext.RoleId;
            //await BindApprovalStatusDropdown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        // [AuthorizeContext(ViewAction.Add)]

        public async Task<IActionResult> Create(int id)
        {
            
            var Data = await _kycPaymentApprovalService.FetchSingleResult(id);
            var Data2 = await _kycformService.FetchSingleResult(Data.KycId);
            Data.FileNo = Data2.FileNo;
            ViewBag.Items = await _userProfileService.GetRole();
            ViewBag.Role = SiteContext.RoleId;
            var BackIdGuid1 = _configuration.GetSection("workflowProccessGuidKYCPayment").Value;
            var BackIdGuid2 = _configuration.GetSection("workflowProccessGuidKYCPayment2").Value;
            var ApprovalProccessBackId = (Data.WorkFlowTemplate == "WF1") ? _approvalproccessService.GetPreviouskycApprovalId(BackIdGuid1, id) : _approvalproccessService.GetPreviouskycApprovalId(BackIdGuid2, id);
            var ApprovalProcessBackData = await _approvalproccessService.FetchKYCApprovalProcessDocumentDetails(ApprovalProccessBackId);

            ViewBag.Level = ApprovalProcessBackData.Level;

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
                var Data2 = await _kycformService.FetchSingleResult(Data.KycId);
                var Data3 = await _kycdemandpaymentdetailstableaService.FetchSingleResultonDemandId(id);
                var DDInfo = await _kycPaymentApprovalService.FetchDDofBranch(Data2.BranchId);
                FileHelper fileHelper = new FileHelper();
                var Msgddl = payment.ApprovalStatus;


                #region Approval Proccess At Further level start Added by ishu 30 july 2021

                var BackIdGuid1 = _configuration.GetSection("workflowProccessGuidKYCPayment").Value;
                var BackIdGuid2 = _configuration.GetSection("workflowProccessGuidKYCPayment2").Value;

                var FirstApprovalProcessData = await _approvalproccessService.FirstkycApprovalProcessData((_configuration.GetSection("workflowProccessGuidKYCPayment").Value), payment.Id);
                var ApprovalProccessBackId = (Data.WorkFlowTemplate == "WF1") ? _approvalproccessService.GetPreviouskycApprovalId(BackIdGuid1, payment.Id) : _approvalproccessService.GetPreviouskycApprovalId(BackIdGuid2, payment.Id);
                var ApprovalProcessBackData = await _approvalproccessService.FetchKYCApprovalProcessDocumentDetails(ApprovalProccessBackId);

                var checkLastApprovalStatuscode = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalProcessBackData.Status));


                var DataFlow = (Data.WorkFlowTemplate == "WF1") ? await DataAsync(ApprovalProcessBackData.Version) : await DataAsync1(ApprovalProcessBackData.Version);

                if (Data.ApprovedStatus == 20 && payment.ApprovalStatus == "22" && SiteContext.RoleId == 29) //at 2nd level if forwarded to cash housing thn update workflow template used infoin main table
                {

                    payment.WorkFlowTemplate = "WF2";
                    result = await _kycPaymentApprovalService.UpdateworkflowinfoAtlevel2(payment.Id, payment);
                }
                else { }

                Kycapprovalproccess approvalproccess = new Kycapprovalproccess();

                /*Check if branchwise then aprovee user must have branchid*/

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
                                        if (DataFlow[d].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                                        {
                                            if (SiteContext.BranchId == null)
                                            {
                                                ViewBag.Items = await _userProfileService.GetRole();
                                                await BindApprovalStatusDropdown(payment);
                                                ViewBag.Message = Alert.Show("Your branch is not available , Without branch application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
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


                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                    approvalproccess.ProcessGuid = payment.WorkFlowTemplate == "WF1" ? BackIdGuid1 : BackIdGuid2;
                    approvalproccess.ServiceId = payment.Id;
                    approvalproccess.SendFrom = SiteContext.UserId.ToString();
                    approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
                    approvalproccess.PendingStatus = 1;
                    approvalproccess.Remarks = payment.ApprovalRemarks; ///May be comment
                    approvalproccess.Status = Convert.ToInt32(payment.ApprovalStatus);
                    // approvalproccess.Version = ApprovalProcessBackData.Version;
                    approvalproccess.Version = payment.WorkFlowTemplate == "WF1" ? ApprovalProcessBackData.Version : "V1(2097090378)";
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
                                            approvalproccess.SendTo = "0";
                                            approvalproccess.PendingStatus = 0;
                                        }
                                        else  // Forward Check
                                        {
                                            if (i == DataFlow.Count - 1)
                                            {
                                                approvalproccess.Level = 0;
                                                approvalproccess.SendTo = "0";
                                                approvalproccess.PendingStatus = 0;
                                            }
                                            else
                                            {
                                                /*Conditional check and other role user wise checks*/
                                                for (int d = i + 1; d < DataFlow.Count; d++)
                                                {
                                                    if (!DataFlow[d].parameterSkip)
                                                    {
                                                        if (payment.ApprovedStatus == 28 || payment.ApprovalStatus == "28")
                                                        {
                                                            approvalproccess.Level = 0;
                                                            approvalproccess.PendingStatus = 0;
                                                            approvalproccess.SendTo = "0";
                                                        }
                                                        else
                                                        {
                                                            approvalproccess.Level = Convert.ToInt32(DataFlow[d].parameterLevel);
                                                            //  approvalproccess.PendingStatus = 0;
                                                            approvalproccess.SendTo = payment.ApprovalUserId.ToString();
                                                        }


                                                        break;
                                                    }
                                                }
                                                //  approvalproccess.SendTo = payment.ApprovalUserId.ToString();
                                                //approvalproccess.SendTo = null;
                                            }
                                        }



                                        #region set sendto and sendtoprofileid 

                                        StringBuilder multouserprofileid = new StringBuilder();
                                        int col = 0;
                                        if (approvalproccess.SendTo != null && approvalproccess.SendTo != "0")
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
                                                    //payment.ApprovedStatus = Convert.ToInt32(payment.ApprovalStatus);
                                                    //payment.PendingAt = approvalproccess.SendTo;

                                                    if (payment.ApprovedStatus == 28 || payment.ApprovalStatus == "28")
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
                                            }
                                            result = await _kycPaymentApprovalService.UpdateBeforeApproval(payment.Id, payment);  //Update Table details 
                                            if (payment.outstandingduesDoc != null) //to upload outstanding dues doc by aao Accounts
                                            {
                                                payment.OutStandingDuesDocument = payment.outstandingduesDoc == null ? null : fileHelper.SaveFile1(OustandingDeusDoc, payment.outstandingduesDoc);

                                                var result1 = await _kycPaymentApprovalService.UpdateDetails(payment.Id, payment, SiteContext.UserId);
                                            }
                                            else { }
                                            if (result && payment.ApprovedStatus == 28) //send mail to applicant if outstanding dues
                                            {
                                                #region Mail Generation Added By Ishu
                                                string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "KycPaymentApprovalMail.html");
                                                MailSMSHelper mailG = new MailSMSHelper();

                                                #region HTML Body Generation
                                                kycOutstandingDuesMailBodyDto bodyDTO = new kycOutstandingDuesMailBodyDto();
                                                bodyDTO.FileNo = Data2.FileNo;
                                                bodyDTO.Date = DateTime.Now.ToString("dd-MMM-yyyy");
                                                bodyDTO.AllotteeName = Data2.Name == null ? "Applicant" : Data2.Name;
                                                bodyDTO.Address = Data2.Address == null ? "NA" : Data2.Address;
                                                bodyDTO.PropertyNo = Data2.PlotNo == null ? "NA" : Data2.PlotNo;
                                                bodyDTO.DatePeriod = Data3 == null ? "NA" : Data3.DemandPeriod;
                                                bodyDTO.DueDate = (DateTime.Now.AddDays(15)).ToString("dd-MMM-yyyy");
                                                bodyDTO.Amount = Data.TotalDues == 0 ? "NA" : Data.TotalDues.ToString();
                                                bodyDTO.GrountRent = Data2.Property == "Lease" ? "GrountRent" : "License Fee";
                                                bodyDTO.UserName = DDInfo.User.UserName == null ? "NA" : DDInfo.User.UserName;
                                                bodyDTO.UserEmail = DDInfo.User.Email == null ? "NA" : DDInfo.User.Email;
                                                bodyDTO.UserNo = DDInfo.User.PhoneNumber == null ? "NA" : DDInfo.User.PhoneNumber;

                                                bodyDTO.path = path;
                                                string strBodyMsg = mailG.PopulateBodyOutstandingDueskycPayment(bodyDTO);
                                                #endregion

                                                //string strMailSubject = "Pending Lease Application Approval Request Details ";
                                                //string strMailCC = "", strMailBCC = "", strAttachPath = "";
                                                //sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);

                                                #region Common Mail Genration
                                                SentMailGenerationDto maildto = new SentMailGenerationDto();
                                                maildto.strMailSubject = "Payment of Outstanding Dues";
                                                maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
                                                maildto.strBodyMsg = strBodyMsg;
                                                maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
                                                maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
                                                maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
                                                maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
                                                maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

                                                maildto.strMailTo = Data2.EmailId;
                                                var sendMailResult1 = mailG.SendMailWithAttachment(maildto);
                                                #endregion
                                                #endregion
                                            }

                                        }
                                    }
                                    break;


                                }
                            }
                        }

                    }
                    var sendMailResult = false;

                    var DataApprovalSatatusMsg = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(payment.ApprovalStatus));

                    if (approvalproccess.SendTo != null && approvalproccess.SendTo != "0")
                    {
                        #region Mail Generate

                        //At successfull completion send mail and sms
                        Uri uri = new Uri("https://master.managemybusinessess.com/ApprovalProcess/Index");
                        string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "ApprovalMailDetailsContent.html");
                        string link = "<a target=\"_blank\" href=\"" + uri + "\">Click Here</a>";
                        string linkhref = "https://master.managemybusinessess.com/ApprovalProcess/Index";

                        var senderUser = await _userProfileService.GetUserById(SiteContext.UserId);
                        StringBuilder multousermailId = new StringBuilder();
                        if (approvalproccess.SendTo != null && approvalproccess.SendTo != "0")
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

            if (Data.WorkFlowTemplate == "WF1")
            {
                var dropdownValue = await GetApprovalStatusDropdownList(Data.Id);
                List<int> dropdownValue1 = ConvertStringListToIntList(dropdownValue);
                Data.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(dropdownValue1.ToArray());

            }
            else
            {
                var dropdownValue = await GetApprovalStatusDropdownList1(Data.Id);
                List<int> dropdownValue1 = ConvertStringListToIntList(dropdownValue);
                Data.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(dropdownValue1.ToArray());

            }

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
            var ApprovalProccessBackId = _approvalproccessService.GetPreviouskycApprovalId((_configuration.GetSection("workflowProccessGuidKYCPayment").Value), serviceid);//find record in kycapprovalprocess table
            var ApprovalProcessBackData = await _approvalproccessService.FetchKYCApprovalProcessDocumentDetails(ApprovalProccessBackId);//fetch complete kycapprovalprocess record data for id
            var checkLastApprovalStatuscode = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalProcessBackData.Status));//get status name from status code

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
        public async Task<List<string>> GetApprovalStatusDropdownList1(int serviceid)  //Bind Dropdown of Approval Status 27 july 2021 ishu
        {
            List<string> dropdown = null;
            var ApprovalProccessBackId = _approvalproccessService.GetPreviouskycApprovalId((_configuration.GetSection("workflowProccessGuidKYCPayment2").Value), serviceid);//find record in kycapprovalprocess table
            var ApprovalProcessBackData = await _approvalproccessService.FetchKYCApprovalProcessDocumentDetails(ApprovalProccessBackId);//fetch complete kycapprovalprocess record data for id
            var checkLastApprovalStatuscode = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalProcessBackData.Status));//get status name from status code

            var DataFlow1 = await DataAsync1(ApprovalProcessBackData.Version);

            if (checkLastApprovalStatuscode.StatusCode != ((int)ApprovalActionStatus.QueryForward))
            {
                for (int i = 0; i < DataFlow1.Count; i++)
                {
                    if (i == ApprovalProcessBackData.Level - 1 && Convert.ToInt32(DataFlow1[i].parameterLevel) == ApprovalProcessBackData.Level)
                    {
                        dropdown = (List<string>)DataFlow1[i].parameterAction;
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
        private async Task<List<TemplateStructure>> DataAsync1(string version)//fetch data from different workflow template defined
        {
            var Data = await _kycformApprovalService.FetchSingleResultOnProcessGuidWithVersion((_configuration.GetSection("workflowProccessGuidKYCPayment2").Value), version);
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
            var Data = await _approvalproccessService.GetKYCPaymentHistoryDetails((_configuration.GetSection("workflowProccessGuidKYCPayment").Value), id);

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

        //***************** Download all form accordian Files  ********************

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
        public async Task<IActionResult> DownloadChallanProof(int Id)
        {
            FileHelper file = new FileHelper();
            Kycdemandpaymentdetailstablec Data = await _kycdemandpaymentdetailstablecService.FetchSingleResult(Id);
            string filename = ChallanProof + Data.Proofinpdf;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        public async Task<IActionResult> DownloadOutstandingDuesDoc(int Id)
        {
            FileHelper file = new FileHelper();
            Kycdemandpaymentdetails Data = await _kycPaymentApprovalService.FetchSingleResult(Id);
            string filename = OustandingDeusDoc + Data.OutStandingDuesDocument;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
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

        [HttpPost]
        //public async Task<JsonResult> GetForwardedUserList(string value,string status)
        public async Task<JsonResult> GetForwardedUserList([FromBody] ForwardedUserListDto jsondata)
        {
            if (jsondata != null)
            {
                int serviceid = Convert.ToInt32(jsondata.Id1);
                int Status = Convert.ToInt32(jsondata.statusId);
                string WF = jsondata.WorkFlowTemplate;
                List<string> dropdown = null;

                var BackIdGuid1 = _configuration.GetSection("workflowProccessGuidKYCPayment").Value;
                var BackIdGuid2 = _configuration.GetSection("workflowProccessGuidKYCPayment2").Value;


                //var ApprovalProccessBackId = (Data.WorkFlowTemplate == "WF1") ? _approvalproccessService.GetPreviouskycApprovalId(BackIdGuid1, payment.Id) : _approvalproccessService.GetPreviouskycApprovalId(BackIdGuid2, payment.Id);



                var ApprovalProccessBackId = (WF == "WF1") ? _approvalproccessService.GetPreviouskycApprovalId(BackIdGuid1, serviceid) : _approvalproccessService.GetPreviouskycApprovalId(BackIdGuid2, serviceid);
                var ApprovalProcessBackData = await _approvalproccessService.FetchKYCApprovalProcessDocumentDetails(ApprovalProccessBackId);
                var checkLastApprovalStatuscode = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalProcessBackData.Status));

                //var DataFlow = await DataAsync(ApprovalProcessBackData.Version);
                var DataFlow = (WF == "WF1") ? await DataAsync(ApprovalProcessBackData.Version) : await DataAsync1(ApprovalProcessBackData.Version);

                if (ApprovalProcessBackData.Level == 2 && Status == 22 && SiteContext.RoleId == 29) //when forwarding to aao cash housing
                {
                    var DataFlow1 = await DataAsync1("V1(2097090378)");

                    List<string> JsonMsg1 = new List<string>();
                    if (checkLastApprovalStatuscode.StatusCode != ((int)ApprovalActionStatus.QueryForward))
                    {
                        for (int i = 0; i < DataFlow1.Count; i++)
                        {
                            if (!DataFlow1[i].parameterSkip)
                            {
                                if (i == ApprovalProcessBackData.Level - 1 && Convert.ToInt32(DataFlow1[i].parameterLevel) == ApprovalProcessBackData.Level)
                                {
                                    for (int d = i + 1; d < DataFlow1.Count; d++)
                                    {
                                        if (!DataFlow1[d].parameterSkip)
                                        {
                                            if (DataFlow1[d].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                                            {
                                                if (SiteContext.BranchId == null)
                                                {
                                                    JsonMsg1.Add("false");
                                                    JsonMsg1.Add("Branch Id not available for next level, untill then Submittion is not possible");
                                                    return Json(JsonMsg1);
                                                }

                                            }
                                            if (DataFlow1[d].parameterValue == (_configuration.GetSection("ApprovalRoleType").Value))
                                            {
                                                for (int b = 0; b < DataFlow1[d].parameterName.Count; b++)
                                                {
                                                    List<kycUserProfileInfoDetailsDto> UserListRoleBasis = null;
                                                    if (DataFlow1[d].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))



                                                        UserListRoleBasis = await _userProfileService.kycGetUserOnRoleZoneBasisConcatedName(Convert.ToInt32(DataFlow1[d].parameterName[b]), SiteContext.BranchId ?? 0);

                                                    else
                                                        UserListRoleBasis = await _userProfileService.GetkycUserOnRoleBasisConcatedName(Convert.ToInt32(DataFlow1[d].parameterName[b]));

                                                    if (UserListRoleBasis.Count == 0)
                                                    {
                                                        JsonMsg1.Add("false");
                                                        JsonMsg1.Add("No user found at the next approval level, In this case, the system is unable to process your request. Please contact to the system administrator.");
                                                        return Json(JsonMsg1);
                                                    }
                                                    else
                                                    {
                                                        return Json(UserListRoleBasis);
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                string SendTo = String.Join(",", (DataFlow1[d].parameterName));
                                                if (DataFlow1[d].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
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
                                                    JsonMsg1.Add("false");
                                                    JsonMsg1.Add("No user found at the next approval level, In this case, the system is unable to process your request. Please contact to the system administrator.");
                                                    return Json(JsonMsg1);
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



                else
                {
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
                                                for (int b = 0; b < DataFlow[d].parameterName.Count; b++)
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
            }
            else return Json("No data available");
        }


        #endregion


        [HttpPost]
        public async Task<IActionResult> UpdatePayment([FromBody] List<KycPaymentApprovalUpdateDto> jsondata)
        {
            if (jsondata != null)
            {
                decimal totalPayableAmount = jsondata[0].TotalPayable ?? 0;
                decimal totalPayableInterest = jsondata[0].TotalPayableInterest ?? 0;
                decimal totalPayableDues = jsondata[0].TotalPayableDues ?? 0;
                List<Kycdemandpaymentdetailstablea> payment = new List<Kycdemandpaymentdetailstablea>();
                var result = await _kycPaymentApprovalService.DeletePayment(jsondata[0].DemandPaymentId);
                for (int i = 0; i < jsondata.Count; i++)
                {
                    payment.Add(new Kycdemandpaymentdetailstablea
                    {

                        KycId = jsondata[i].KycId,
                        DemandPaymentId = jsondata[i].DemandPaymentId,
                        DemandPeriod = jsondata[i].DemandPeriod,
                        GroundRent = jsondata[i].GroundRent,
                        InterestRate = jsondata[i].InterestRate,
                        TotdalDues = jsondata[i].TotdalDues,


                    });
                }

                var result1 = await _kycPaymentApprovalService.SavePayment(payment);


                if (result1 == true)
                {
                    Kycdemandpaymentdetails kycpayment = new Kycdemandpaymentdetails();
                    kycpayment.TotalPayable = totalPayableAmount;
                    kycpayment.TotalPayableInterest = totalPayableInterest;
                    kycpayment.TotalDues = totalPayableDues;
                    var result2 = await _kycPaymentApprovalService.UpdateDetails(jsondata[0].DemandPaymentId, kycpayment, SiteContext.UserId);
                    ViewBag.Message = Alert.Show("Record updated successfully", "", AlertType.Success);
                    return Json("Record updated successfully");


                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return Json("Record Not Updated");


                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return Json("Record Not Updated");

            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateChallan([FromBody] List<KycChallanApprovalUpdateDto> jsondata)
        {

            if (jsondata.Count != 0)
            {
                decimal totalPayableAmount = jsondata[0].TotalPayable ?? 0;
                decimal totalPayableInterest = jsondata[0].TotalPayableInterest ?? 0;
                decimal totalPayableDues = jsondata[0].TotalPayableDues ?? 0;
                List<Kycdemandpaymentdetailstablec> challan = new List<Kycdemandpaymentdetailstablec>();
                var result = await _kycPaymentApprovalService.DeleteChallan(jsondata[0].DemandPaymentId);
                for (int i = 0; i < jsondata.Count; i++)
                {
                    challan.Add(new Kycdemandpaymentdetailstablec
                    {

                        KycId = jsondata[i].KycId,
                        DemandPaymentId = jsondata[i].DemandPaymentId,
                        IsVerified = jsondata[i].IsVerified,
                        PaymentType = jsondata[i].PaymentType,
                        Period = jsondata[i].Period,
                        ChallanNo = jsondata[i].ChallanNo,
                        Amount = jsondata[i].Amount,
                        DateofPaymentByAllottee = jsondata[i].DateofPaymentByAllottee,
                        Proofinpdf = jsondata[i].Proofinpdf,
                        Ddabankcredit = jsondata[i].Ddabankcredit,


                    });
                }

                var result1 = await _kycPaymentApprovalService.SaveChallan(challan);


                if (result1 == true)
                {
                    Kycdemandpaymentdetails kycpayment = new Kycdemandpaymentdetails();
                    kycpayment.TotalPayable = totalPayableAmount;
                    kycpayment.TotalPayableInterest = totalPayableInterest;
                    kycpayment.TotalDues = totalPayableDues;
                    var result2 = await _kycPaymentApprovalService.UpdateDetails(jsondata[0].DemandPaymentId, kycpayment, SiteContext.UserId);

                    ViewBag.Message = Alert.Show("Record updated successfully", "", AlertType.Success);


                    return Json("Record updated successfully");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return Json("Record Not Updated");

                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return Json("No Data Available to update");

            }

        }


        [HttpPost]
        public async Task<IActionResult> UpdateBhoomi([FromBody] List<InsertIntoLIMSPaymentApidto> jsondata)
        {
            string msg = string.Empty;
            if (jsondata.Count != 0)
            {

                for (int i = 0; i < jsondata.Count; i++)
                {

                    HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(_configuration.GetSection("InsertInLIMSpaymentApi").Value);
                    request.Method = "POST";
                    string json = "CHLLN_NO=" + jsondata[i].CHLLN_NO + "&CHLLN_AMNT=" + jsondata[i].CHLLN_AMNT + "&DPST_DT=" + Convert.ToDateTime(jsondata[i].DPST_DT).ToString("dd/MM/yyyy") + "&USR_ID=" + jsondata[i].USR_ID + "&SCHM_ID=" + jsondata[i].SCHM_ID + "&FL_NMBR=" + jsondata[i].FL_NMBR;
                    request.Timeout = 1000 * 30;
                    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
                    request.PreAuthenticate = true;
                    request.Credentials = CredentialCache.DefaultCredentials;

                    byte[] byteArray = Encoding.UTF8.GetBytes(json);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        if (result == "{\"cargo\":1}")
                        {
                            if (msg == string.Empty)
                            {
                                msg = "<ul><li>Data for Challna No <b>" + jsondata[i].CHLLN_NO + "</b> updated in Bhoomi Application</li>";
                            }
                            else
                            {
                                msg = msg + "<li>Data for Challna No <b>" + jsondata[i].CHLLN_NO + "</b> updated in Bhoomi Application</li>";
                            }

                        }
                        else if (result == "{\"cargo\":0}")
                        {
                            if (msg == string.Empty)
                            {
                                msg = "<ul><li>Challna No <b>" + jsondata[i].CHLLN_NO + "</b> already updated in Bhoomi Application</li>";
                            }
                            else
                            {
                                msg = msg + "<li>Challna No <b>" + jsondata[i].CHLLN_NO + "</b> already updated in Bhoomi Application</li>";
                            }

                        }
                        else if (result == "{\"cargo\":2}")
                        {
                            if (msg == string.Empty)
                            {
                                msg = "<ul><li>Not able to update data as Challna No <b>" + jsondata[i].CHLLN_NO + "</b> is not valid </li>";
                            }
                            else
                            {
                                msg = msg + "<li>Not able to update data as Challna No <b>" + jsondata[i].CHLLN_NO + "</b> is not valid </li>";
                            }

                        }
                        else
                        {
                            if (msg == string.Empty)
                            {
                                msg = "<ul><li>Not able to update data for Challna No <b>" + jsondata[i].CHLLN_NO + "</b> in Bhoomi Application</li>";
                            }
                            else
                            {
                                msg = msg + "<li>Not able to update data for Challna No <b>" + jsondata[i].CHLLN_NO + "</b> in Bhoomi Application</li>";
                            }
                        }
                    }

                }
                msg = msg + "</ul>";
                return Json(msg);


            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return Json("No Data Available to update");

            }

        }

    }
}
