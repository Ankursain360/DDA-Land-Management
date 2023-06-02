using Dto.Master;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Renci.SshNet.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Dto.Master.VillageAndKhasraBiseReport_ApiDto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Vacant.Land.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AcquiredlandvillageApiController : ControllerBase
    {
        private readonly IPossessiondetailsService _service;
        private readonly IConfiguration _configuration;
        string un4documentPath = "";
        string un4LivePath = "";
        string un6documentPath = "";
        string un6LivePath = "";
        string un17documentPath = "";
        string un17LivePath = "";
        string un22documentPath = "";
        string un22LivePath = "";
        string AwarddocumentPath = "";
        string AwardLivePath = "";
        string possessiondocumentPath = "";
        string possessionLivePath = "";


        public AcquiredlandvillageApiController(IPossessiondetailsService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
            un4documentPath = _configuration.GetSection("FilePaths:US4:DocumentFIlePath").Value.ToString();
            un4LivePath = _configuration.GetSection("FilePaths:US4:LivePath").Value.ToString();
            un6documentPath = _configuration.GetSection("FilePaths:US6:DocumentFIlePath").Value.ToString();
            un6LivePath = _configuration.GetSection("FilePaths:US6:LivePath").Value.ToString();
            un17documentPath = _configuration.GetSection("FilePaths:US17:DocumentFIlePath").Value.ToString();
            un17LivePath = _configuration.GetSection("FilePaths:US6:LivePath").Value.ToString();
            un22documentPath = _configuration.GetSection("FilePaths:US22:DocumentFIlePath").Value.ToString();
            un22LivePath = _configuration.GetSection("FilePaths:US6:LivePath").Value.ToString();
            AwarddocumentPath = _configuration.GetSection("FilePaths:AwardMaster:DocumentFIlePath").Value.ToString();
            AwardLivePath = _configuration.GetSection("FilePaths:US6:LivePath").Value.ToString();
            possessiondocumentPath = _configuration.GetSection("FilePaths:Possesion:DocumentFIlePath").Value.ToString();
            possessionLivePath = _configuration.GetSection("FilePaths:US6:LivePath").Value.ToString();
        }
        [HttpPost]
        [Route("[action]")]
        [Route("api/AcquiredlandvillageApi/GetAcquiredlandvillage")]
        public async Task<IActionResult> GetAcquiredlandvillage([FromBody] AcquiredlandvillageApiDto model)
        {
            AcquiredlandvillageResponseDetails acquiredlandvillageResponse = new AcquiredlandvillageResponseDetails();
            List<AcquiredlandvillageApiDto> dtoList = new List<AcquiredlandvillageApiDto>();
            if (model.ZoneId == 0 || model.ZoneId < 0)
            {
                acquiredlandvillageResponse = new AcquiredlandvillageResponseDetails
                {
                    responseCode = "403",
                    responseMessage = "Please Insert ZoneId/Valid ZoneId",
                    response = dtoList
                };

                return Ok(acquiredlandvillageResponse);
            }
            var data = await _service.GetAllVillage(model);
            if (data != null && data.Count != 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    dtoList.Add(new AcquiredlandvillageApiDto()
                    {
                        ZoneId = data[i].ZoneId??0,
                        villageID = data[i].Id,
                        village_NAME = data[i].Name,
                    });
                }
                acquiredlandvillageResponse = new AcquiredlandvillageResponseDetails
                {
                    responseCode = "200",
                    responseMessage = "details fetched successfully",
                    response = dtoList
                };

                return Ok(acquiredlandvillageResponse);
            }

            else
            {
                acquiredlandvillageResponse = new AcquiredlandvillageResponseDetails
                {
                    responseCode = "404",
                    responseMessage = "No Data Available",
                    response = dtoList
                };
                return Ok(acquiredlandvillageResponse);
            }

        }


        [HttpGet]
        [Route("[action]")]
        [Route("api/DepartmentAPI/GetKhasra")]
        public async Task<IActionResult> GetKhasra(int id)
        {
            AcquiredlandKhasraResponseDetails khasraResponseDetails = new AcquiredlandKhasraResponseDetails();
            List<AcquiredlandKhasraApiDto> dtoData = new List<AcquiredlandKhasraApiDto>();
            if (id != 0)
            {
                var data = await _service.BindKhasra(id);
                if (data != null)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        dtoData.Add(new AcquiredlandKhasraApiDto()
                        {
                            KhasraID = data[i].Id,
                            Khasra_NAME = data[i].Name,

                        });
                    }

                    khasraResponseDetails = new AcquiredlandKhasraResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        response = dtoData
                    };

                    return Ok(khasraResponseDetails);
                }
                else
                {
                    khasraResponseDetails = new AcquiredlandKhasraResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        response = dtoData
                    };
                    return Ok(khasraResponseDetails);
                }
            }
            else
            {
                khasraResponseDetails = new AcquiredlandKhasraResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    response = dtoData
                };
                return Ok(khasraResponseDetails);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/DepartmentAPI/GetVillageAndKhasraReport")]
        public async Task<IActionResult> GetVillageAndKhasraReport([FromBody] VillageAndKhasraDetailsSearchDto model)
        {
            VillageAndKhasraBiseReportResponseDetails villageAndKhasraResponse = new VillageAndKhasraBiseReportResponseDetails();
            List<VillageAndKhasraBiseReport_ApiDto> dtoData = new List<VillageAndKhasraBiseReport_ApiDto>();
            if (model.Khasraid != 0)
            {
                var data = await _service.GetPagedKhasraDetails(model);
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        dtoData.Add(new VillageAndKhasraBiseReport_ApiDto()
                        {
                            VillageName = data[i].VillageName,
                            Khasra_No = data[i].KhasraName,
                            AreaBhigha_Biswa_Biswana = data[i].Bigha + "-" + data[i].Biswa + "-" + data[i].Biswanshi.ToString(),
                            Notification_s_US_4 = data[i].un4Number /*+ "-" + data[i].um4Date*/,
                            um4Date = data[i].um4Date,
                            un4document = data[i].un4document == null ? "" : System.IO.File.Exists(un4documentPath + data[i].un4document) ? un4LivePath + data[i].un4document : "",
                            Notification_s_US_6 = data[i].un6Number,
                            um6Date = data[i].um6Date,
                            un6document = data[i].un6document == null ? "" :System.IO.File.Exists(un6documentPath + data[i].un6document) ? un6LivePath + data[i].un6document : "",
                            Notification_s_US_17 = data[i].un17Number /*+ "-" + data[i].um17Date*/,
                            um17Date = data[i].um17Date,
                            un17document = data[i].un17document == null ? "" : System.IO.File.Exists(un17documentPath + data[i].un17document)?un17LivePath + data[i].un17document : "",
                            Notification_s_US_22 = data[i].un22Number /*+ "-" + data[i].un22Date*/,
                            un22Date = data[i].un22Date,
                            un22document = data[i].un22document == null ? "" : System.IO.File.Exists(un22documentPath + data[i].un22document)?un22LivePath + data[i].un22document:"",
                            AwardsNumber = data[i].AwardNumber/* + data[i].AwardDate*/,
                            AwardDate = data[i].AwardDate,
                            Awarddocument = data[i].Awarddocument == null ? "" : System.IO.File.Exists(AwarddocumentPath + data[i].Awarddocument)?AwardLivePath + data[i].Awarddocument:"",
                            DateofPossesion = data[i].PossDate,
                            possessiondocument = data[i].possessiondocument == null ? "" : System.IO.File.Exists(possessiondocumentPath + "" + data[i].possessiondocument) ? possessionLivePath + data[i].possessiondocument :""
                        });
                    }
                    villageAndKhasraResponse = new VillageAndKhasraBiseReportResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        response = dtoData
                    };
                    return Ok(villageAndKhasraResponse);

                }
                else
                {
                    villageAndKhasraResponse = new VillageAndKhasraBiseReportResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        response = dtoData
                    };
                    return Ok(villageAndKhasraResponse);
                }

            }
            else
            {
                villageAndKhasraResponse = new VillageAndKhasraBiseReportResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    response = dtoData
                };
                return Ok(villageAndKhasraResponse);
            }

        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/AcquiredlandvillageApi/GetZoneList")]
        public async Task<IActionResult> GetZoneList()
        {
            ZoneApiResponseDetails apiResponseDetails = new ZoneApiResponseDetails();
            List<ZoneDtoForApi> dtoData = new List<ZoneDtoForApi>();
            var data = await _service.GetZoneListApi();
            if (data != null && data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    dtoData.Add(new ZoneDtoForApi()
                    {
                        ZONEID = data[i].Id,
                        ZONE = data[i].Name,

                    });
                }
                apiResponseDetails = new ZoneApiResponseDetails
                {
                    responseCode = "200",
                    responseMessage = "Zone Details Fetched Successfully",
                    response = dtoData
                };
                return Ok(apiResponseDetails);

            }
            else
            {
                apiResponseDetails = new ZoneApiResponseDetails
                {
                    responseCode = "404",
                    responseMessage = " Details Not Found",
                    response = dtoData
                };
                return Ok(apiResponseDetails);
            }

        }
    }
}
