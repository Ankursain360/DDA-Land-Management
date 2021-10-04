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

namespace Vacant.Land.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WatchWardAPIController : ControllerBase
    {
        private readonly IWatchWardAPIService _watchWardAPIService;
        private readonly IWatchandwardService _watchandwardService;
        public IConfiguration _configuration;
        string targetPhotoPathLayout = "";
        string targetReportfilePathLayout = "";
        public WatchWardAPIController(IWatchWardAPIService watchWardAPIService,
            IWatchandwardService watchandwardService,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _watchWardAPIService = watchWardAPIService;
            _watchandwardService = watchandwardService;
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
                    if (dto.Date == null  ||
                        dto.LocalityId == null || dto.LocalityId == 0 ||
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

                        List<ApiSaveWatchandwardDto> dtoData = new List<ApiSaveWatchandwardDto>();
                        apiResponseDetails = new ApiSaveWatchandwardDtoResponseDetails
                        {
                            responseCode = "200",
                            responseMessage = "Record saved successfully",
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
                //if (dto != null)
                //{
                //    //if (dto.RoleId == 0 || dto.UserId == 0)
                //{
                //    List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                //    apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                //    {
                //        responseCode = "400",
                //        responseMessage = "Bad Request. Insufficient Parameters",
                //        ApiSaveDoor2DoorSurveyDto = dtoData
                //    };
                //    return NotFound(apiResponseDetails);
                //}

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
                //}
                //else
                //{
                //    List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                //    apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                //    {
                //        responseCode = "400",
                //        responseMessage = "Bad Request. Insufficient Parameters",
                //        ApiSaveDoor2DoorSurveyDto = dtoData
                //    };
                //    return NotFound(apiResponseDetails);
                //}
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

    }
}
