using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Service.IApplicationService;
using Dto.Master;
using Utility.Helper;
using Libraries.Model.Entity;
using System.IO;
using System.Drawing;
using Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using Core.Enum;
using Microsoft.AspNetCore.Authorization;

namespace Vacant.Land.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WatchWardAPIController : ControllerBase
    {
        private readonly IWatchWardAPIService _watchWardAPIService;
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
        public WatchWardAPIController(IWatchWardAPIService watchWardAPIService,
            IWatchandwardService watchandwardService,
             IApprovalProccessService approvalproccessService,
            IWorkflowTemplateService workflowtemplateService, 
            IPropertyRegistrationService propertyregistrationService, 
            IUserProfileService userProfileService,
            IHostingEnvironment hostingEnvironment, 
            IUserNotificationService userNotificationService,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _watchWardAPIService = watchWardAPIService;
            _watchandwardService = watchandwardService;

            _workflowtemplateService = workflowtemplateService;
            _propertyregistrationService = propertyregistrationService;
           
            _approvalproccessService = approvalproccessService;
            _userProfileService = userProfileService;
            _hostingEnvironment = hostingEnvironment;
            _userNotificationService = userNotificationService;

            targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
            targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();

        }
        [HttpPost]
        [Route("[action]")]
        [Route("api/WatchWardAPI/SaveWatchWardAPIdata")]
        public async Task<IActionResult> SaveWatchWardAPIdata([FromBody] ApiSaveWatchandwardDto dto)
        {
            ApiSaveWatchandwardDtoResponseDetails apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails();
            try
            {       
                if (dto != null)
                {
                    if(dto.Date == null  ||
                       dto.UserId == null ||
                       // dto.LocalityId == null || dto.LocalityId == 0 ||
                        //dto.KhasraId == null || dto.KhasraId == 0||
                        dto.PrimaryListNo == 0 ||
                        dto.Landmark == null || dto.Landmark == "" ||
                        dto.StatusOnGround == null || dto.StatusOnGround == "" 
                      )
                    {
                        List<ApiSaveWatchandwardDto> dtoData = new List<ApiSaveWatchandwardDto>();
                        apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails
                        {
                            responseCode = "205",
                            responseMessage = "Mandatory Fields missing",
                            ApiSaveWatchandwardDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                    FileHelper fileHelper = new FileHelper();
                    var singlepropertyregdetails = await _watchandwardService.FetchSingleResultOnPrimaryList(Convert.ToInt32(dto.PrimaryListNo));
                    Random r = new Random();
                    int num = r.Next();
                    dto.RefNo = DateTime.Now.Year.ToString() + singlepropertyregdetails.Zone.Code + num.ToString();

                    #region Approval Proccess At 1st level Check Initial Before Creating Record  Added by ishu 4 Oct 2021

                    Approvalproccess approvalproccess = new Approvalproccess();
                    var DataFlow = await dataAsync();
                    var user1 = await _watchWardAPIService.GetUserOngivenUserId(dto.UserId);//to get user zone details
                    for (int i = 0; i < DataFlow.Count; i++)
                    {
                        if (!DataFlow[i].parameterSkip)
                        {
                            if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                            {
                                if (user1.ZoneId == null)
                                {
                                    List<ApiSaveWatchandwardDto> dtoData = new List<ApiSaveWatchandwardDto>();
                                    apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails
                                    {
                                        responseCode = "500",
                                        responseMessage = "Your Zone is not available , Without zone application cannot be processed further, Please contact system administrator",
                                        ApiSaveWatchandwardDto = dtoData
                                    };
                                    return NotFound(apiResponseDetails);
                                    //ViewBag.Message = Alert.Show("Your Zone is not available , Without zone application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                    //return View(watchandward);
                                }

                                dto.ApprovalZoneId = user1.ZoneId;
                            }
                            if (DataFlow[i].parameterValue == (_configuration.GetSection("ApprovalRoleType").Value))
                            {
                                for (int j = 0; j < DataFlow[i].parameterName.Count; j++)
                                {
                                    List<UserProfileDto> UserListRoleBasis = null;
                                    if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                                        UserListRoleBasis = await _userProfileService.GetUserOnRoleZoneBasis(Convert.ToInt32(DataFlow[i].parameterName[j]), user1.ZoneId ?? 0);
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
                                            var UserProfile = await _userProfileService.GetUserByIdZone(Convert.ToInt32(MultiUserId), user1.ZoneId ?? 0);
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


                    var data = await _watchWardAPIService.Create(dto);
                    ///for photo file:
                    if (dto.PhotoFileData != null && dto.PhotoFileData.Count > 0)
                    {
                        List<Watchandwardphotofiledetails> photo = new List<Watchandwardphotofiledetails>();
                        for (int i = 0; i < dto.PhotoFileData.Count; i++)
                        {
                            if (dto.PhotoFileData[i] != "" && dto.PhotoFileData != null)
                            {
                                var PhotoPath = Guid.NewGuid().ToString() + ".jpg";
                                if (!Directory.Exists(targetPhotoPathLayout))
                                {
                                    DirectoryInfo directoryInfo = Directory.CreateDirectory(targetPhotoPathLayout);
                                }
                                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(dto.PhotoFileData[i])))
                                {
                                    using (Bitmap bm2 = new Bitmap(ms))
                                    {
                                        bm2.Save(targetPhotoPathLayout + PhotoPath);
                                    }
                                }
                                photo.Add(new Watchandwardphotofiledetails
                                {
                                    WatchAndWardId = dto.Id,
                                    PhotoFilePath = PhotoPath,
                                    Lattitude =dto.Latitude,
                                    Longitude=dto.Longitude,
                                    LattLongUrl ="NA"
                                });
                            }
                        }

                        foreach (var item in photo)
                        {
                            data = await _watchWardAPIService.SaveWatchandwardphotofiledetails(item);
                        }
                    }
                    //for report file:
                    //if (dto.ReportFileData != null && dto.ReportFileData.Count > 0)
                    //{
                    //    List<Watchandwardreportfiledetails> report = new List<Watchandwardreportfiledetails>();
                    //    for (int i = 0; i < dto.ReportFileData.Count; i++)
                    //    {
                    //        string FilePath = fileHelper.SaveFile1(targetReportfilePathLayout, dto.ReportFileData[i]);
                    //        report.Add(new Watchandwardreportfiledetails
                    //        {
                    //            WatchAndWardId = dto.Id,
                    //            ReportFilePath = FilePath
                    //        });
                    //    }
                    //    foreach (var item in report)
                    //    {
                    //      data = await _watchWardAPIService.SaveWatchandwardreportfiledetails(item);
                    //    }
                    //}


                    if (data == true)
                    {

                        #region Approval Proccess At 1st level start Added by ishu 4 Oct 2021
                        var workflowtemplatedata = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidWatchWard").Value));
                            var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);
                            //var DataFlow = await dataAsync();
                            for (int i = 0; i < DataFlow.Count; i++)
                            {
                                if (!DataFlow[i].parameterSkip)
                                {
                                    dto.ApprovedStatus = ApprovalStatus.Id;
                                    dto.PendingAt = approvalproccess.SendTo;
                                    var result = await _watchWardAPIService.UpdateBeforeApproval(dto);  //Update watchward Table details 
                                    if (result)
                                    {
                                        approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                        approvalproccess.ProcessGuid = (_configuration.GetSection("workflowPreccessGuidWatchWard").Value);
                                        approvalproccess.ServiceId = dto.Id;
                                        approvalproccess.SendFrom = dto.UserId.ToString();
                                    //approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
                                    approvalproccess.SendFromProfileId = user1.Id.ToString();
                                  
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
                                        result = await _approvalproccessService.Create(approvalproccess, dto.UserId); //Create a row in approvalproccess Table

                                        #region Insert Into usernotification table Added By ishu oct 2021
                                        if (result == true && approvalproccess.SendTo != null)
                                        {
                                            var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidWatchWard").Value);
                                            var user = await _userProfileService.GetUserById(dto.UserId);
                                            Usernotification usernotification = new Usernotification();
                                            var replacement = notificationtemplate.Template.Replace("{proccess name}", "Watch & Ward").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                            usernotification.Message = replacement;
                                            usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidWatchWard").Value);
                                            usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                            usernotification.ServiceId = approvalproccess.ServiceId;
                                            usernotification.SendFrom = approvalproccess.SendFrom;
                                            usernotification.SendTo = approvalproccess.SendTo;
                                            result = await _userNotificationService.Create(usernotification, dto.UserId);
                                        }
                                        #endregion
                                    }

                                    break;
                                }
                            }

                        #endregion

                        #region Approval Proccess  Mail Generation Added by ishu 4 Oct 2021
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

                                var senderUser = await _userProfileService.GetUserById(dto.UserId);
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
                                bodyDTO.ApplicationName = "Watch & Ward Application";
                                bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                                bodyDTO.SenderName = senderUser.User.Name;
                                bodyDTO.Link = linkhref;
                                bodyDTO.AppRefNo = dto.RefNo;
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
                           
                       

                        List<ApiSaveWatchandwardDto> dtoData = new List<ApiSaveWatchandwardDto>();
                        apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails
                        {
                            responseCode = "200",
                            responseMessage = "Record added successfully and Sent for Approval.",
                            ApiSaveWatchandwardDto = dtoData
                        };

                        return Ok(apiResponseDetails);
                    }
                    else
                    {
                        List<ApiSaveWatchandwardDto> dtoData = new List<ApiSaveWatchandwardDto>();
                        apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails
                        {
                            responseCode = "404",
                            responseMessage = "Details not found",
                            ApiSaveWatchandwardDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                }
                else
                {
                    List<ApiSaveWatchandwardDto> dtoData = new List<ApiSaveWatchandwardDto>();
                    apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails
                    {
                        responseCode = "400",
                        responseMessage = "Bad Request. Insufficient Parameters",
                        ApiSaveWatchandwardDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            catch (Exception)
            {
                List<ApiSaveWatchandwardDto> dtoData = new List<ApiSaveWatchandwardDto>();
                apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    ApiSaveWatchandwardDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }
       
        [HttpGet]
        [Route("[action]")]
        [Route("api/WatchWardAPI/GetPrimaryListNoList")]
        public async Task<IActionResult> GetPrimaryListNoList()
        {
            APIGetPrimaryListNoResponseDetails apiResponseDetails = new APIGetPrimaryListNoResponseDetails();
            try
            {
                var data = await _watchWardAPIService.GetPrimaryListNoList();
                if (data != null && data.Count > 0)
                {

                    List<APIGetPrimaryListNoListDto> dtoData = new List<APIGetPrimaryListNoListDto>();
                    apiResponseDetails = new APIGetPrimaryListNoResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        APIGetPrimaryListNoListDto = data
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    List<APIGetPrimaryListNoListDto> dtoData = new List<APIGetPrimaryListNoListDto>();
                    apiResponseDetails = new APIGetPrimaryListNoResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        APIGetPrimaryListNoListDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }

            }
            catch (Exception ex)
            {
                List<APIGetPrimaryListNoListDto> dtoData = new List<APIGetPrimaryListNoListDto>();
                apiResponseDetails = new APIGetPrimaryListNoResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    APIGetPrimaryListNoListDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/WatchWardAPI/GetAllWatchWard")]
        public async Task<IActionResult> GetAllWatchWard([FromBody] ApiWatchWardParmsDto dto)
        {
            ApiSaveWatchandwardDtoResponseDetails apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails();
            try
            {
                

                var data = await _watchWardAPIService.GetAllWatchandward(dto);
                        
                    if (data != null && data.Count > 0)
                    {

                        List<ApiSaveWatchandwardDto> dtoData = new List<ApiSaveWatchandwardDto>();
                        apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails
                        {
                            responseCode = "200",
                            responseMessage = "details fetched successfully",
                            ApiSaveWatchandwardDto = data
                        };

                        return Ok(apiResponseDetails);
                    }
                    else
                    {
                        List<ApiSaveWatchandwardDto> dtoData = new List<ApiSaveWatchandwardDto>();
                        apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails
                        {
                            responseCode = "404",
                            responseMessage = " details not found",
                            ApiSaveWatchandwardDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                
            }
            catch (Exception ex)
            {
                List<ApiSaveWatchandwardDto> dtoData = new List<ApiSaveWatchandwardDto>();
                apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    ApiSaveWatchandwardDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        #region Fetch workflow data for approval prrocess Added by ishu 4 Oct 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidWatchWard").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion
    }
}
