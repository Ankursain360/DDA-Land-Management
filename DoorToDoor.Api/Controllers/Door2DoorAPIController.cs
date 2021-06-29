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
using System.IO;
using System.Drawing;

namespace DoorToDoor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class Door2DoorAPIController : ControllerBase
    {
        private readonly IDoor2DoorAPIService _door2DoorAPIService;
        public IConfiguration _configuration;
        string IdentityDocumentPath = "";
        string PropertyDocumentPath = "";
        public Door2DoorAPIController(IDoor2DoorAPIService door2DoorAPIService,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _door2DoorAPIService = door2DoorAPIService;
            IdentityDocumentPath = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyIdentityDocumentPath").Value.ToString();
            PropertyDocumentPath = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyPropertyDocumentPath").Value.ToString();

        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/Door2DoorAPI/SaveDoor2DoorSurvey")]
        public async Task<IActionResult> SaveDoor2DoorSurvey([FromBody] ApiSaveDoor2DoorSurveyDto dto)
        {
            ApiSaveDoor2DoorSurveyResponseDetails apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails();
            try
            {
                if (dto != null)
                {
                    if (dto.PropertyAddress == null || dto.PropertyAddress == "" ||
                        dto.GeoReferencingLattitude == null || dto.GeoReferencingLattitude == "" ||
                        dto.Longitude == null || dto.Longitude == "" ||
                        dto.PresentUseId == null || dto.PresentUseId == 0 ||
                        dto.ApproxPropertyArea == null || dto.ApproxPropertyArea == 0 ||
                        dto.NumberOfFloors == null || dto.NumberOfFloors == "" ||
                        dto.OccupantName == null || dto.OccupantName == "" ||
                        dto.MobileNo == null || dto.MobileNo == "" ||
                        dto.OccupantAadharNo == null || dto.OccupantAadharNo == "" ||
                        dto.VoterIdNo == null || dto.VoterIdNo == "")
                    {
                        List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                        apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                        {
                            responseCode = "205",
                            responseMessage = "Mandatory Fields missing",
                            ApiSaveDoor2DoorSurveyDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                    FileHelper fileHelper = new FileHelper();

                    var data = await _door2DoorAPIService.Create(dto);

                    if (dto.OccupantIdentityPrrofFileData != null && dto.OccupantIdentityPrrofFileData.Count > 0)
                    {
                        List<Doortodoorsurveyidentityproof> doortodoorsurveyidentityproofs = new List<Doortodoorsurveyidentityproof>();
                        for (int i = 0; i < dto.OccupantIdentityPrrofFileData.Count; i++)
                        {
                            if (dto.OccupantIdentityPrrofFileData[i] != "" && dto.OccupantIdentityPrrofFileData != null)
                            {
                                var OccupantIdentityPrrofFilePathData = Guid.NewGuid().ToString() + ".jpg";
                                if (!Directory.Exists(IdentityDocumentPath))
                                {
                                    DirectoryInfo directoryInfo = Directory.CreateDirectory(IdentityDocumentPath);
                                }
                                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(dto.OccupantIdentityPrrofFileData[i])))
                                {
                                    using (Bitmap bm2 = new Bitmap(ms))
                                    {
                                        bm2.Save(IdentityDocumentPath + OccupantIdentityPrrofFilePathData);
                                    }
                                }
                                doortodoorsurveyidentityproofs.Add(new Doortodoorsurveyidentityproof
                                {
                                    DoorToDoorSurveyId = dto.Id,
                                    OccupantIdentityPrrofFilePath = OccupantIdentityPrrofFilePathData,
                                    CreatedBy = dto.CreatedBy
                                });
                            }
                        }

                        foreach (var item in doortodoorsurveyidentityproofs)
                        {
                            data = await _door2DoorAPIService.SaveDoorToDoorSurveyIdentityProofs(item);
                        }
                    }
                    if (dto.PropertyFileData != null && dto.PropertyFileData.Count > 0)
                    {
                        List<Doortodoorsurveypropertyproof> doortodoorsurveypropertyproofs = new List<Doortodoorsurveypropertyproof>();
                        for (int i = 0; i < dto.PropertyFileData.Count; i++)
                        {
                            if (dto.PropertyFileData[i] != "" && dto.PropertyFileData[i] != null)
                            {
                                var PropertyFilePathdata = Guid.NewGuid().ToString() + ".jpg";
                                if (!Directory.Exists(PropertyDocumentPath))
                                {
                                    DirectoryInfo directoryInfo = Directory.CreateDirectory(PropertyDocumentPath);
                                }
                                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(dto.PropertyFileData[i])))
                                {
                                    using (Bitmap bm2 = new Bitmap(ms))
                                    {
                                        bm2.Save(PropertyDocumentPath + PropertyFilePathdata);
                                    }
                                }
                                doortodoorsurveypropertyproofs.Add(new Doortodoorsurveypropertyproof
                                {
                                    DoorToDoorSurveyId = dto.Id,
                                    PropertyFilePath = PropertyFilePathdata,
                                    CreatedBy = dto.CreatedBy
                                });
                            }
                        }

                        foreach (var item in doortodoorsurveypropertyproofs)
                        {
                            data = await _door2DoorAPIService.SaveDoorToDoorSurveyPropertyProofs(item);
                        }
                    }

                    if (data == true)
                    {

                        List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                        apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                        {
                            responseCode = "200",
                            responseMessage = "Record saved successfully",
                            ApiSaveDoor2DoorSurveyDto = dtoData
                        };

                        return Ok(apiResponseDetails);
                    }
                    else
                    {
                        List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                        apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                        {
                            responseCode = "404",
                            responseMessage = "Details not found",
                            ApiSaveDoor2DoorSurveyDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                }
                else
                {
                    List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                    apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                    {
                        responseCode = "400",
                        responseMessage = "Bad Request. Insufficient Parameters",
                        ApiSaveDoor2DoorSurveyDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            catch (Exception)
            {
                List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    ApiSaveDoor2DoorSurveyDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/Door2DoorAPI/UpdateDoor2DoorSurvey")]
        public async Task<IActionResult> UpdateDoor2DoorSurvey([FromBody] ApiSaveDoor2DoorSurveyDto dto)
        {
            ApiSaveDoor2DoorSurveyResponseDetails apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails();
            try
            {
                if (dto != null)
                {
                    if (dto.PropertyAddress == null || dto.PropertyAddress == "" ||
                        dto.GeoReferencingLattitude == null || dto.GeoReferencingLattitude == "" ||
                        dto.Longitude == null || dto.Longitude == "" ||
                        dto.PresentUseId == null || dto.PresentUseId == 0 ||
                        dto.ApproxPropertyArea == null || dto.ApproxPropertyArea == 0 ||
                        dto.NumberOfFloors == null || dto.NumberOfFloors == "" ||
                        dto.OccupantName == null || dto.OccupantName == "" ||
                        dto.MobileNo == null || dto.MobileNo == "" ||
                        dto.OccupantAadharNo == null || dto.OccupantAadharNo == "" ||
                        dto.VoterIdNo == null || dto.VoterIdNo == "" || dto.Id == 0)
                    {
                        List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                        apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                        {
                            responseCode = "205",
                            responseMessage = "Mandatory Fields missing",
                            ApiSaveDoor2DoorSurveyDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                    FileHelper fileHelper = new FileHelper();

                    var data = await _door2DoorAPIService.Update(dto);


                    if (dto.OccupantIdentityPrrofFileData != null && dto.OccupantIdentityPrrofFileData.Count > 0)
                    {
                        List<Doortodoorsurveyidentityproof> doortodoorsurveyidentityproofs = new List<Doortodoorsurveyidentityproof>();
                        data = await _door2DoorAPIService.DeleteDoorToDoorSurveyIdentityProofs(dto.Id);
                        for (int i = 0; i < dto.OccupantIdentityPrrofFileData.Count; i++)
                        {
                            if (dto.OccupantIdentityPrrofFileData[i] != "" && dto.OccupantIdentityPrrofFileData != null)
                            {
                                var OccupantIdentityPrrofFilePathData = Guid.NewGuid().ToString() + ".jpg";
                                if (!Directory.Exists(IdentityDocumentPath))
                                {
                                    DirectoryInfo directoryInfo = Directory.CreateDirectory(IdentityDocumentPath);
                                }
                                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(dto.OccupantIdentityPrrofFileData[i])))
                                {
                                    using (Bitmap bm2 = new Bitmap(ms))
                                    {
                                        bm2.Save(IdentityDocumentPath + OccupantIdentityPrrofFilePathData);
                                    }
                                }
                                doortodoorsurveyidentityproofs.Add(new Doortodoorsurveyidentityproof
                                {
                                    DoorToDoorSurveyId = dto.Id,
                                    OccupantIdentityPrrofFilePath = OccupantIdentityPrrofFilePathData,
                                    CreatedBy = dto.CreatedBy
                                });
                            }
                        }

                        foreach (var item in doortodoorsurveyidentityproofs)
                        {
                            data = await _door2DoorAPIService.SaveDoorToDoorSurveyIdentityProofs(item);
                        }
                    }
                    if (dto.PropertyFileData != null && dto.PropertyFileData.Count > 0)
                    {
                        List<Doortodoorsurveypropertyproof> doortodoorsurveypropertyproofs = new List<Doortodoorsurveypropertyproof>();
                        data = await _door2DoorAPIService.DeleteDoorToDoorSurveyPropertyProofs(dto.Id);
                        for (int i = 0; i < dto.PropertyFileData.Count; i++)
                        {
                            if (dto.PropertyFileData[i] != "" && dto.PropertyFileData[i] != null)
                            {
                                var PropertyFilePathdata = Guid.NewGuid().ToString() + ".jpg";
                                if (!Directory.Exists(PropertyDocumentPath))
                                {
                                    DirectoryInfo directoryInfo = Directory.CreateDirectory(PropertyDocumentPath);
                                }
                                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(dto.PropertyFileData[i])))
                                {
                                    using (Bitmap bm2 = new Bitmap(ms))
                                    {
                                        bm2.Save(PropertyDocumentPath + PropertyFilePathdata);
                                    }
                                }
                                doortodoorsurveypropertyproofs.Add(new Doortodoorsurveypropertyproof
                                {
                                    DoorToDoorSurveyId = dto.Id,
                                    PropertyFilePath = PropertyFilePathdata,
                                    CreatedBy = dto.CreatedBy
                                });
                            }
                        }

                        foreach (var item in doortodoorsurveypropertyproofs)
                        {
                            data = await _door2DoorAPIService.SaveDoorToDoorSurveyPropertyProofs(item);
                        }
                    }

                    if (data == true)
                    {

                        List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                        apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                        {
                            responseCode = "200",
                            responseMessage = "Record updated successfully.",
                            ApiSaveDoor2DoorSurveyDto = dtoData
                        };

                        return Ok(apiResponseDetails);
                    }
                    else
                    {
                        List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                        apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                        {
                            responseCode = "404",
                            responseMessage = " details not found",
                            ApiSaveDoor2DoorSurveyDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                }
                else
                {
                    List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                    apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                    {
                        responseCode = "400",
                        responseMessage = "Bad Request. Insufficient Parameters",
                        ApiSaveDoor2DoorSurveyDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            catch (Exception ex)
            {
                List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    ApiSaveDoor2DoorSurveyDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/Door2DoorAPI/GetSingleDoor2DoorSurvey")]
        public async Task<IActionResult> GetSingleDoor2DoorSurvey([FromBody] ApiSaveDoor2DoorSurveyDto dto)
        {
            ApiSaveDoor2DoorSurveyResponseDetails apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails();
            try
            {
                if (dto != null)
                {
                    var data = await _door2DoorAPIService.GetSurveyDetails(dto, (_configuration.GetSection("FileAPiPaths:Door2DoorSurvey:SurveyIdentityDocumentPath").Value).ToString(), (_configuration.GetSection("FileAPiPaths:Door2DoorSurvey:SurveyPropertyDocumentPath").Value).ToString());
                    if (data != null && data.Count > 0)
                    {

                        List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                        apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                        {
                            responseCode = "200",
                            responseMessage = "details fetched successfully",
                            ApiSaveDoor2DoorSurveyDto = data
                        };

                        return Ok(apiResponseDetails);
                    }
                    else
                    {
                        List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                        apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                        {
                            responseCode = "404",
                            responseMessage = " details not found",
                            ApiSaveDoor2DoorSurveyDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                }
                else
                {
                    List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                    apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                    {
                        responseCode = "400",
                        responseMessage = "Bad Request. Insufficient Parameters",
                        ApiSaveDoor2DoorSurveyDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            catch (Exception ex)
            {
                List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    ApiSaveDoor2DoorSurveyDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/Door2DoorAPI/GetAllDoor2DoorSurvey")]
        public async Task<IActionResult> GetAllDoor2DoorSurvey([FromBody] ApiGetAllDoor2DoorSurveyParamsDto dto)
        {
            ApiSaveDoor2DoorSurveyResponseDetails apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails();
            try
            {
                if (dto != null)
                {
                    if (dto.RoleId == 0 || dto.UserId == 0)
                    {
                        List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                        apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                        {
                            responseCode = "400",
                            responseMessage = "Bad Request. Insufficient Parameters",
                            ApiSaveDoor2DoorSurveyDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                    var data = await _door2DoorAPIService.GetAllSurveyDetails(dto, Convert.ToInt32(_configuration.GetSection("DoorToDoorAdminRole").Value));
                    if (data != null && data.Count > 0)
                    {

                        List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                        apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                        {
                            responseCode = "200",
                            responseMessage = "details fetched successfully",
                            ApiSaveDoor2DoorSurveyDto = data
                        };

                        return Ok(apiResponseDetails);
                    }
                    else
                    {
                        List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                        apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                        {
                            responseCode = "404",
                            responseMessage = " details not found",
                            ApiSaveDoor2DoorSurveyDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                }
                else
                {
                    List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                    apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                    {
                        responseCode = "400",
                        responseMessage = "Bad Request. Insufficient Parameters",
                        ApiSaveDoor2DoorSurveyDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            catch (Exception ex)
            {
                List<ApiSaveDoor2DoorSurveyDto> dtoData = new List<ApiSaveDoor2DoorSurveyDto>();
                apiResponseDetails = new ApiSaveDoor2DoorSurveyResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    ApiSaveDoor2DoorSurveyDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/Door2DoorAPI/GetPresentUse")]
        public async Task<IActionResult> GetPresentUse()
        {
            ApiGetPresentUseResponseDetails apiResponseDetails = new ApiGetPresentUseResponseDetails();
            try
            {
                var data = await _door2DoorAPIService.GetPresentUseDetails();
                if (data != null && data.Count > 0)
                {

                    List<ApiGetPresentUseDto> dtoData = new List<ApiGetPresentUseDto>();
                    apiResponseDetails = new ApiGetPresentUseResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        ApiGetPresentUseDto = data
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    List<ApiGetPresentUseDto> dtoData = new List<ApiGetPresentUseDto>();
                    apiResponseDetails = new ApiGetPresentUseResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        ApiGetPresentUseDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }

            }
            catch (Exception ex)
            {
                List<ApiGetPresentUseDto> dtoData = new List<ApiGetPresentUseDto>();
                apiResponseDetails = new ApiGetPresentUseResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    ApiGetPresentUseDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/Door2DoorAPI/Login")]
        public async Task<IActionResult> Login([FromBody] ApiSurveyUserLoginDto dto)
        {
            ApiSurveyUserDetailsResponseDetails apiResponseDetails = new ApiSurveyUserDetailsResponseDetails();
            try
            {
                if (dto != null)
                {
                    if (dto.username == null || dto.username == "" ||
                        dto.password == null || dto.password == "")
                    {
                        List<ApiSurveyUserDetailsDto> dtoData = new List<ApiSurveyUserDetailsDto>();
                        apiResponseDetails = new ApiSurveyUserDetailsResponseDetails
                        {
                            responseCode = "205",
                            responseMessage = "Either username or password missing",
                            ApiSurveyUserDetailsDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                    dto.password = (new EncryptionHelper()).Base64Encode(dto.password);
                    var data = await _door2DoorAPIService.VerifySurveyUserDetailsLogin(dto);
                    if (data != null && data.Count > 0)
                    {
                        apiResponseDetails = new ApiSurveyUserDetailsResponseDetails
                        {
                            responseCode = "200",
                            responseMessage = "details fetched successfully",
                            ApiSurveyUserDetailsDto = data
                        };

                        return Ok(apiResponseDetails);
                    }
                    else
                    {
                        List<ApiSurveyUserDetailsDto> dtoData = new List<ApiSurveyUserDetailsDto>();
                        apiResponseDetails = new ApiSurveyUserDetailsResponseDetails
                        {
                            responseCode = "404",
                            responseMessage = " details not found",
                            ApiSurveyUserDetailsDto = dtoData
                        };
                        return NotFound(apiResponseDetails);
                    }
                }
                else
                {
                    List<ApiSurveyUserDetailsDto> dtoData = new List<ApiSurveyUserDetailsDto>();
                    apiResponseDetails = new ApiSurveyUserDetailsResponseDetails
                    {
                        responseCode = "400",
                        responseMessage = "Bad Request. Insufficient Parameters",
                        ApiSurveyUserDetailsDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            catch (Exception)
            {
                List<ApiSurveyUserDetailsDto> dtoData = new List<ApiSurveyUserDetailsDto>();
                apiResponseDetails = new ApiSurveyUserDetailsResponseDetails
                {
                    responseCode = "500",
                    responseMessage = "Internal Server Error",
                    ApiSurveyUserDetailsDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }
    }
}
