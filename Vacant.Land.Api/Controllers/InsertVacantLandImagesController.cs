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

namespace Vacant.Land.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class InsertVacantLandImagesController : ControllerBase
    {
        private readonly IInsertVacantLandImagesService _insertVacantLandImagesService;
        public IConfiguration _configuration;
        string VacantLandImagePath = "";
        public InsertVacantLandImagesController(IInsertVacantLandImagesService insertVacantLandImagesService,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _insertVacantLandImagesService = insertVacantLandImagesService;
            VacantLandImagePath = _configuration.GetSection("FilePaths:VacantLandImage:VacantLandImagePath").Value.ToString();

        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/InsertVacantLandImages/SaveVacantLandImage")]
        public async Task<IActionResult> SaveVacantLandImage([FromBody] ApiInsertVacantLandImageDto dto)
        {
            ApiInsertVacantLandImageResponseDetails apiResponseDetails = new ApiInsertVacantLandImageResponseDetails();
            if (dto != null)
            {
                if (dto.BoundaryWall =="" || dto.BoundaryWall == null)
                {
                   List<ApiInsertVacantLandImageDto> dtodata = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "Boundary Wall is mandatory",
                        response = dtodata
                    };
                    return Ok(apiResponseDetails);
                }
                else if (dto.Fencing == "" || dto.Fencing == null)
                {
                    List<ApiInsertVacantLandImageDto> dtodata = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "Fencing is mandatory",
                        response = dtodata
                    };
                    return Ok(apiResponseDetails);
                }
                else if (dto.Ddaboard == "" || dto.Ddaboard == null)
                {
                    List<ApiInsertVacantLandImageDto> dtodata = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "DDA Board is mandatory",
                        response = dtodata
                    };
                    return Ok(apiResponseDetails);
                }
                else if (dto.ScurityGuard == "" || dto.ScurityGuard == null)
                {
                    List<ApiInsertVacantLandImageDto> dtodata = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "Security Guard is mandatory",
                        response = dtodata
                    };
                    return Ok(apiResponseDetails);
                }
                else if (dto.IsExistanceEncroachment == "" || dto.IsExistanceEncroachment == null)
                {
                    List<ApiInsertVacantLandImageDto> dtodata = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "Certified that the plot is 100% encroachment free is mandatory",
                        response = dtodata
                    };
                    return Ok(apiResponseDetails);
                }
                else if (dto.IsExistanceEncroachment.ToUpper() == "N" || dto.IsExistanceEncroachment.ToUpper() == "NO" && (dto.PerEncroached == "" || dto.PerEncroached ==null))
                {
                    List<ApiInsertVacantLandImageDto> dtodata = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "Plot Encroached is mandatory",
                        response = dtodata
                    };
                    return Ok(apiResponseDetails);
                }
                else if (dto.IsExistanceEncroachment.ToUpper() == "N" || dto.IsExistanceEncroachment.ToUpper() == "NO" && (dto.AreaEncroached == "" || dto.AreaEncroached == null))
                {
                    List<ApiInsertVacantLandImageDto> dtodata = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "Area Encroached is mandatory",
                        response = dtodata
                    };
                    return Ok(apiResponseDetails);
                }
                else if (dto.IsExistanceEncroachment.ToUpper() == "N" || dto.IsExistanceEncroachment.ToUpper() == "NO" && (dto.IsActionInitiated == "" || dto.IsActionInitiated == null))
                {
                    List<ApiInsertVacantLandImageDto> dtodata = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "Action initiated/taken for removal of Encroachment is mandatory",
                        response = dtodata
                    };
                    return Ok(apiResponseDetails);
                }
                else if (dto.Longitude == "" || dto.Longitude == null)
                {
                    List<ApiInsertVacantLandImageDto> dtodata = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "Longitude is mandatory",
                        response = dtodata
                    };
                    return Ok(apiResponseDetails);
                }
                else if (dto.Latitude == "" || dto.Latitude == null)
                {
                    List<ApiInsertVacantLandImageDto> dtodata = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "Latitude is mandatory",
                        response = dtodata
                    };
                    return Ok(apiResponseDetails); 
                }
                else if (dto.Remarks == "" || dto.Remarks == null)
                {
                    List<ApiInsertVacantLandImageDto> dtodata = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "Remarks/Comment is mandatory",
                        response = dtodata
                    };
                    return Ok(apiResponseDetails);
                }
                FileHelper fileHelper = new FileHelper();               
                var data = await _insertVacantLandImagesService.Create(dto);                
                if (dto.ImageData != null && dto.ImageData.Count>0)
                {
                    List<vacantlandlistimage> vacantlandlistimages = new List<vacantlandlistimage>();
                    for (int i = 0; i < dto.ImageData.Count; i++)
                    {
                        if (dto.ImageData[i] != null && dto.ImageData[i] != "")
                        {
                            var imagePath = Guid.NewGuid().ToString() + ".jpg";
                            if (!Directory.Exists(VacantLandImagePath))
                            {
                                DirectoryInfo directoryInfo = Directory.CreateDirectory(VacantLandImagePath);
                            }
                            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(dto.ImageData[i])))
                            {
                                using (Bitmap bm2 = new Bitmap(ms))
                                {
                                    bm2.Save(VacantLandImagePath + Guid.NewGuid().ToString() + ".jpg");
                                }
                            }
                            vacantlandlistimages.Add(new vacantlandlistimage()
                            {
                                
                                vacantlandimageId = dto.Id,
                                ImagePath = imagePath,
                                CreatedBy = dto.CreatedBy
                            });
                        }
                    }
                    foreach (var item in vacantlandlistimages)
                    {
                        data = await _insertVacantLandImagesService.SaveVacantlandlistimage(item);
                    }
                   
                }
                if (data == true)
                {

                    List<ApiInsertVacantLandImageDto> dtoData = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "Record saved successfully!",
                        response = dtoData
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    List<ApiInsertVacantLandImageDto> dtoData = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        response = dtoData
                    };
                    return Ok(apiResponseDetails);
                }
            }
            else
            {
                List<ApiInsertVacantLandImageDto> dtoData = new List<ApiInsertVacantLandImageDto>();
                apiResponseDetails = new ApiInsertVacantLandImageResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    response = dtoData
                };
                return Ok(apiResponseDetails);
            }
        }



    }
}
