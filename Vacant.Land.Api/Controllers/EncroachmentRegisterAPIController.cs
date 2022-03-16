
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Master;
using Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using Core.Enum;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Vacant.Land.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EncroachmentRegisterAPIController : ControllerBase
    {
        private readonly IEncroachmentRegisterAPIService _encroachmentRegisterAPIService;
        public IConfiguration _configuration;
        private readonly IWatchWardAPIService _watchWardAPIService;
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
        private readonly IWatchandwardService _watchandwardService;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserProfileService _userProfileService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserNotificationService _userNotificationService;

        //string targetPhotoPathLayout = "";
        //string targetReportfilePathLayout = "";
        string PhotoFilePath = "";
        string LocationMapFilePath = "";
        string FirfilePath = "";
        public EncroachmentRegisterAPIController(IEncroachmentRegisterAPIService encroachmentRegisterAPIService,
            IConfiguration configuration,
            IWatchWardAPIService watchWardAPIService,
            IEncroachmentRegisterationService encroachmentRegisterationService,
            IWatchandwardService watchandwardService,
            IApprovalProccessService approvalproccessService,
            IWorkflowTemplateService workflowtemplateService,
            IUserProfileService userProfileService,
            IHostingEnvironment hostingEnvironment,
            IUserNotificationService userNotificationService)
        {
            _configuration = configuration;
            _encroachmentRegisterAPIService = encroachmentRegisterAPIService;

            _encroachmentRegisterationService = encroachmentRegisterationService;
            _watchWardAPIService = watchWardAPIService;
            _watchandwardService = watchandwardService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _userProfileService = userProfileService;
            _hostingEnvironment = hostingEnvironment;
            _userNotificationService = userNotificationService;
            //targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
            //targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();
            PhotoFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:PhotoFilePath").Value.ToString();
            LocationMapFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:LocationMapFilePath").Value.ToString();
            FirfilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:FIRFilePath").Value.ToString();

        }
       
        [HttpGet]
        [Route("[action]")]
        [Route("api/EncroachmentRegisterAPI/GetDepartmentList")]
        public async Task<IActionResult> GetDepartmentList()
        {
            APIGetDepartmentListDtoResponseDetails apiResponseDetails = new APIGetDepartmentListDtoResponseDetails();
            try
            {
                var data = await _encroachmentRegisterAPIService.GetDepartmentDropDownList();
                if (data != null && data.Count > 0)
                {

                    List<APIGetDepartmentListDto> dtoData = new List<APIGetDepartmentListDto>();
                    apiResponseDetails = new APIGetDepartmentListDtoResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        APIGetDepartmentListDto = data
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    List<APIGetDepartmentListDto> dtoData = new List<APIGetDepartmentListDto>();
                    apiResponseDetails = new APIGetDepartmentListDtoResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        APIGetDepartmentListDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }

            }
            catch (Exception ex)
            {
                List<APIGetDepartmentListDto> dtoData = new List<APIGetDepartmentListDto>();
                apiResponseDetails = new APIGetDepartmentListDtoResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    APIGetDepartmentListDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

       
        [HttpPost]
        [Route("[action]")]
        [Route("api/EncroachmentRegisterAPI/GetZoneList")]
        public async Task<IActionResult> GetZoneList(int departmentId)
        {
            ApiGetZoneListDtoResponseDetails apiResponseDetails = new ApiGetZoneListDtoResponseDetails();
            List<ApiGetZoneListDto> dtoData = new List<ApiGetZoneListDto>();
            if (departmentId != 0)
            {
                var data = await _encroachmentRegisterAPIService.GetZoneDropDownList(departmentId);
                if (data != null)
                {
                    
                    apiResponseDetails = new ApiGetZoneListDtoResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        ApiGetZoneListDto = data
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    
                    apiResponseDetails = new ApiGetZoneListDtoResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        ApiGetZoneListDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            else
            {
               
                apiResponseDetails = new ApiGetZoneListDtoResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    ApiGetZoneListDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/EncroachmentRegisterAPI/GetDivisionList")]
        public async Task<IActionResult> GetDivisionList(int zoneId)
        {
            ApiGetDivisionListDtoResponseDetails apiResponseDetails = new ApiGetDivisionListDtoResponseDetails();
            List<ApiGetDivisionListDto> dtoData = new List<ApiGetDivisionListDto>();
            if (zoneId != 0)
            {
                var data = await _encroachmentRegisterAPIService.GetDivisionDropDownList(zoneId);
                if (data != null)
                {

                    apiResponseDetails = new ApiGetDivisionListDtoResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        ApiGetDivisionListDto = data
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {

                    apiResponseDetails = new ApiGetDivisionListDtoResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        ApiGetDivisionListDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            else
            {

                apiResponseDetails = new ApiGetDivisionListDtoResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    ApiGetDivisionListDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/EncroachmentRegisterAPI/GetLocalityList")]
        public async Task<IActionResult> GetLocalityList(int divisionId)
        {
            ApiGetLocalityListDtoResponseDetails apiResponseDetails = new ApiGetLocalityListDtoResponseDetails();
            List<ApiGetLocalityListDto> dtoData = new List<ApiGetLocalityListDto>();
            if (divisionId != 0)
            {
                var data = await _encroachmentRegisterAPIService.GetLocalityDropDownList(divisionId);
                if (data != null)
                {

                    apiResponseDetails = new ApiGetLocalityListDtoResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        ApiGetLocalityListDto = data
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {

                    apiResponseDetails = new ApiGetLocalityListDtoResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        ApiGetLocalityListDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            else
            {

                apiResponseDetails = new ApiGetLocalityListDtoResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    ApiGetLocalityListDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/EncroachmentRegisterAPI/GetKhasraList")]
        public async Task<IActionResult> GetKhasraList()
        {
            APIGetKhasraListDtoResponseDetails apiResponseDetails = new APIGetKhasraListDtoResponseDetails();
            try
            {
                var data = await _encroachmentRegisterAPIService.GetKhasraDropDownList();
                if (data != null)
                {

                    List<APIGetKhasraListDto> dtoData = new List<APIGetKhasraListDto>();
                    apiResponseDetails = new APIGetKhasraListDtoResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        APIGetKhasraListDto = data
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    List<APIGetKhasraListDto> dtoData = new List<APIGetKhasraListDto>();
                    apiResponseDetails = new APIGetKhasraListDtoResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        APIGetKhasraListDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }

            }
            catch (Exception ex)
            {
                List<APIGetKhasraListDto> dtoData = new List<APIGetKhasraListDto>();
                apiResponseDetails = new APIGetKhasraListDtoResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    APIGetKhasraListDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }
        [HttpPost]
        [Route("[action]")]
        [Route("api/EncroachmentRegisterAPI/SaveEncroachmentRegisterAPIdata")]
        public async Task<IActionResult> SaveEncroachmentRegisterAPIdata([FromBody] ApiSaveEncroachmentRegisterDto dto)
        {
            ApiSaveEncroachmentRegisterDtoResponseDetails apiResponseDetails = new ApiSaveEncroachmentRegisterDtoResponseDetails();
            try
            {
                if (dto != null)
                {
                    if (dto.EncrochmentDate == null ||
                       dto.UserId == null || dto.UserId == 0 ||
                        dto.DepartmentId == null || dto.DepartmentId == 0 ||
                         dto.ZoneId == null || dto.ZoneId == 0 ||
                          dto.DivisionId == null || dto.DivisionId == 0 ||
                         dto.LocalityId == null || dto.LocalityId == 0 ||
                        dto.KhasraNo == null || dto.KhasraNo == ""||
                        dto.AreaUnit == 0 || 
                        dto.Area == null || dto.Area == 0 ||
                        dto.StatusOfLand == null || dto.StatusOfLand == "" ||
                        dto.IsPossession == null || dto.IsPossession == "" ||
                         dto.IsEncroachment == null || dto.IsEncroachment == "" ||
                         dto.PoliceStation == null || dto.PoliceStation == "" ||
                         dto.SecurityGuardOnDuty == null || dto.SecurityGuardOnDuty == "" ||
                         dto.Remarks == null || dto.Remarks == "" ||
                         dto.LocationAddressWithLandMark == null || dto.LocationAddressWithLandMark == "" 
                      )
                    {
                        List<ApiSaveEncroachmentRegisterDto> dtoData = new List<ApiSaveEncroachmentRegisterDto>();
                        apiResponseDetails = new ApiSaveEncroachmentRegisterDtoResponseDetails
                        {
                            responseCode = "205",
                            responseMessage = "Mandatory Fields missing",
                            ApiSaveEncroachmentRegisterDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                    FileHelper fileHelper = new FileHelper();

                    Random r = new Random();
                    int num = r.Next();
                    var zonecode =  await _encroachmentRegisterAPIService.GetZonecode(dto.ZoneId);
                    dto.RefNo = DateTime.Now.Year.ToString() + zonecode.Code + num.ToString();

                    #region Approval Proccess At 1st level Check Initial Before Creating Record  Added by ishu 8 oct 2021

                    Approvalproccess approvalproccess = new Approvalproccess();
                    var user1 = await _watchWardAPIService.GetUserOngivenUserId(dto.UserId);//to get user zone details

                    var DataFlow = await dataAsync();
                    for (int i = 0; i < DataFlow.Count; i++)
                    {
                        if (!DataFlow[i].parameterSkip)
                        {
                            if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                            {
                                if (user1.ZoneId == null)
                                {
                                    List<ApiSaveEncroachmentRegisterDto> dtoData = new List<ApiSaveEncroachmentRegisterDto>();
                                    apiResponseDetails = new ApiSaveEncroachmentRegisterDtoResponseDetails
                                    {
                                        responseCode = "500",
                                        responseMessage = "Your Zone is not available , Without zone application cannot be processed further, Please contact system administrator",
                                        ApiSaveEncroachmentRegisterDto = dtoData
                                    };
                                    return NotFound(apiResponseDetails);
                                    //ViewBag.Message = Alert.Show("Without Zone application cannot be submitted, Please Contact System Administrator", "", AlertType.Warning);
                                    //return View(encroachmentRegisterations);
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
                                            var UserProfile = await _userProfileService.GetUserByIdZoneConcatedName(Convert.ToInt32(MultiUserId), user1.ZoneId ?? 0);
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
                    dto.OtherDepartment = dto.OtherDepartment == 0 ? null : dto.OtherDepartment;
                    dto.WatchWardId = dto.WatchWardId == 0 ? null : dto.WatchWardId;


                    var data = await _encroachmentRegisterAPIService.Create(dto);

                    ///for photo file:
                    //if (dto.PhotoFileData != null && dto.PhotoFileData.Count > 0)
                    //{
                    //    List<Watchandwardphotofiledetails> photo = new List<Watchandwardphotofiledetails>();
                    //    for (int i = 0; i < dto.PhotoFileData.Count; i++)
                    //    {
                    //        if (dto.PhotoFileData[i] != "" && dto.PhotoFileData != null)
                    //        {
                    //            var PhotoPath = Guid.NewGuid().ToString() + ".jpg";
                    //            if (!Directory.Exists(targetPhotoPathLayout))
                    //            {
                    //                DirectoryInfo directoryInfo = Directory.CreateDirectory(targetPhotoPathLayout);
                    //            }
                    //            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(dto.PhotoFileData[i])))
                    //            {
                    //                using (Bitmap bm2 = new Bitmap(ms))
                    //                {
                    //                    bm2.Save(targetPhotoPathLayout + PhotoPath);
                    //                }
                    //            }
                    //            photo.Add(new Watchandwardphotofiledetails
                    //            {
                    //                WatchAndWardId = dto.Id,
                    //                PhotoFilePath = PhotoPath,
                    //                Lattitude = dto.Latitude,
                    //                Longitude = dto.Longitude,
                    //                LattLongUrl = "NA"
                    //            });
                    //        }
                    //    }

                    //    foreach (var item in photo)
                    //    {
                    //        data = await _watchWardAPIService.SaveWatchandwardphotofiledetails(item);
                    //    }
                    //}
                   


                    if (data == true)
                    {
                        if (dto.NameOfStructure != null &&
                                  dto.AreaApprox != null &&
                                  dto.Type != null &&
                                  dto.DateOfEncroachment != null &&
                                  dto.CountOfStructure != null &&
                                  dto.ReferenceNoOnLocation != null &&
                                  dto.ConstructionStatus != null &&
                                  dto.NameOfStructure.Count > 0 &&
                                  dto.AreaApprox.Count > 0 &&
                                  dto.Type.Count > 0 &&
                                  dto.DateOfEncroachment.Count > 0 &&
                                  dto.CountOfStructure.Count > 0 &&
                                  dto.ConstructionStatus.Count > 0 &&
                                  dto.ReferenceNoOnLocation.Count > 0)
                        {
                            List<DetailsOfEncroachment> detailsOfEncroachment = new List<DetailsOfEncroachment>();
                            for (int i = 0; i < dto.NameOfStructure.Count; i++)
                            {
                                detailsOfEncroachment.Add(new DetailsOfEncroachment
                                {
                                    Area = dto.AreaApprox.Count <= i ? 0 : dto.AreaApprox[i],
                                    CountOfStructure = dto.CountOfStructure.Count <= i ? 0 : dto.CountOfStructure[i],
                                    DateOfEncroachment = dto.DateOfEncroachment.Count <= i ? "0" : dto.DateOfEncroachment[i],
                                    ReligiousStructure = dto.ReligiousStructure.Count <= i ? "" : dto.ReligiousStructure[i],
                                    ConstructionStatus = dto.ConstructionStatus.Count <= i ? "" : dto.ConstructionStatus[i],
                                    NameOfStructure = dto.NameOfStructure.Count <= i ? "" : dto.NameOfStructure[i],
                                    ReferenceNoOnLocation = dto.ReferenceNoOnLocation.Count <= i ? "" : dto.ReferenceNoOnLocation[i],
                                    Type = dto.Type.Count <= i ? "" : dto.Type[i],
                                    EncroachmentRegisterationId = dto.Id
                                });
                            }
                            foreach (var item in detailsOfEncroachment)
                            {
                               var result = await _encroachmentRegisterationService.SaveDetailsOfEncroachment(item);
                            }
                        }

                        if (dto.Firfile != null && dto.Firfile.Count > 0)
                        {
                            List<EncroachmentFirFileDetails> encroachmentFirFileDetails = new List<EncroachmentFirFileDetails>();
                            for (int i = 0; i < dto.Firfile.Count; i++)
                            {
                                string FilePath = fileHelper.SaveFile1(FirfilePath, dto.Firfile[i]);
                                encroachmentFirFileDetails.Add(new EncroachmentFirFileDetails
                                {
                                    EncroachmentRegistrationId = dto.Id,
                                    FirFilePath = FilePath
                                });
                            }
                            foreach (var item in encroachmentFirFileDetails)
                            {
                               var result = await _encroachmentRegisterationService.SaveEncroachmentFirFileDetails(item);
                            }
                        }
                        if (dto.PhotoFile != null && dto.PhotoFile.Count > 0)
                        {
                            List<EncroachmentPhotoFileDetails> encroachmentPhotoFileDetails = new List<EncroachmentPhotoFileDetails>();
                            for (int i = 0; i < dto.PhotoFile.Count; i++)
                            {
                                string FilePath = fileHelper.SaveFile1(PhotoFilePath, dto.PhotoFile[i]);
                                encroachmentPhotoFileDetails.Add(new EncroachmentPhotoFileDetails
                                {
                                    EncroachmentRegistrationId = dto.Id,
                                    PhotoFilePath = FilePath
                                });
                            }
                            foreach (var item in encroachmentPhotoFileDetails)
                            {
                               var result = await _encroachmentRegisterationService.SaveEncroachmentPhotoFileDetails(item);
                            }
                        }
                        if (dto.LocationMapFile != null && dto.LocationMapFile.Count > 0)
                        {
                            List<EncroachmentLocationMapFileDetails> encroachmentLocationMapFileDetails = new List<EncroachmentLocationMapFileDetails>();
                            for (int i = 0; i < dto.LocationMapFile.Count; i++)
                            {
                                string FilePath = fileHelper.SaveFile1(LocationMapFilePath, dto.LocationMapFile[i]);
                                encroachmentLocationMapFileDetails.Add(new EncroachmentLocationMapFileDetails
                                {
                                    EncroachmentRegistrationId = dto.Id,
                                    LocationMapFilePath = FilePath
                                });
                            }
                            foreach (var item in encroachmentLocationMapFileDetails)
                            {
                              var  result = await _encroachmentRegisterationService.SaveEncroachmentLocationMapFileDetails(item);
                            }
                        }
                        #region Approval Proccess At 1st level start Added by ishu 8 oct 2021

                        var workflowtemplatedata = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidInspection").Value));
                        var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);
                        for (int i = 0; i < DataFlow.Count; i++)
                        {
                            if (!DataFlow[i].parameterSkip)
                            {
                                dto.ApprovedStatus = ApprovalStatus.Id;
                                dto.PendingAt = approvalproccess.SendTo;
                               var  result = await _encroachmentRegisterAPIService.UpdateBeforeApproval(dto);  //Update Table details 
                                if (result)
                                {
                                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                    approvalproccess.ProcessGuid = (_configuration.GetSection("workflowPreccessGuidInspection").Value);
                                    approvalproccess.ServiceId = dto.Id;
                                    approvalproccess.SendFrom = dto.UserId.ToString();
                                    // approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
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
                                    approvalproccess.Remarks = "Record Added and Sent for Approval";///May be Uncomment
                                    result = await _approvalproccessService.Create(approvalproccess, dto.UserId); //Create a row in approvalproccess Table
                                    
                                    #region Insert Into usernotification table Added By Renu 18 June 2021
                                    if (result == true && approvalproccess.SendTo != null)
                                    {
                                        var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidInspection").Value);
                                        var user = await _userProfileService.GetUserById(dto.UserId);
                                        Usernotification usernotification = new Usernotification();
                                        var replacement = notificationtemplate.Template.Replace("{proccess name}", "Inspection/Encroachment Register").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                        usernotification.Message = replacement;
                                        usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidInspection").Value);
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

                        #region Approval Proccess  Mail Generation Added by ishu 8 oct 2021
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

                            #region Mail Generation Added By Renu

                            MailSMSHelper mailG = new MailSMSHelper();

                            #region HTML Body Generation
                            ApprovalMailBodyDto bodyDTO = new ApprovalMailBodyDto();
                            bodyDTO.ApplicationName = "Inspection/Encroachment Register Application";
                            bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                            bodyDTO.SenderName = senderUser.User.Name;
                            bodyDTO.Link = linkhref;
                            bodyDTO.AppRefNo = dto.RefNo;
                            bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                            bodyDTO.Remarks = approvalproccess.Remarks;
                            bodyDTO.path = path;
                            string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                            #endregion

                            //sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                            #region Common Mail Genration
                            SentMailGenerationDto maildto = new SentMailGenerationDto();
                            maildto.strMailSubject = "Pending Inspection/Encroachment Register Application Approval Request Details ";
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


                        List<ApiSaveEncroachmentRegisterDto> dtoData = new List<ApiSaveEncroachmentRegisterDto>();
                        apiResponseDetails = new ApiSaveEncroachmentRegisterDtoResponseDetails
                        {
                            responseCode = "200",
                            responseMessage = "Record added successfully and Sent for Approval.",
                            ApiSaveEncroachmentRegisterDto = dtoData
                        };

                        return Ok(apiResponseDetails);
                    }
                    else
                    {
                        List<ApiSaveEncroachmentRegisterDto> dtoData = new List<ApiSaveEncroachmentRegisterDto>();
                        apiResponseDetails = new ApiSaveEncroachmentRegisterDtoResponseDetails
                        {
                            responseCode = "404",
                            responseMessage = "Details not found",
                            ApiSaveEncroachmentRegisterDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                }
                else
                {
                    List<ApiSaveEncroachmentRegisterDto> dtoData = new List<ApiSaveEncroachmentRegisterDto>();
                    apiResponseDetails = new ApiSaveEncroachmentRegisterDtoResponseDetails
                    {
                        responseCode = "400",
                        responseMessage = "Bad Request. Insufficient Parameters",
                        ApiSaveEncroachmentRegisterDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            catch (Exception ex)
            {
                List<ApiSaveEncroachmentRegisterDto> dtoData = new List<ApiSaveEncroachmentRegisterDto>();
                apiResponseDetails = new ApiSaveEncroachmentRegisterDtoResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    ApiSaveEncroachmentRegisterDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }
        #region Fetch workflow data for approval prrocess Added by ishu 8 oct 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowPreccessGuidInspection").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion

        [HttpPost]
        [Route("[action]")]
        [Route("api/EncroachmentRegisterAPI/GetAllEncroachmentRegisterAPIdata")]
        
        public async Task<IActionResult> GetAllEncroachmentRegisterAPIdata([FromBody] ApiEncroachmentRegisterParmsDto dto)
        {
            ApiSaveEncroachmentRegisterDtoResponseDetails apiResponseDetails = new ApiSaveEncroachmentRegisterDtoResponseDetails();
            try
            {


                var data = await _encroachmentRegisterAPIService.GetAllEncroachmentRegisterAPIdata(dto);

                if (data != null )
                {

                    List<ApiSaveEncroachmentRegisterDto> dtoData = new List<ApiSaveEncroachmentRegisterDto>();
                    apiResponseDetails = new ApiSaveEncroachmentRegisterDtoResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        ApiSaveEncroachmentRegisterDto = data
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    List<ApiSaveEncroachmentRegisterDto> dtoData = new List<ApiSaveEncroachmentRegisterDto>();
                    apiResponseDetails = new ApiSaveEncroachmentRegisterDtoResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        ApiSaveEncroachmentRegisterDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }

            }
            catch (Exception ex)
            {
                List<ApiSaveEncroachmentRegisterDto> dtoData = new List<ApiSaveEncroachmentRegisterDto>();
                apiResponseDetails = new ApiSaveEncroachmentRegisterDtoResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    ApiSaveEncroachmentRegisterDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

    }
}
