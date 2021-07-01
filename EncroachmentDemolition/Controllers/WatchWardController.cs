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
using EncroachmentDemolition.Filters;
using Core.Enum;
using Dto.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EncroachmentDemolition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.IApplicationService;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace EncroachmentDemolition.Controllers
{
    public class WatchWardController : BaseController
    {
        private readonly IWatchandwardService _watchandwardService;
        private readonly IPropertyRegistrationService _propertyregistrationService;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserProfileService _userProfileService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserNotificationService _userNotificationService;

        public IConfiguration _configuration;
        string targetPhotoPathLayout = "";
        string targetReportfilePathLayout = "";
        string targetPathLayout = "";
        public WatchWardController(IWatchandwardService watchandwardService, IApprovalProccessService approvalproccessService,
            IWorkflowTemplateService workflowtemplateService, IConfiguration configuration,
            IPropertyRegistrationService propertyregistrationService, IUserProfileService userProfileService,
            IHostingEnvironment hostingEnvironment, IUserNotificationService userNotificationService)
        {
            _workflowtemplateService = workflowtemplateService;
            _propertyregistrationService = propertyregistrationService;
            _watchandwardService = watchandwardService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
            _userProfileService = userProfileService;
            _hostingEnvironment = hostingEnvironment;
            _userNotificationService = userNotificationService;
            targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
            targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();

        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] WatchandwardSearchDto model)
        {
            var result = await _watchandwardService.GetPagedWatchandward(model, SiteContext.ZoneId ?? 0);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Watchandward watchandward = new Watchandward();
            watchandward.IsActive = 1;
            watchandward.LocalityList = await _watchandwardService.GetAllLocality();
            watchandward.KhasraList = await _watchandwardService.GetAllKhasra();
            watchandward.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();
            return View(watchandward);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Watchandward watchandward)
        {
            try
            {
                watchandward.LocalityList = await _watchandwardService.GetAllLocality();
                watchandward.KhasraList = await _watchandwardService.GetAllKhasra();
                watchandward.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();
                watchandward.PrimaryListNoNavigation = await _watchandwardService.FetchSingleResultOnPrimaryList(Convert.ToInt32(watchandward.PrimaryListNo));
                Random r = new Random();
                int num = r.Next();
                watchandward.RefNo = DateTime.Now.Year.ToString() + watchandward.PrimaryListNoNavigation.Zone.Code + num.ToString();
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
                                    ViewBag.Message = Alert.Show("Your Zone is not available , Without zone application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                    return View(watchandward);
                                }

                                watchandward.ApprovalZoneId = SiteContext.ZoneId;
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

                    if (watchandward.Encroachment == 0)
                        watchandward.Encroachment = 0;
                    else if (watchandward.Encroachment == 1)
                        watchandward.Encroachment = 1;
                    var result = await _watchandwardService.Create(watchandward);

                    if (result)
                    {
                        FileHelper fileHelper = new FileHelper();
                        ///for photo file:
                        if (watchandward.Photo != null && watchandward.Photo.Count > 0)
                        {
                            List<Watchandwardphotofiledetails> watchandwardphotofiledetails = new List<Watchandwardphotofiledetails>();
                            for (int i = 0; i < watchandward.Photo.Count; i++)
                            {
                                string FilePath = fileHelper.SaveFile1(targetPhotoPathLayout, watchandward.Photo[i]);
                                GetLattLongDetailsDto dto = GetLattLongDetails(targetPhotoPathLayout + FilePath, watchandward.Latitude, watchandward.Longitude);
                                var LattitudeValue = dto.Lattitude;
                                watchandward.Latitude = LattitudeValue;
                                var LongitudeValue = dto.Longitude;
                                watchandward.Longitude = LongitudeValue;
                                var lattlongurlvalue = dto.Url;
                                watchandwardphotofiledetails.Add(new Watchandwardphotofiledetails
                                {
                                    WatchAndWardId = watchandward.Id,
                                    PhotoFilePath = FilePath,
                                    Lattitude = watchandward.Latitude,
                                    Longitude = watchandward.Longitude,
                                    LattLongUrl = lattlongurlvalue

                                });
                            }
                            foreach (var item in watchandwardphotofiledetails)
                            {
                                result = await _watchandwardService.SaveWatchandwardphotofiledetails(item);
                            }
                        }
                        //for report file:
                        if (watchandward.ReportFile != null && watchandward.ReportFile.Count > 0)
                        {
                            List<Watchandwardreportfiledetails> watchandwardreportfiledetails = new List<Watchandwardreportfiledetails>();
                            for (int i = 0; i < watchandward.Photo.Count; i++)
                            {
                                string FilePath = fileHelper.SaveFile1(targetReportfilePathLayout, watchandward.Photo[i]);
                                watchandwardreportfiledetails.Add(new Watchandwardreportfiledetails
                                {
                                    WatchAndWardId = watchandward.Id,
                                    ReportFilePath = FilePath
                                });
                            }
                            foreach (var item in watchandwardreportfiledetails)
                            {
                                result = await _watchandwardService.SaveWatchandwardreportfiledetails(item);
                            }
                        }

                        if (result)
                        {
                            #region Approval Proccess At 1st level start Added by Renu 21 April 2021
                            var workflowtemplatedata = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidWatchWard").Value));
                            var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);
                            //var DataFlow = await dataAsync();
                            for (int i = 0; i < DataFlow.Count; i++)
                            {
                                if (!DataFlow[i].parameterSkip)
                                {
                                    watchandward.ApprovedStatus = ApprovalStatus.Id;
                                    watchandward.PendingAt = approvalproccess.SendTo;
                                    result = await _watchandwardService.UpdateBeforeApproval(watchandward.Id, watchandward);  //Update Table details 
                                    if (result)
                                    {
                                        approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                        approvalproccess.ProcessGuid = (_configuration.GetSection("workflowPreccessGuidWatchWard").Value);
                                        approvalproccess.ServiceId = watchandward.Id;
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
                                            var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidWatchWard").Value);
                                            var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                            Usernotification usernotification = new Usernotification();
                                            var replacement = notificationtemplate.Template.Replace("{proccess name}", "Watch & Ward").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                            usernotification.Message = replacement;
                                            usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidWatchWard").Value);
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

                            #region Approval Proccess  Mail Generation Added by Renu 07 May 2021
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
                                bodyDTO.ApplicationName = "Watch & Ward Application";
                                bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                                bodyDTO.SenderName = senderUser.User.Name;
                                bodyDTO.Link = linkhref;
                                bodyDTO.AppRefNo = watchandward.RefNo;
                                bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                                bodyDTO.Remarks = approvalproccess.Remarks;
                                bodyDTO.path = path;
                                string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                                #endregion

                                // sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                                #region Common Mail Genration
                                SentMailGenerationDto maildto = new SentMailGenerationDto();
                                maildto.strMailSubject = "Pending Watch & Ward Application Approval Request Details ";
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
                            var result1 = await _watchandwardService.GetAllWatchandward();
                            return View("Index", result1);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(watchandward);
                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(watchandward);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(watchandward);
                }
            }
            catch (Exception ex)
            {
                #region Roll Back of Transaction Added by Renu 26 April  2021 
                var deleteResult = false;
                if (watchandward.Id != 0)
                {
                    deleteResult = await _userNotificationService.RollBackEntry((_configuration.GetSection("workflowPreccessGuidWatchWard").Value), watchandward.Id);
                    deleteResult = await _approvalproccessService.RollBackEntry((_configuration.GetSection("workflowPreccessGuidWatchWard").Value), watchandward.Id);
                    deleteResult = await _watchandwardService.RollBackEntryPhoto(watchandward.Id);
                    deleteResult = await _watchandwardService.RollBackEntryReport(watchandward.Id);
                    deleteResult = await _watchandwardService.RollBackEntry(watchandward.Id);
                }
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(watchandward);
                #endregion
            }

        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _watchandwardService.FetchSingleResult(id);


            Data.LocalityList = await _watchandwardService.GetAllLocality();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Watchandward watchandward)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            Data.LocalityList = await _watchandwardService.GetAllLocality();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();

            if (ModelState.IsValid)
            {
                if (watchandward.Encroachment == 0)
                    watchandward.Encroachment = 0;
                else if (watchandward.Encroachment == 1)
                    watchandward.Encroachment = 1;
                var result = await _watchandwardService.Update(id, watchandward);
                if (result)
                {
                    FileHelper fileHelper = new FileHelper();


                    //for photo file:

                    if (watchandward.Photo != null && watchandward.Photo.Count > 0)
                    {
                        List<Watchandwardphotofiledetails> watchandwardphotofiledetails = new List<Watchandwardphotofiledetails>();
                        result = await _watchandwardService.DeleteWatchandwardphotofiledetails(id);
                        for (int i = 0; i < watchandward.Photo.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile1(targetPhotoPathLayout, watchandward.Photo[i]);
                            GetLattLongDetailsDto dto = GetLattLongDetails(FilePath, watchandward.Latitude, watchandward.Longitude);
                            var LattitudeValue = dto.Lattitude;
                            watchandward.Latitude = LattitudeValue;
                            var LongitudeValue = dto.Longitude;
                            watchandward.Longitude = LongitudeValue;
                            var lattlongurlvalue = dto.Url;
                            watchandwardphotofiledetails.Add(new Watchandwardphotofiledetails
                            {
                                WatchAndWardId = watchandward.Id,
                                PhotoFilePath = FilePath,
                                Lattitude = watchandward.Latitude,
                                Longitude = watchandward.Longitude,
                                LattLongUrl = lattlongurlvalue

                            });
                        }
                        foreach (var item in watchandwardphotofiledetails)
                        {
                            result = await _watchandwardService.SaveWatchandwardphotofiledetails(item);
                        }
                    }

                    //for report file:

                    if (watchandward.ReportFile != null && watchandward.ReportFile.Count > 0)
                    {
                        List<Watchandwardreportfiledetails> watchandwardreportfiledetails = new List<Watchandwardreportfiledetails>();
                        result = await _watchandwardService.DeleteWatchandwardreportfiledetails(id);
                        for (int i = 0; i < watchandward.Photo.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile1(targetReportfilePathLayout, watchandward.Photo[i]);
                            watchandwardreportfiledetails.Add(new Watchandwardreportfiledetails
                            {
                                WatchAndWardId = watchandward.Id,
                                ReportFilePath = FilePath
                            });
                        }
                        foreach (var item in watchandwardreportfiledetails)
                        {
                            result = await _watchandwardService.SaveWatchandwardreportfiledetails(item);
                        }
                    }

                    if (result)
                    {

                        ViewBag.Message = Alert.Show(Messages.UpdateAndApprovalRecordSuccess, "", AlertType.Success);
                        var result1 = await _watchandwardService.GetAllWatchandward();

                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(watchandward);
                    }

                }
                else
                {
                    return View(watchandward);
                }
            }
            else
            {
                return View(watchandward);
            }

        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            Data.LocalityList = await _watchandwardService.GetAllLocality();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _watchandwardService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var result1 = await _watchandwardService.GetAllWatchandward();
            return View("Index", result1);
        }


        //***to download photo file ***
        public async Task<IActionResult> DownloadPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string filename = targetPhotoPathLayout + Data.PhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        //***to download report file ***
        public async Task<IActionResult> DownloadReportFile(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardreportfiledetails Data = await _watchandwardService.GetWatchandwardreportfiledetails(Id);
            string filename = targetReportfilePathLayout + Data.ReportFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public IActionResult WatchWardApproval()
        {
            return View();
        }

        public IActionResult WatchWardApprovalCreate()
        {
            return View();
        }

        [HttpGet]
        public GetLattLongDetailsDto GetLattLongDetails(string path, string Latt, string Long)
        {
            GetLattLongDetailsDto dtodata = new GetLattLongDetailsDto();
            double? latitdue = null;
            double? longitude = null;
            string url = null;
            if (path != null)
            {
                Bitmap bmp = new Bitmap(path);
                foreach (PropertyItem propItem in bmp.PropertyItems)
                {
                    switch (propItem.Type)
                    {
                        case 5:
                            if (propItem.Id == 2) // Latitude Array
                            {
                                latitdue = GetLatitudeAndLongitude(propItem);
                            }
                            if (propItem.Id == 4) //Longitude Array
                            {
                                longitude = GetLatitudeAndLongitude(propItem);
                            }
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(latitdue.ToString()) && !string.IsNullOrEmpty(longitude.ToString()))
                {
                    url = $"https://www.google.com/maps/place/{latitdue},{longitude}";
                }
                else
                {
                    ViewBag.Message = Alert.Show("Uploaded Image does not contain any geo location.please enter your request location in below textbox", "", AlertType.Error);

                }
                bmp.Dispose();
            }
            dtodata.Lattitude = latitdue.ToString();
            dtodata.Longitude = longitude.ToString();
            dtodata.Url = url;

            return dtodata;

        }

        private static double? GetLatitudeAndLongitude(PropertyItem propItem)
        {
            try
            {
                uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
                uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
                uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
                uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
                uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
                uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
                return (Convert.ToDouble(degreesNumerator) / Convert.ToDouble(degreesDenominator)) + (Convert.ToDouble(Convert.ToDouble(minutesNumerator) / Convert.ToDouble(minutesDenominator)) / 60) +
                       (Convert.ToDouble((Convert.ToDouble(secondsNumerator) / Convert.ToDouble(secondsDenominator)) / 3600));
            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOtherDetails(int? propertyId)
        {
            propertyId = propertyId ?? 0;
            var data = await _watchandwardService.FetchSingleResultOnPrimaryList(Convert.ToInt32(propertyId));
            return Json(data);
        }

        [HttpGet]
        public async Task<JsonResult> GetLocalityList()
        {
            return Json(await _watchandwardService.GetAllLocality());
        }

        public async Task<FileResult> ViewDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string path = targetPhotoPathLayout + Data.PhotoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        #region View Details of Property Inventory

        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownList();
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            propertyregistration.TakenOverDepartmentList = await _propertyregistrationService.GetTakenDepartmentDropDownList();
            propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetHandedDepartmentDropDownList();

        }
        public async Task<PartialViewResult> InventoryView(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            if (Data != null)
            {
                ViewBag.LayoutDocView = Data.LayoutFilePath;
                ViewBag.GeoDocView = Data.GeoFilePath;
                ViewBag.TakenOverDocView = Data.TakenOverFilePath;
                ViewBag.HandedOverDocView = Data.HandedOverFilePath;
                ViewBag.DisposalTypeDocView = Data.DisposalTypeFilePath;
                await BindDropDown(Data);

                Data.ZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
                Data.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(Data.ZoneId);
                Data.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
                Data.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
                Data.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
                Data.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
                Data.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            }
            return PartialView("_InventoryView", Data);
        }

        #endregion

        #region Fetch workflow data for approval prrocess Added by Renu 26 Nov 2020
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidWatchWard").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion





        public async Task<IActionResult> WatchWardList()
        {
            var result = await _watchandwardService.GetAllWatchandward();
            List<WatchWardListDto> data = new List<WatchWardListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new WatchWardListDto()
                    {
                        Id = result[i].Id,
                        Date = Convert.ToDateTime(result[i].Date).ToString("dd-MMM-yyyy") == null ? "" : Convert.ToDateTime(result[i].Date).ToString("dd-MMM-yyyy"),

                        Loaclity = result[i].PrimaryListNoNavigation.LocalityId == null ? "" : result[i].PrimaryListNoNavigation.Locality.Name == null ? "" : result[i].PrimaryListNoNavigation.Locality.Name,
                        KhasraNo = result[i].PrimaryListNoNavigation.KhasraNo == null ? "" : result[i].PrimaryListNoNavigation.KhasraNo.ToString(),
                        PrimaryListNo = result[i].PrimaryListNoNavigation.PrimaryListNo == null ? "" : result[i].PrimaryListNoNavigation.PrimaryListNo,
                        StatusOnGround = result[i].StatusOnGround.ToString(),
                        //IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
