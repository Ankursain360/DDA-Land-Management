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
using Dto.Search;
using System.IO;
using Dto.Master;
using Service.IApplicationService;
using System.Text;

using Microsoft.AspNetCore.Http;





namespace LeaseForPublic.Controllers
{
    public class KYCformController : BaseController
    {
        private readonly IKycformService _kycformService;
        public IConfiguration _configuration;
        private readonly IUserProfileService _userProfileService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserNotificationService _userNotificationService;
        string AadharDoc = "";
        string LetterDoc = "";
        string ApplicantDoc = "";
        public KYCformController(IConfiguration configuration,
             IUserNotificationService userNotificationService,
            IKycformService KycformService,
            IUserProfileService userProfileService,
             IApprovalProccessService approvalproccessService )
          
        {
            _configuration = configuration;
            _kycformService = KycformService;
            _userNotificationService = userNotificationService;
            AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
             LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
             ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();
            _userProfileService = userProfileService;
            _approvalproccessService = approvalproccessService;
        }
        public IActionResult Index()
        {
            var mobile = HttpContext.Session.GetString("Mobile");
            if (mobile != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Create", "SignupForm");
            }
               
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] KycformSearchDto model)
        {
            var mobile = HttpContext.Session.GetString("Mobile");
          ////  if (mobile!=null)
           // {
                model.Mobileno = mobile.ToString();
                var result = await _kycformService.GetPagedKycform(model);
                return PartialView("_List", result);
          //  }
           
        }

        // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            var mobile = HttpContext.Session.GetString("Mobile");
            var email = HttpContext.Session.GetString("Email");
            if(mobile != null) { 
            Kycform kyc = new Kycform();
            kyc.MobileNo = mobile;
            kyc.EmailId = email;
            kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            kyc.BranchList = await _kycformService.GetAllBranchList();
            kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            kyc.ZoneList = await _kycformService.GetAllZoneList();
            kyc.LocalityList = await _kycformService.GetLocalityList(kyc.ZoneId);
            return View(kyc);
            }
            else
            {
                return RedirectToAction("Create", "SignupForm");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Kycform kyc)
        {
            kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            kyc.BranchList = await _kycformService.GetAllBranchList();
            kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            kyc.ZoneList = await _kycformService.GetAllZoneList();
            kyc.LocalityList = await _kycformService.GetLocalityList(kyc.ZoneId);
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
                                if (UserListRoleBasis.Count == 0)
                                {
                                    ViewBag.Message = Alert.Show("No User is available for selected Branch , Without User application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                    return View(kyc);
                                }
                                else
                                {
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
                                //approvalproccess.SendFrom = SiteContext.UserId.ToString();
                                //approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
                                approvalproccess.SendFrom = kyc.Id.ToString();
                                approvalproccess.SendFromProfileId = "0";
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
                                //result = await _kycformService.CreatekycApproval(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                                result = await _kycformService.CreatekycApproval(approvalproccess, kyc.Id); //Create a row in kycapprovalproccess Table

                                #region Insert Into usernotification table Added By Renu 18 June 2021
                                if (result)
                                {
                                    var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidKYCForm").Value);
                                    var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                    Usernotification usernotification = new Usernotification();
                                    var replacement = notificationtemplate.Template.Replace("{proccess name}", "KYC Form").Replace("{from user}", kyc.Name).Replace("{datetime}", DateTime.Now.ToString());
                                    usernotification.Message = replacement;
                                    usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidKYCForm").Value);
                                    usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                    usernotification.ServiceId = approvalproccess.ServiceId;
                                    usernotification.SendFrom = approvalproccess.SendFrom;
                                    usernotification.SendTo = approvalproccess.SendTo;
                                    //result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                                    result = await _userNotificationService.Create(usernotification, kyc.Id);
                                }
                                #endregion
                            }

                            break;
                        }
                    }

                    #endregion

                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                     
                    var list = await _kycformService.GetAllKycform();
                    return View("Index", list);
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

       
        //[AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _kycformService.FetchSingleResult(id);
            Data.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            Data.BranchList = await _kycformService.GetAllBranchList();
            Data.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            Data.ZoneList = await _kycformService.GetAllZoneList();
          //  Data.LocalityList = await _kycformService.GetLocalityList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Kycform kyc)
        {
            var Data = await _kycformService.FetchSingleResult(id);
            kyc.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            kyc.BranchList = await _kycformService.GetAllBranchList();
            kyc.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            kyc.ZoneList = await _kycformService.GetAllZoneList();
          //  kyc.LocalityList = await _kycformService.GetLocalityList();
            string AadharDoc = _configuration.GetSection("FilePaths:KycFiles:AadharDocument").Value.ToString();
            string LetterDoc = _configuration.GetSection("FilePaths:KycFiles:LetterDocument").Value.ToString();
            string ApplicantDoc = _configuration.GetSection("FilePaths:KycFiles:ApplicantDocument").Value.ToString();


            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();

                if (kyc.Aadhar != null)
                {
                    kyc.AadhaarNoPath = fileHelper.SaveFile1(AadharDoc, kyc.Aadhar);
                }
                else
                {
                    kyc.AadhaarNoPath = Data.AadhaarNoPath;
                }

                if (kyc.Letter != null)
                {
                    kyc.LetterPath = fileHelper.SaveFile1(LetterDoc, kyc.Letter);
                }
                else
                {
                    kyc.LetterPath = Data.LetterPath;
                }
                if (kyc.ApplicantPan != null)
                {
                    kyc.AadhaarPanapplicantPath = fileHelper.SaveFile1(ApplicantDoc, kyc.ApplicantPan);
                }
                else
                {
                    kyc.AadhaarPanapplicantPath = Data.AadhaarPanapplicantPath;
                }

                kyc.IsActive = 1;
                kyc.ModifiedBy = SiteContext.UserId;
                var result = await _kycformService.Update(id, kyc);
                    if (result == true)
                    {

                    Kycapprovalproccess approvalproccess = new Kycapprovalproccess();
                    var DataFlow = await dataAsync();

                    #region Resubmitting form Approval Proccess At last level start Added by ishu  27 july 2021
                   
                    var workflowtemplatedata = await _kycformService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProccessGuidKYCForm").Value));

                    var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);

                    for (int i = 0; i < DataFlow.Count; i++)
                    {
                        if (!DataFlow[i].parameterSkip)
                        {
                            
                                    var CheckLastUserForRevert = await _approvalproccessService.KycUserResubmitForApproval((_configuration.GetSection("workflowProccessGuidKYCForm").Value), kyc.Id, Convert.ToInt32(DataFlow[i].parameterLevel));
                                    approvalproccess.SendTo = CheckLastUserForRevert.SendTo;
                                    // approvalproccess.Level = Convert.ToInt32(DataFlow[d].parameterLevel);
                                    approvalproccess.Level = 3;
                          

                            kyc.ApprovedStatus = ApprovalStatus.Id;
                            kyc.PendingAt = approvalproccess.SendTo;
                            result = await _kycformService.UpdateBeforeApproval(kyc.Id, kyc);  //Update kycform Table details 
                            if (result)
                            {


                                /* Update last record pending status in Approval Process Table*/
                                var ApprovalProccessBackId = _approvalproccessService.GetPreviouskycApprovalId((_configuration.GetSection("workflowProccessGuidKYCForm").Value), kyc.Id);                                
                                approvalproccess.PendingStatus = 0;
                                result = await _approvalproccessService.UpdatePreviouskycApprovalProccess(ApprovalProccessBackId, approvalproccess, kyc.Id);
                                /*end of Code*/


                                approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                approvalproccess.ProcessGuid = (_configuration.GetSection("workflowProccessGuidKYCForm").Value);
                                approvalproccess.ServiceId = kyc.Id;
                               
                                approvalproccess.SendFrom = kyc.Id.ToString();
                                approvalproccess.SendFromProfileId = "0";
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
                                //approvalproccess.Level = i + 1;
                                approvalproccess.Version = workflowtemplatedata.Version;
                                approvalproccess.Remarks = "Record Resubmitted by "+kyc.Name;///May be Uncomment
                                //result = await _kycformService.CreatekycApproval(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                                result = await _kycformService.CreatekycApproval(approvalproccess, kyc.Id); //Create a row in approvalproccess Table

                                #region Insert Into usernotification table Added By ishu 18 June 2021
                                if (result)
                                {
                                    var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidKYCForm").Value);
                                    var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                    Usernotification usernotification = new Usernotification();
                                    var replacement = notificationtemplate.Template.Replace("{proccess name}", "KYC Form").Replace("{from user}", kyc.Name).Replace("{datetime}", DateTime.Now.ToString());
                                    usernotification.Message = replacement;
                                    usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidKYCForm").Value);
                                    usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                    usernotification.ServiceId = approvalproccess.ServiceId;
                                    usernotification.SendFrom = approvalproccess.SendFrom;
                                    usernotification.SendTo = approvalproccess.SendTo;
                                    //result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                                    result = await _userNotificationService.Create(usernotification, kyc.Id);
                                }
                                #endregion
                            }

                            break;
                        }
                    }

                    #endregion

                    ViewBag.Message = Alert.Show(Messages.UpdateAndApprovalRecordSuccess, "", AlertType.Success);

                        var list = await _kycformService.GetAllKycform();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(kyc);

                    }
               
            }
            return View(kyc);
        }

        //[AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _kycformService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _kycformService.GetAllKycform();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _kycformService.GetAllKycform();
                return View("Index", result1);
            }
        }

        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _kycformService.FetchSingleResult(id);
            Data.LeasetypeList = await _kycformService.GetAllLeasetypeList();
            Data.BranchList = await _kycformService.GetAllBranchList();
            Data.PropertyTypeList = await _kycformService.GetAllPropertyTypeList();
            Data.ZoneList = await _kycformService.GetAllZoneList();
           
            Data.LocalityList = await _kycformService.GetLocalityList(Data.ZoneId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        //***************** Download Files  ********************
       
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

        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? zoneid)
        {
            zoneid = zoneid ?? 0;
            return Json(await _kycformService.GetLocalityList(Convert.ToInt32(zoneid)));
        }

    }
}
