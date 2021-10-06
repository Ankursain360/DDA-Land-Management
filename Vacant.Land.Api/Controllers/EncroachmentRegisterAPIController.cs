
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

namespace Vacant.Land.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EncroachmentRegisterAPIController : ControllerBase
    {
        private readonly IEncroachmentRegisterAPIService _encroachmentRegisterAPIService;
        public IConfiguration _configuration;
        public EncroachmentRegisterAPIController(IEncroachmentRegisterAPIService encroachmentRegisterAPIService,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _encroachmentRegisterAPIService = encroachmentRegisterAPIService;
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
        [Route("api/EncroachmentRegisterAPI/GetDivision")]
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

    }
}
