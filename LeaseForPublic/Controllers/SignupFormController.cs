using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Enum;
using LeaseForPublic.Filters;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using System.IO;
using Microsoft.AspNetCore.Hosting;

using Dto.Master;
using Dto.Common;



using Service.IApplicationService;
using System.Text;



using Microsoft.AspNetCore.Http;

using Dto.Search;





namespace LeaseForPublic.Controllers
{
    public class SignupFormController : BaseController
    {
        private readonly ILeasesignupService _leasesignupService;
        public IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IKycformService _kycformService;
        private readonly IUserProfileService _userProfileService;
        private readonly IApprovalProccessService _approvalproccessService;
        string AadharDoc = "";
        string LetterDoc = "";
        string ApplicantDoc = "";


        public SignupFormController(IConfiguration configuration, ILeasesignupService leasesignupService, IHostingEnvironment hostingEnvironment,
            IKycformService KycformService,
            IUserProfileService userProfileService,
             IApprovalProccessService approvalproccessService)

        {
            _configuration = configuration;
            _leasesignupService = leasesignupService;
            _hostingEnvironment = hostingEnvironment;
            _kycformService = KycformService;
            AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
            LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
            ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();
            _userProfileService = userProfileService;
            _approvalproccessService = approvalproccessService;

        }
        public IActionResult Index()
        {

           // string str;
           // str = TempData["data1"].ToString();
          //  ViewBag.Message = TempData["data1"] as string;
           // ViewBag.Message = TempData["Message"] as string;
            //var Id = HttpContext.Session.GetString("Id");
            var mobile = HttpContext.Session.GetString("Mobile");
            return View();
        }

        //[HttpPost]
        //public async Task<PartialViewResult> KycList(string Mobileno)
        //{


        //    var mobile = HttpContext.Session.GetString("Mobile");
        //    Mobileno = mobile;
        //    var result = await _leasesignupService.GetAllKycformList(Mobileno);
        //    return PartialView("_List", result);
        //}



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Leasesignuplist model)
        {


            var mobile = HttpContext.Session.GetString("Mobile");
            model.Mobileno = mobile;
            var result = await _leasesignupService.AllKycformList(model);
            return PartialView("_List", result);
        }

        public async Task<IActionResult> CreateLogin()
        {
            Leasesignup leasesignup = new Leasesignup();

            return View(leasesignup);
        }

        public async Task<IActionResult> Create(int id)
        {
            var Data = await _kycformService.FetchSingleResult(id);
            Leasesignup leasesignup = new Leasesignup();


            return View(leasesignup);

        }

        public async Task<IActionResult> CreateKyc()
        {
            var mobile = HttpContext.Session.GetString("Mobile");
            var email = HttpContext.Session.GetString("Email");
            Kycform kyc = new Kycform();
            kyc.MobileNo = mobile;
            kyc.EmailId = email;
            kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            kyc.BranchList = await _kycformService.GetAllBranchList();
            kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            kyc.ZoneList = await _kycformService.GetAllZoneList();
            kyc.LocalityList = await _kycformService.GetLocalityList();
            return View(kyc);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> CreateKyc(Kycform kyc)
        {
            kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            kyc.BranchList = await _kycformService.GetAllBranchList();
            kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            kyc.ZoneList = await _kycformService.GetAllZoneList();
            kyc.LocalityList = await _kycformService.GetLocalityList();
            string AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
            string LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
            string ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();


            if (ModelState.IsValid)
            {
                #region Approval Proccess At 1st level Check Initial Before Creating Record

                Kycapprovalproccess approvalproccess = new Kycapprovalproccess();
                var DataFlow = await dataAsync();
                for (int i = 0; i < DataFlow.Count; i++)
                {
                    if (!DataFlow[i].parameterSkip)
                    {
                        if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                        {
                            if (kyc.BranchId == null)
                            {
                                ViewBag.Message = Alert.Show("Your Branch is not available , Without branch application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                return View(kyc);
                            }

                            // leaseapplication.ApprovalZoneId = SiteContext.ZoneId;
                        }
                        if (DataFlow[i].parameterValue == (_configuration.GetSection("ApprovalRoleType").Value))
                        {
                            for (int j = 0; j < DataFlow[i].parameterName.Count; j++)
                            {
                                List<UserProfileDto> UserListRoleBasis = null;
                                if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                                    UserListRoleBasis = await _userProfileService.GetUserOnRoleBranchBasis(Convert.ToInt32(DataFlow[i].parameterName[j]), kyc.BranchId ?? 0);
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
                            if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalBranchWise").Value))
                            {
                                StringBuilder multousersbranchwise = new StringBuilder();
                                int col = 0;
                                if (approvalproccess.SendTo != null)
                                {
                                    string[] multiTo = approvalproccess.SendTo.Split(',');
                                    foreach (string MultiUserId in multiTo)
                                    {
                                        if (col > 0)
                                            multousersbranchwise.Append(",");
                                        var UserProfile = await _userProfileService.GetUserByIdBranch(Convert.ToInt32(MultiUserId), kyc.BranchId ?? 0);
                                        multousersbranchwise.Append(UserProfile.UserId);
                                        col++;
                                    }
                                    approvalproccess.SendTo = multousersbranchwise.ToString();
                                }
                            }
                        }


                        break;
                    }
                }
                #endregion



                FileHelper fileHelper = new FileHelper();

                if (kyc.Aadhar != null)
                {
                    kyc.AadhaarNoPath = fileHelper.SaveFile1(AadharDoc, kyc.Aadhar);
                }
                if (kyc.Letter != null)
                {
                    kyc.LetterPath = fileHelper.SaveFile1(LetterDoc, kyc.Letter);
                }
                if (kyc.ApplicantPan != null)
                {
                    kyc.AadhaarPanapplicantPath = fileHelper.SaveFile1(ApplicantDoc, kyc.ApplicantPan);
                }

                kyc.CreatedBy = SiteContext.UserId;
                kyc.IsActive = 1;
                var result = await _kycformService.Create(kyc);
                if (result == true)
                {

                    #region Approval Proccess At 1st level start Added by ishu  20 july 2021
                    var workflowtemplatedata = await _kycformService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProccessGuidKYCForm").Value));

                    var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);

                    for (int i = 0; i < DataFlow.Count; i++)
                    {
                        if (!DataFlow[i].parameterSkip)
                        {
                            kyc.ApprovedStatus = ApprovalStatus.Id;
                            kyc.PendingAt = approvalproccess.SendTo;
                            result = await _kycformService.UpdateBeforeApproval(kyc.Id, kyc);  //Update kycform Table details 
                            if (result)
                            {
                                approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                approvalproccess.ProcessGuid = (_configuration.GetSection("workflowProccessGuidKYCForm").Value);
                                approvalproccess.ServiceId = kyc.Id;
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
                                result = await _kycformService.CreatekycApproval(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table

                                //#region Insert Into usernotification table Added By Renu 18 June 2021
                                //if (result)
                                //{
                                //    var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidLeaseApplicationForm").Value);
                                //    var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                //    Usernotification usernotification = new Usernotification();
                                //    var replacement = notificationtemplate.Template.Replace("{proccess name}", "Lease").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                //    usernotification.Message = replacement;
                                //    usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidLeaseApplicationForm").Value);
                                //    usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                //    usernotification.ServiceId = approvalproccess.ServiceId;
                                //    usernotification.SendFrom = approvalproccess.SendFrom;
                                //    usernotification.SendTo = approvalproccess.SendTo;
                                //    result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                                //}
                                //#endregion
                            }

                            break;
                        }
                    }

                    #endregion

                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                    var list = await _kycformService.GetAllKycform();
                    return View("CreateLogin");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(kyc);

                }
            }
            else
            {
                return View(kyc);
            }

        }


        //get kycworkflowtemplate table data
        #region Fetch workflow data for approval prrocess Added by Renu 16 March 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _kycformService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProccessGuidKYCForm").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion

        //[HttpPost]
        //public async Task<PartialViewResult> sendotp([FromBody] DamagePayeeRegistrationSearchDto model)
        //{
        //    var result = await _damagePayeeRegistrationService.GetPagedDamagePayeeRegistration(model);
        //    return PartialView("_List", result);
        //}


        [HttpPost]
        public async Task<IActionResult> sendotp([FromBody] Leasesignup model)
        {

            List<string> JsonMsg = new List<string>();
            var IsEmailExist = await _leasesignupService.ValidateMobileEmail(model.MobileNo, model.EmailId);
            if (IsEmailExist)
            {
                JsonMsg.Add("false");
                JsonMsg.Add("This Email Id and Mobile is already Linked with Another Account. Kindly use different Email Id");
                return Json(JsonMsg);
            }

            string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "OTPMailDesign.html");



            var sendMailResult = false;
            Random random = new Random();
            var otp = random.Next(111111, 999999);


            string Action = "Otp is " + otp;
            String Mobile = model.MobileNo;
            String EmailID = model.EmailId;


            MailSMSHelper mail = new MailSMSHelper();

            #region HTML Body Generation
            LeaseSignupMailDto bodyDTO = new LeaseSignupMailDto();

            bodyDTO.Otp = otp.ToString();

            bodyDTO.path = path;
            string strBodyMsg = mail.GenerateMailFormatForLeaseSignUp(bodyDTO);
            #endregion




            #region Common Mail Genration
            SentMailGenerationDto maildto = new SentMailGenerationDto();
            maildto.strMailSubject = "OTP Request";
            maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
            maildto.strBodyMsg = strBodyMsg;
            maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
            maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
            maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
            maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
            maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

            maildto.strMailTo = model.EmailId;
            sendMailResult = mail.SendMailWithAttachment(maildto);
            #endregion


            SendSMSDto SMS = new SendSMSDto();
            SMS.GenerateSendSMS(Action, Mobile);
            HttpContext.Session.SetString("Mobile", model.MobileNo);
            HttpContext.Session.SetString("Email", model.EmailId);
            JsonMsg.Add("true");
            JsonMsg.Add("Otp send successfully!");
            JsonMsg.Add(otp.ToString());
            return Json(JsonMsg);

        }


        [HttpPost]

        public async Task<IActionResult> Create(Leasesignup leasesignup)
        {
            try
            {
                var mobile = HttpContext.Session.GetString("Mobile");
                var email = HttpContext.Session.GetString("Email");

                if (ModelState.IsValid)
                {
                    var result = await _leasesignupService.Create(leasesignup);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.RegistrationConfirm, "", AlertType.Success);
                        return RedirectToAction("Index", "KYCform");
                        //return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(leasesignup);
                    }
                }
                else
                {
                    return View(leasesignup);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(leasesignup);
            }
        }





        [HttpPost]

        public async Task<IActionResult> CreateLogin(Leasesignup leasesignup)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _leasesignupService.Create(leasesignup);
                    if (result == true)
                    {
                        
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(leasesignup);
                    }
                }
                else
                {
                    return View(leasesignup);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(leasesignup);
            }
        }



        [HttpPost]
        public async Task<IActionResult> sendotp1([FromBody] Leasesignup model)
        {

            List<string> JsonMsg = new List<string>();
            var IsEmailExist = await _leasesignupService.ValidateMobileEmail(model.MobileNo, model.EmailId);
            var checkemail = await _leasesignupService.GetEmailAndMobile(model.MobileNo, model.EmailId);

            if (!IsEmailExist)
            {
                JsonMsg.Add("false");
                JsonMsg.Add("This Mobile Number is not Registered. Please Signup");
                return Json(JsonMsg);
            }

            string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "OTPMailDesign.html");

            var sendMailResult = false;
            Random random = new Random();
            var otp = random.Next(111111, 999999);


            string Action = "Otp is " + otp;
            String Mobile = model.MobileNo;
            String EmailID = model.EmailId;


            MailSMSHelper mail = new MailSMSHelper();

            #region HTML Body Generation
            LeaseSignupMailDto bodyDTO = new LeaseSignupMailDto();

            bodyDTO.Otp = otp.ToString();

            bodyDTO.path = path;
            string strBodyMsg = mail.GenerateMailFormatForLeaseSignUp(bodyDTO);
            #endregion




            #region Common Mail Genration
            SentMailGenerationDto maildto = new SentMailGenerationDto();
            maildto.strMailSubject = "OTP Request";
            maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
            maildto.strBodyMsg = strBodyMsg;
            maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
            maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
            maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
            maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
            maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

            maildto.strMailTo = model.EmailId;
            sendMailResult = mail.SendMailWithAttachment(maildto);
            #endregion


            SendSMSDto SMS = new SendSMSDto();
            SMS.GenerateSendSMS(Action, Mobile);

            //TempData["data1"] = model.MobileNo;
          //  TempData["Message"] = Alert.Show(Messages.AddRecordSuccess + " Your Reference No is  " + model.MobileNo, "", AlertType.Success);


            // return str;
            // return RedirectToAction("Read");

            HttpContext.Session.SetString("Mobile", model.MobileNo);
            HttpContext.Session.SetString("Email", checkemail[0].EmailId);
            //  var mobile = HttpContext.Session.GetString("Mobile");
            JsonMsg.Add("true");
            JsonMsg.Add("Otp send successfully!");
            JsonMsg.Add(otp.ToString());
            return Json(JsonMsg);

        }





    }
}
