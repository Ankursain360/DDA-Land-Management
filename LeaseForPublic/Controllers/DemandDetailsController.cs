using Core.Enum;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utility.Helper;

namespace LeaseForPublic.Controllers
{
    public class DemandDetailsController : BaseController
    {
        private readonly IDemandDetailsService _demandDetailsService;
        private readonly IKycformService _kycformService;
        private readonly IKycdemandpaymentdetailstableaService __kycdemandpaymentdetailstableaService;
        private readonly IKycdemandpaymentdetailstablebService _kycdemandpaymentdetailstablebService;
        private readonly IKycdemandpaymentdetailstablecService _kycdemandpaymentdetailstablecService;

        private readonly IUserProfileService _userProfileService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IKycformApprovalService _kycformApprovalService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IKycPaymentApprovalService _kycPaymentApprovalService;

        public IConfiguration _configuration;
        public DemandDetailsController(IConfiguration configuration,
            IDemandDetailsService demandDetailsService,
            IKycformService kycform,
            IKycdemandpaymentdetailstableaService kycdemandpaymentdetailstableaService,
            IKycdemandpaymentdetailstablebService kycdemandpaymentdetailstablebService,
            IKycdemandpaymentdetailstablecService kycdemandpaymentdetailstablecService,
             IKycPaymentApprovalService kycPaymentApprovalService,
             IUserNotificationService userNotificationService,
            IUserProfileService userProfileService,
             IApprovalProccessService approvalproccessService,
             IKycformApprovalService kycformApprovalService,
             IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _demandDetailsService = demandDetailsService;
            _kycformService = kycform;
            __kycdemandpaymentdetailstableaService = kycdemandpaymentdetailstableaService;
            _kycdemandpaymentdetailstablebService = kycdemandpaymentdetailstablebService;
            _kycdemandpaymentdetailstablecService=kycdemandpaymentdetailstablecService;

            _kycPaymentApprovalService = kycPaymentApprovalService;
            _userProfileService = userProfileService;
            _approvalproccessService = approvalproccessService;
            _kycformApprovalService = kycformApprovalService;
           _userNotificationService = userNotificationService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        
        {
            return View();
        }


        public async Task<IActionResult> Create(int Id)
        {
            LeasePublicDemandPaymentDetailsDto dto = new LeasePublicDemandPaymentDetailsDto();
            var data= await _kycformService.FetchSingleResult(Id);
            dto.KycId = data.Id;
            dto.FileNo = data.FileNo;           
            return View(dto);

        }


        public async Task<PartialViewResult> KYCFormView(int id)
        {
            var Data = await _kycformService.FetchKYCSingleResult(id);
            Data.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            Data.BranchList = await _kycformService.GetAllBranchList();
            Data.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            Data.ZoneList = await _kycformService.GetAllZoneList();
            Data.LocalityList = await _kycformService.GetLocalityList();
            
            return PartialView("_KYCFormView", Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeasePublicDemandPaymentDetailsDto dto)
        {
            Kycdemandpaymentdetails oKycdemandpaymentdetails = new Kycdemandpaymentdetails();
            try
            {
                ModelState.Remove("Amount");
                ModelState.Remove("DateofPaymentByAllottee");

                if (ModelState.IsValid)
                {
                    #region Approval Proccess At 1st level Check Initial Before Creating Record  Added by ishu 2 august 2021

                    Kycapprovalproccess approvalproccess = new Kycapprovalproccess();
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
                                    return View(dto);
                                }

                               // leaseapplication.ApprovalZoneId = SiteContext.ZoneId;
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


                    oKycdemandpaymentdetails.KycId = dto.KycId;
                    oKycdemandpaymentdetails.PendingAt = dto.PendingAt;
                    oKycdemandpaymentdetails.TotalPayable = dto.TotalPayable;
                    oKycdemandpaymentdetails.TotalDues=dto.TotalDues;
                    oKycdemandpaymentdetails.IsPaymentAgreed = dto.IsPaymentAgreed;
                    oKycdemandpaymentdetails.CreatedBy = SiteContext.UserId;
                    oKycdemandpaymentdetails.IsActive = 1;
                    oKycdemandpaymentdetails.CreatedDate = DateTime.Now;

                    var result = await _demandDetailsService.Create(oKycdemandpaymentdetails);

                    if (result == true)
                    {
                        var result1 = await _demandDetailsService.GetPaymentDetails(Convert.ToInt32(dto.KycId));
                        List<Kycdemandpaymentdetailstablea> data = new List<Kycdemandpaymentdetailstablea>();

                        //*********************************************** Save Payment Deatails  ****************************  
                        if (result1 != null)
                        {
                            for (int i = 0; i < result1.Count; i++)
                            {
                                data.Add(new Kycdemandpaymentdetailstablea()
                                {
                                    DemandPeriod = result1[i].DemandPeriod,
                                    GroundRent = result1[i].GroundRentLeaseRent,
                                    InterestRate = result1[i].InterestAmount,
                                    TotdalDues = result1[i].TotalDues,                                
                                    KycId= oKycdemandpaymentdetails.KycId,
                                    DemandPaymentId = oKycdemandpaymentdetails.Id,
                                    IsActive = 1,
                                    CreatedBy =SiteContext.UserId,
                                    CreatedDate= DateTime.Now,
                            });
                            }

                            foreach (var item in data)
                            {
                                result = await __kycdemandpaymentdetailstableaService.SaveDemandPaymentDetails(item);
                            }
                        }

                        //*********************************************** Save Payment API Details  ********************************  
                        using (var httpClient = new HttpClient())
                        {
                            using (var response = await httpClient.GetAsync(_configuration.GetSection("BhoomiApi").Value + dto.FileNo))
                            {
                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    string apiResponse = await response.Content.ReadAsStringAsync();
                                    var result2 = JsonSerializer.Deserialize<ApiResponseBhoomiApiFileWise>(apiResponse);

                                    List<Kycdemandpaymentdetailstableb> datac = new List<Kycdemandpaymentdetailstableb>();
                                    if (result2 != null)
                                    {
                                        for (int i = 0; i < result2.cargo.Count(); i++)
                                        {
                                            datac.Add(new Kycdemandpaymentdetailstableb()
                                            {
                                                ChallanNo = result2.cargo[i].CHLLN_NMBR,
                                                ChallanAmount = result2.cargo[i].CHLLN_AMNT.ToString(),
                                                DepositeDate =Convert.ToDateTime(result2.cargo[i].DPST_DT),
                                                KycId = oKycdemandpaymentdetails.KycId,
                                                DemandPaymentId = oKycdemandpaymentdetails.Id,
                                                CreatedBy=SiteContext.UserId,
                                                CreatedDate=DateTime.Now,
                                                IsActive=1,
                                            });
                                        }

                                        foreach (var item in datac)
                                        {
                                            var result3 = await _kycdemandpaymentdetailstablebService.SaveDemandPaymentAPIDetails(item);
                                        }
                                    }


                                }
                            }
                        }

                        //********************************** Save Payment Challan Details  ********************************************  

                        if (oKycdemandpaymentdetails.IsPaymentAgreed == "N")
                        {
                            
                          
                                List<Kycdemandpaymentdetailstablec> okycdemandpaymentdetailstablec = new List<Kycdemandpaymentdetailstablec>();
                                for (int i = 0; i < dto.PaymentType.Count; i++)
                                {
                                    okycdemandpaymentdetailstablec.Add(new Kycdemandpaymentdetailstablec
                                    {
                                        PaymentType = dto.PaymentType[i],
                                        Period = dto.Period[i],
                                        ChallanNo = dto.ChallanNoForPayment[i],   
                                        Amount = dto.Amount[i],
                                        Proofinpdf = dto.Proofinpdf[i],
                                        DateofPaymentByAllottee = dto.DateofPaymentByAllottee[i],
                                        Ddabankcredit = dto.Ddabankcredit[i],
                                        CreatedBy=SiteContext.UserId,
                                         IsActive=1,
                                         CreatedDate=DateTime.Now,
                                         KycId=oKycdemandpaymentdetails.KycId,
                                        DemandPaymentId = oKycdemandpaymentdetails.Id,
                                    });
                                }
                                foreach (var item in okycdemandpaymentdetailstablec)
                                {
                                    var result4 = await _kycdemandpaymentdetailstablecService.SaveKycChallanDetails(item);
                                }

                            }


                        #region Approval Proccess At 1st level start Added by Ishu 21 April 2021


                        var workflowtemplatedata = await _kycPaymentApprovalService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProccessGuidKYCPayment").Value));
                        var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);

                        for (int i = 0; i < DataFlow.Count; i++)
                        {
                            if (!DataFlow[i].parameterSkip)
                            {
                                oKycdemandpaymentdetails.ApprovedStatus = ApprovalStatus.Id;
                                oKycdemandpaymentdetails.PendingAt = approvalproccess.SendTo;

                                result = await _demandDetailsService.UpdateBeforeApproval(oKycdemandpaymentdetails.Id, oKycdemandpaymentdetails);  //Update Kycdemandpaymentdetails  Table details 
                               
                                if (result)
                                {
                                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                    approvalproccess.ProcessGuid = (_configuration.GetSection("workflowProccessGuidKYCPayment").Value);
                                    approvalproccess.ServiceId = oKycdemandpaymentdetails.Id;
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
                                    result = await _kycformService.CreatekycApproval(approvalproccess, SiteContext.UserId); //Create a row in kycapprovalproccess Table

                                    #region Insert Into usernotification table Added By Renu 18 June 2021
                                    if (result)
                                    {
                                        var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidKycPayment").Value);
                                        var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                        Usernotification usernotification = new Usernotification();
                                        var replacement = notificationtemplate.Template.Replace("{proccess name}", "Lease").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                        usernotification.Message = replacement;
                                        usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidKycPayment").Value);
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

                        #region Approval Proccess  Mail Generation Added by ishu 2 august 2021
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

                            #region Mail Generation Added By ishu

                            MailSMSHelper mailG = new MailSMSHelper();

                            #region HTML Body Generation
                            ApprovalMailBodyDto bodyDTO = new ApprovalMailBodyDto();
                            bodyDTO.ApplicationName = "KYC Payment Approval";
                            bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                            bodyDTO.SenderName = senderUser.User.Name;
                            bodyDTO.Link = linkhref;
                            bodyDTO.AppRefNo = oKycdemandpaymentdetails.Id.ToString();
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
                            maildto.strMailSubject = "Pending KYC Payment Approval Request Details ";
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

                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(dto);

                    }
                }
                else
                {
                    return View(dto);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(dto);
            }
            return View(dto);
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



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemandDetailsSearchDto model)
        {
            var result = await _demandDetailsService.GetPagedDemandDetails(model, "8506092802");

            return PartialView("_List", result);
        }


        #region Fetch workflow data for approval prrocess Added by Renu 16 March 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _kycformApprovalService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProccessGuidKYCPayment").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion
    }


}
