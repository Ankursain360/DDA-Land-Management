using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Dto.Common;
using EncroachmentDemolition.Filters;
using Core.Enum;
using Service.IApplicationService;
using Dto.Master;
using System.Text;

namespace EncroachmentDemolition.Controllers
{
    public class ComplaintController : BaseController
    {
        private readonly IOnlinecomplaintService _onlinecomplaintService;
        public IConfiguration _configuration;
        string targetPhotoPathLayout = string.Empty;
        string targetReportfilePathLayout = string.Empty;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserProfileService _userProfileService;

        public ComplaintController(IOnlinecomplaintService onlinecomplaintService, IApprovalProccessService approvalproccessService,
            IWorkflowTemplateService workflowtemplateService, IConfiguration configuration,
            IUserProfileService userProfileService)
        {
            _workflowtemplateService = workflowtemplateService;
            _onlinecomplaintService = onlinecomplaintService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
            _userProfileService = userProfileService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] OnlinecomplaintSearchDto model)
        {
            var result = await _onlinecomplaintService.GetPagedOnlinecomplaint(model);
            return PartialView("_List", result);
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            ViewBag.Message = TempData["Message"] as string;
            Onlinecomplaint onlinecomplaint = new Onlinecomplaint();
            onlinecomplaint.IsActive = 1;
            onlinecomplaint.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
            onlinecomplaint.LocationList = await _onlinecomplaintService.GetAllLocation();
            return View(onlinecomplaint);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Onlinecomplaint onlinecomplaint)
        {
            try
            {
                var finalString = (DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond).ToUpper();
                onlinecomplaint.ReferenceNo = "TRN" + finalString;
                onlinecomplaint.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
                onlinecomplaint.LocationList = await _onlinecomplaintService.GetAllLocation();
                onlinecomplaint.IsActive = 1;

                if (ModelState.IsValid)
                {
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
                                    return View(onlinecomplaint);
                                }

                                onlinecomplaint.ApprovalZoneId = SiteContext.ZoneId;
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

                    targetPhotoPathLayout = _configuration.GetSection("FilePaths:OnlineComplaint:Photo").Value.ToString();
                   
                    FileHelper file = new FileHelper();
                    if (onlinecomplaint.Photo != null)
                    {
                        onlinecomplaint.PhotoPath = file.SaveFile(targetPhotoPathLayout, onlinecomplaint.Photo);
                   
                    }
                    var result = await _onlinecomplaintService.Create(onlinecomplaint);
                    if (result)
                    {
                        #region Approval Proccess At 1st level start Added by Renu 21 April 2021
                        var workflowtemplatedata = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidOnlineComplaintId").Value));
                        var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);
                        for (int i = 0; i < DataFlow.Count; i++)
                        {
                            if (!DataFlow[i].parameterSkip)
                            {
                                onlinecomplaint.ApprovedStatus = ApprovalStatus.Id;
                                onlinecomplaint.PendingAt = approvalproccess.SendTo;
                                result = await _onlinecomplaintService.UpdateBeforeApproval(onlinecomplaint.Id, onlinecomplaint);  //Update Table details 
                                if (result)
                                {
                                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                    approvalproccess.ProcessGuid = (_configuration.GetSection("workflowPreccessGuidOnlineComplaintId").Value);
                                    approvalproccess.ServiceId = onlinecomplaint.Id;
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
                                }

                                break;
                            }
                        }

                        #endregion

                    }
                    if (result == true)
                    {
                        string DisplayName = onlinecomplaint.Name.ToString();
                        string EmailID = onlinecomplaint.Email.ToString();

                        string Action = "Dear Requester, <br> Your Request for <b>" + onlinecomplaint.ComplaintType.Name + "</b> has been successfully submitted.Please note your reference No for future reference.<br> Your Ref. number is : <b>" + onlinecomplaint.ReferenceNo + "</b> <br><br><br> Regards,<br>DDA";
                        String Mobile = onlinecomplaint.Contact;
                        SendMailDto mail = new SendMailDto();
                        SendSMSDto SMS = new SendSMSDto();
                        SMS.GenerateSendSMS(Action, Mobile);
                        try
                        {
                            mail.GenerateMailFormatForComplaint(DisplayName, EmailID, Action);


                            TempData["Message"] = Alert.Show(Messages.AddRecordSuccess + " Your Reference No is  " + onlinecomplaint.ReferenceNo, "", AlertType.Success);

                            return Redirect("/Complaint/Create");
                        }
                        catch (Exception ex)
                        {

                            TempData["Message"] = Alert.Show(Messages.AddRecordSuccess + " Your Reference No is " + onlinecomplaint.ReferenceNo + " system is unable to send the complaint details on mail.", "", AlertType.Success);
                            return Redirect("/Complaint/Create");

                        }

                        return View(onlinecomplaint);

                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(onlinecomplaint);
                    }
                }
                else
                {
                    return View(onlinecomplaint);
                }
            }
            catch (Exception ex)
            {
                #region Roll Back of Transaction Added by Renu 26 April  2021 
                var deleteResult = false;
                if (onlinecomplaint.Id != 0)
                {
                    deleteResult = await _approvalproccessService.RollBackEntry((_configuration.GetSection("workflowPreccessGuidWatchWard").Value), onlinecomplaint.Id);                    
                    deleteResult = await _onlinecomplaintService.RollBackEntry(onlinecomplaint.Id);
                }
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(onlinecomplaint);
                #endregion
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _onlinecomplaintService.FetchSingleResult(id);
            Data.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
            Data.LocationList = await _onlinecomplaintService.GetAllLocation();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Onlinecomplaint onlinecomplaint)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _onlinecomplaintService.Update(id, onlinecomplaint);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _onlinecomplaintService.GetAllOnlinecomplaint();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(onlinecomplaint);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(onlinecomplaint);
                }
            }
            else
            {
                return View(onlinecomplaint);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _onlinecomplaintService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _onlinecomplaintService.GetAllOnlinecomplaint();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _onlinecomplaintService.FetchSingleResult(id);
            Data.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
            Data.LocationList = await _onlinecomplaintService.GetAllLocation();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        #region Fetch workflow data for approval prrocess Added by Renu 30 April 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidOnlineComplaintId").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion

    }
}