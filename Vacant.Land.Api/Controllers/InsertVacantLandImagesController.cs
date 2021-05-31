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
                FileHelper fileHelper = new FileHelper();
                dto.ImagePath = Guid.NewGuid().ToString() + ".jpg";
                if (!Directory.Exists(VacantLandImagePath))
                {
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(VacantLandImagePath);
                }
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(dto.ImageData)))
                {
                    using (Bitmap bm2 = new Bitmap(ms))
                    {
                        bm2.Save(VacantLandImagePath + Guid.NewGuid().ToString() + ".jpg");
                    }
                }
                var data = await _insertVacantLandImagesService.Create(dto);
                if (data == true)
                {

                    List<ApiInsertVacantLandImageDto> dtoData = new List<ApiInsertVacantLandImageDto>();
                    apiResponseDetails = new ApiInsertVacantLandImageResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        ApiInsertVacantLandImageDto = dtoData
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
                        ApiInsertVacantLandImageDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            else
            {
                List<ApiInsertVacantLandImageDto> dtoData = new List<ApiInsertVacantLandImageDto>();
                apiResponseDetails = new ApiInsertVacantLandImageResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    ApiInsertVacantLandImageDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }



    }
}
