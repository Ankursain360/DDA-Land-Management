using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Dto.Master;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System.IO;
using System.Text.Json;
using DamagePayee.Filters;
using Core.Enum;
using Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace DamagePayee.Controllers
{
    public class DamagePayeeApprovalController : BaseController
    {
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;
        private readonly IDamagePayeeApprovalService _damagePayeeApprovalService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly ISelfAssessmentDamageService _selfAssessmentDamageService;
        private readonly IProccessWorkflowService _proccessWorkflowService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserProfileService _userProfileService;
        private readonly IHostingEnvironment _hostingEnvironment;
        string ApprovalDocumentPath = "";
        public DamagePayeeApprovalController(IDamagePayeeApprovalService damagePayeeApprovalService,
            IDamagepayeeregisterService damagepayeeregisterService, IConfiguration configuration,
            IWorkflowTemplateService workflowtemplateService, ISelfAssessmentDamageService selfAssessmentDamageService,
            IProccessWorkflowService proccessWorkflowService, IApprovalProccessService approvalproccessService,
            IUserProfileService userProfileService, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _damagePayeeApprovalService = damagePayeeApprovalService;
            _damagepayeeregisterService = damagepayeeregisterService;
            _workflowtemplateService = workflowtemplateService;
            _selfAssessmentDamageService = selfAssessmentDamageService;
            _proccessWorkflowService = proccessWorkflowService;
            _approvalproccessService = approvalproccessService;
            _userProfileService = userProfileService;
            _hostingEnvironment = hostingEnvironment;
            ApprovalDocumentPath = _configuration.GetSection("FilePaths:DamagePayeeFiles:ApprovalDocumentPath").Value.ToString();

        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Damagepayeeregister data = new Damagepayeeregister();
            var dropdownValue = await GetApprovalStatusDropdownListAtIndex();
            int[] actions = Array.ConvertAll(dropdownValue, int.Parse);
            data.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(actions.Distinct().ToArray());

            var Msg = TempData["Message"] as string;
            if (Msg != null)
                ViewBag.Message = Msg;
            return View(data);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagepayeeRegisterApprovalDto model)
        {
            var result = await _damagePayeeApprovalService.GetPagedDamageForApproval(model, SiteContext.UserId);
            ViewBag.IsApproved = model.StatusId;
            return PartialView("_List", result);
        }

        async Task BindDropDown(Damagepayeeregister damagepayeeregistertemp)
        {
            damagepayeeregistertemp.LocalityList = await _damagepayeeregisterService.GetLocalityList();
            damagepayeeregistertemp.DistrictList = await _damagepayeeregisterService.GetDistrictList();
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);
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
        public async Task<IActionResult> Create(int id, Damagepayeeregister damagepayeeregister)
        {
            var result = false;
            var IsApplicationPendingAtUserEnd = await _damagePayeeApprovalService.IsApplicationPendingAtUserEnd(id, SiteContext.UserId);
            if (IsApplicationPendingAtUserEnd)
            {
                var Data = await _damagePayeeApprovalService.FetchSingleResult(id);
                FileHelper fileHelper = new FileHelper();
                var Msgddl = damagepayeeregister.ApprovalStatus;
                #region Approval Proccess At Further level start Added by Renu 16 march 2021
                var FirstApprovalProcessData = await _approvalproccessService.FirstApprovalProcessData((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value), damagepayeeregister.Id);
                var ApprovalProccessBackId = _approvalproccessService.GetPreviousApprovalId((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value), damagepayeeregister.Id);
                var ApprovalProcessBackData = await _approvalproccessService.FetchApprovalProcessDocumentDetails(ApprovalProccessBackId);
                var checkLastApprovalStatuscode = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalProcessBackData.Status));

                var DataFlow = await DataAsync(ApprovalProcessBackData.Version);

                Approvalproccess approvalproccess = new Approvalproccess();

                /*Check if zonewise then aprovee user must have zoneid*/
                if (damagepayeeregister.ApprovalStatusCode == ((int)ApprovalActionStatus.Forward) && checkLastApprovalStatuscode.StatusCode != ((int)ApprovalActionStatus.QueryForward))
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
                                                await BindApprovalStatusDropdown(damagepayeeregister);
                                                ViewBag.Message = Alert.Show("Your Zone is not available , Without zone application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                                return View(damagepayeeregister);
                                            }

                                            damagepayeeregister.ApprovalZoneId = SiteContext.ZoneId;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (ApprovalProcessBackData.Level == FirstApprovalProcessData.Level && damagepayeeregister.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))//check if revert available at first level
                {
                    result = false;
                    ViewBag.Items = await _userProfileService.GetRole();
                    await BindApprovalStatusDropdown(damagepayeeregister);
                    ViewBag.Message = Alert.Show("Application cannot be Reverted at First Level", "", AlertType.Warning);
                    return View(damagepayeeregister);
                }
                else
                {
                    /* Update last record pending status in Approval Process Table*/
                    result = true;
                    approvalproccess.PendingStatus = 0;
                    result = await _approvalproccessService.UpdatePreviousApprovalProccess(ApprovalProccessBackId, approvalproccess, SiteContext.UserId);

                    /*Now New row added in Approval process*/
                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                    approvalproccess.ProcessGuid = (_configuration.GetSection("workflowPreccessGuidDamagePayee").Value);
                    approvalproccess.ServiceId = damagepayeeregister.Id;
                    approvalproccess.SendFrom = SiteContext.UserId.ToString();
                    approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
                    approvalproccess.PendingStatus = 1;
                    approvalproccess.Remarks = damagepayeeregister.ApprovalRemarks; ///May be comment
                    approvalproccess.Status = Convert.ToInt32(damagepayeeregister.ApprovalStatus);
                    approvalproccess.Version = ApprovalProcessBackData.Version;
                    approvalproccess.DocumentName = damagepayeeregister.ApprovalDocument == null ? null : fileHelper.SaveFile1(ApprovalDocumentPath, damagepayeeregister.ApprovalDocument);


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
                            damagepayeeregister.ApprovedStatus = Convert.ToInt32(damagepayeeregister.ApprovalStatus);
                            damagepayeeregister.PendingAt = ApprovalProcessBackData.SendFrom;
                            result = await _damagepayeeregisterService.UpdateBeforeApproval(damagepayeeregister.Id, damagepayeeregister);  //Update Table details 
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
                                        if (damagepayeeregister.ApprovalStatusCode == ((int)ApprovalActionStatus.QueryForward))
                                        {
                                            approvalproccess.Level = ApprovalProcessBackData.Level;
                                            approvalproccess.SendTo = damagepayeeregister.ApprovalUserId.ToString();
                                        }
                                        else if (damagepayeeregister.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))
                                        {
                                            /*Check previous level for revert */
                                            for (int d = i - 1; d >= 0; d--)
                                            {
                                                if (!DataFlow[d].parameterSkip)
                                                {
                                                    var CheckLastUserForRevert = await _approvalproccessService.CheckLastUserForRevert((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value), damagepayeeregister.Id, Convert.ToInt32(DataFlow[i].parameterLevel));
                                                    approvalproccess.SendTo = CheckLastUserForRevert.SendFrom;
                                                    approvalproccess.Level = Convert.ToInt32(DataFlow[d].parameterLevel);
                                                    break;
                                                }
                                            }
                                        }
                                        else if (damagepayeeregister.ApprovalStatusCode == ((int)ApprovalActionStatus.Rejected))
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
                                                approvalproccess.SendTo = damagepayeeregister.ApprovalUserId.ToString();

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
                                        else if (approvalproccess.SendTo == null && (damagepayeeregister.ApprovalStatusCode != ((int)ApprovalActionStatus.Rejected) && damagepayeeregister.ApprovalStatusCode != ((int)ApprovalActionStatus.Approved)))
                                        {
                                            ViewBag.Items = await _userProfileService.GetRole();
                                            await BindApprovalStatusDropdown(damagepayeeregister);
                                            ViewBag.Message = Alert.Show("No user found at the next approval level, In this case, the system is unable to process your request. Please contact to the system administrator.", "", AlertType.Warning);
                                            return View(damagepayeeregister);
                                        }
                                        #endregion

                                        result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table

                                        if (result)
                                        {
                                            if (damagepayeeregister.ApprovalStatusCode == ((int)ApprovalActionStatus.QueryForward))
                                            {
                                                damagepayeeregister.ApprovedStatus = Convert.ToInt32(damagepayeeregister.ApprovalStatus);
                                                damagepayeeregister.PendingAt = approvalproccess.SendTo;
                                            }
                                            else if (damagepayeeregister.ApprovalStatusCode == ((int)ApprovalActionStatus.Revert))
                                            {
                                                damagepayeeregister.ApprovedStatus = Convert.ToInt32(damagepayeeregister.ApprovalStatus);
                                                damagepayeeregister.PendingAt = approvalproccess.SendTo;
                                            }
                                            else if (damagepayeeregister.ApprovalStatusCode == ((int)ApprovalActionStatus.Rejected))
                                            {
                                                damagepayeeregister.ApprovedStatus = Convert.ToInt32(damagepayeeregister.ApprovalStatus);
                                                damagepayeeregister.PendingAt = "0";
                                            }
                                            else
                                            {
                                                if (i == DataFlow.Count - 1)
                                                {
                                                    damagepayeeregister.ApprovedStatus = Convert.ToInt32(damagepayeeregister.ApprovalStatus);
                                                    damagepayeeregister.PendingAt = "0";
                                                }
                                                else
                                                {
                                                    damagepayeeregister.ApprovedStatus = Convert.ToInt32(damagepayeeregister.ApprovalStatus);
                                                    damagepayeeregister.PendingAt = approvalproccess.SendTo;
                                                }
                                            }
                                            result = await _damagepayeeregisterService.UpdateBeforeApproval(damagepayeeregister.Id, damagepayeeregister);  //Update Table details 
                                        }
                                    }
                                    break;




                                }
                            }
                        }

                    }
                    var sendMailResult = false;

                    var DataApprovalSatatusMsg = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(damagepayeeregister.ApprovalStatus));

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
                        bodyDTO.ApplicationName = "Damage Payee Register Application";
                        bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                        bodyDTO.SenderName = senderUser.User.Name;
                        bodyDTO.Link = linkhref;
                        bodyDTO.AppRefNo = damagepayeeregister.RefNo;
                        bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                        bodyDTO.Remarks = damagepayeeregister.ApprovalRemarks;
                        bodyDTO.path = path;
                        string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                        #endregion

                        #region Common Mail Genration
                        SentMailGenerationDto maildto = new SentMailGenerationDto();
                        maildto.strMailSubject = "Pending Damage Payee Register Application Approval Request Details ";
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


                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Items = await _userProfileService.GetRole();
                        await BindApprovalStatusDropdown(damagepayeeregister);
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(damagepayeeregister);
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


        private async Task<bool> CreateAprrovedRecordsinActualTable(Damagepayeeregister damagepayeeregistertemp)
        {
            var result = false;
            if (damagepayeeregistertemp != null)
            {
                Damagepayeeregister model = new Damagepayeeregister();
                damagepayeeregistertemp = await _damagepayeeregisterService.FetchSingleResult(damagepayeeregistertemp.Id);
                result = await _damagepayeeregisterService.CreateApprovedDamagepayeeRegister(damagepayeeregistertemp, model);

                List<Damagepayeepersonelinfo> damagepayeepersonelinfo = new List<Damagepayeepersonelinfo>();
                var data = damagepayeeregistertemp.Damagepayeepersonelinfo.ToList();
                for (int j = 0; j < data.Count; j++)
                {
                    damagepayeepersonelinfo.Add(new Damagepayeepersonelinfo
                    {
                        Name = data[j].Name,
                        FatherName = data[j].FatherName,
                        Gender = data[j].Gender,
                        Address = data[j].Address,
                        MobileNo = data[j].MobileNo,
                        EmailId = data[j].EmailId,
                        AadharNo = data[j].AadharNo,
                        PanNo = data[j].PanNo,
                        AadharNoFilePath = data[j].AadharNoFilePath,
                        PanNoFilePath = data[j].PanNoFilePath,
                        PhotographPath = data[j].PhotographPath,
                        SignaturePath = data[j].SignaturePath,
                        DamagePayeeRegisterTempId = model.Id,
                        CreatedBy = SiteContext.UserId
                    });
                }
                result = await _damagepayeeregisterService.SavePersonelInfo(damagepayeepersonelinfo);

                List<Allottetype> allottetype = new List<Allottetype>();
                var allottetmp = damagepayeeregistertemp.Allottetype.ToList();
                for (int k = 0; k < allottetmp.Count; k++)
                {
                    allottetype.Add(new Allottetype
                    {
                        Name = allottetmp[k].Name,
                        FatherName = allottetmp[k].FatherName,
                        Date = allottetmp[k].Date,
                        AtsgpadocumentPath = allottetmp[k].AtsgpadocumentPath,
                        DamagePayeeRegisterTempId = model.Id,
                        CreatedBy = SiteContext.UserId
                    });
                }
                result = await _damagepayeeregisterService.SaveAllotteType(allottetype);

                List<Damagepaymenthistory> damagepaymenthistory = new List<Damagepaymenthistory>();
                var historytmp = damagepayeeregistertemp.Damagepaymenthistory.ToList();
                for (int m = 0; m < historytmp.Count; m++)
                {
                    damagepaymenthistory.Add(new Damagepaymenthistory
                    {
                        Name = historytmp[m].Name,
                        RecieptNo = historytmp[m].RecieptNo,
                        PaymentMode = historytmp[m].PaymentMode,
                        PaymentDate = historytmp[m].PaymentDate,
                        Amount = historytmp[m].Amount,
                        RecieptDocumentPath = historytmp[m].RecieptDocumentPath,
                        DamagePayeeRegisterTempId = model.Id,
                        CreatedBy = SiteContext.UserId
                    });
                }

                result = await _damagepayeeregisterService.SavePaymentHistory(damagepaymenthistory);
            }
            return result;
        }

        #region Damage Payee Register  Details
        public async Task<PartialViewResult> DamagePayeeRegisterView(int id)
        {
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);
            await BindDropDown(Data);
            return PartialView("_DamagePayeeRegister", Data);
        }
        public async Task<JsonResult> GetDetailspersonelinfotemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _selfAssessmentDamageService.GetPersonalInfoTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.FatherName,
                x.Gender,
                x.Address,
                x.MobileNo,
                x.EmailId,
                x.AadharNoFilePath,
                x.PanNoFilePath,
                x.PhotographPath,
                x.SignaturePath,
                x.AadharNo,
                x.PanNo
            }));
        }
        public async Task<JsonResult> GetDetailsAllottetypetemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _selfAssessmentDamageService.GetAllottetypeTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.FatherName,
                Date = Convert.ToDateTime(x.Date).ToString("yyyy-MM-dd"),
                x.AtsgpadocumentPath
            }));
        }
        public async Task<JsonResult> GetDetailspaymenthistorytemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _selfAssessmentDamageService.GetPaymentHistoryTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.RecieptNo,
                x.PaymentMode,
                PaymentDate = Convert.ToDateTime(x.PaymentDate).ToString("yyyy-MM-dd"),
                x.Amount,
                x.RecieptDocumentPath
            }));
        }

        public async Task<FileResult> ViewPersonelInfoAadharFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.AadharNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPanFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.PanNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.PhotographPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoSignautreFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.SignaturePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewATSGPAFile(int Id)
        {
            FileHelper file = new FileHelper();
            Allottetype Data = await _selfAssessmentDamageService.GetAllotteTypeSingleResult(Id);
            string path = Data.AtsgpadocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewRecieptFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepaymenthistory Data = await _selfAssessmentDamageService.GetPaymentHistorySingleResult(Id);
            string path = Data.RecieptDocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        //******************  download files **************************
        public async Task<IActionResult> DownloadPropertyPhoto(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.PropertyPhotoPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadShowCauseNotice(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.ShowCauseNoticePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadFgform(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.FgformPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadBillfile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.DocumentForFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        #endregion

        #region History Details Only For Approval Page Added By Renu 26 April  2021
        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _approvalproccessService.GetHistoryDetails((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value), id);

            return PartialView("_HistoryDetails", Data);
        }
        #endregion

        #region Fetch workflow data for approval prrocess Added by Renu 16 march 2021
        private async Task<List<TemplateStructure>> DataAsync(string version)
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuidWithVersion((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value), version);
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
        async Task BindApprovalStatusDropdown(Damagepayeeregister Data)
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
            var ApprovalProccessBackId = _approvalproccessService.GetPreviousApprovalId((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value), serviceid);
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
            var DataFlow = await _workflowtemplateService.GetWorkFlowDataOnGuid((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value));

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
            var ApprovalProccessBackId = _approvalproccessService.GetPreviousApprovalId((_configuration.GetSection("workflowPreccessGuidDamagePayee").Value), serviceid);
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
            var Data = await _damagePayeeApprovalService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        #endregion



        public async Task<IActionResult> DamagePayeeRegisterApprovalList()
        {
            var result = await _damagePayeeApprovalService.GetAllDamagePayeeApprovallist();
            List<DamagePayeeApprovalListDto> data = new List<DamagePayeeApprovalListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DamagePayeeApprovalListDto()
                    {
                        Id = result[i].Id,
                        FileNo = result[i].FileNo,
                        TypeOfDamageAssessee = result[i].TypeOfDamageAssessee,
                        PropertyHouseMunicipalNo = result[i].PropertyNo,
                        District = result[i].District.Name,
                        Locality = result[i].Locality == null ? "" : result[i].Locality.Name.ToString(),
                        SubmittedOn = result[i].CreatedDate.ToString("dd MMM yyyy"),
                      
                        ApprovalStatus = result[i].ApprovedStatusNavigation == null ? "" : result[i].ApprovedStatusNavigation.SentStatusName.ToString(),
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }




    }
}
