using Dto.Master;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renci.SshNet.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Dto.Master.VillageAndKhasraBiseReport_ApiDto;

namespace Vacant.Land.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AcquiredlandvillageApiController : ControllerBase
    {
        private readonly IPossessiondetailsService _service;

        public AcquiredlandvillageApiController(IPossessiondetailsService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("[action]")]
        [Route("api/AcquiredlandvillageApi/GetAcquiredlandvillage")]
        public async Task<IActionResult> GetAcquiredlandvillage()
        {
            AcquiredlandvillageResponseDetails acquiredlandvillageResponse = new AcquiredlandvillageResponseDetails();
            List<AcquiredlandvillageApiDto> dtoList = new List<AcquiredlandvillageApiDto>();
            var data = await _service.GetAllVillage();
            if (data != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    dtoList.Add(new AcquiredlandvillageApiDto()
                    {
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
                    responseMessage = " details not found",
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

        [HttpGet]
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
                            AreaBhigha_Biswa_Biswana = data[i].Bigha +"-"+ data[i].Biswa + "-" + data[i].Biswanshi.ToString(),
                            Notification_s_US_4 = data[i].un4Number +"-"+ data[i].um4Date,
                            Notification_s_US_6 = data[i].un6Number + "-" + data[i].un6Date,
                            Notification_s_US_17 = data[i].un17Number + "-" + data[i].um17Date,
                            Notification_s_US_22 = data[i].un22Number + "-" + data[i].un22Date,
                            Awards = data[i].AwardNumber + data[i].AwardDate,
                            Date_of_Possesion = data[i].PossDate
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
    }
}
