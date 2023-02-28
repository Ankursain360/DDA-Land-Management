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
    public class DepartmentAPIController : ControllerBase
    {
        private readonly IPropertyRegistrationService _propertyRegistrationService;
        public IConfiguration _configuration;
        public DepartmentAPIController(IPropertyRegistrationService propertyRegistrationService,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _propertyRegistrationService = propertyRegistrationService;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/DepartmentAPI/GetDepartment")]
        public async Task<IActionResult> GetDepartment()
        {
            ApiResponseDetails apiResponseDetails = new ApiResponseDetails();
            List<ApiDepartmentListDto> dtoData = new List<ApiDepartmentListDto>();
            var data = await _propertyRegistrationService.GetDepartmentDropDownListForApi();
            if (data != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    dtoData.Add(new ApiDepartmentListDto()
                    {
                        DEPTID = data[i].Id,
                        DEPT_NAME = data[i].Name,
                        CREATIONDATE = data[i].CreatedDate,
                        CREATEDBY = data[i].CreatedBy,
                    });
                }

                apiResponseDetails = new ApiResponseDetails
                {
                    responseCode = "200",
                    responseMessage = "details fetched successfully",
                    ApiDepartmentListDto = dtoData
                };

                return Ok(apiResponseDetails);
            }
            else
            {
                apiResponseDetails = new ApiResponseDetails
                {
                    responseCode = "404",
                    responseMessage = " details not found",
                    ApiDepartmentListDto = dtoData
                };
                return Ok(apiResponseDetails);
            }
        }
        [HttpGet]
        [Route("[action]")]
        [Route("api/DepartmentAPI/GetZone")]
        public async Task<IActionResult> GetZone(int id)
        {
            ApiZoneResponseDetails apiResponseDetails = new ApiZoneResponseDetails();
            List<ApiZoneListDto> dtoData = new List<ApiZoneListDto>();
            if (id != 0)
            {
                var data = await _propertyRegistrationService.GetZoneDropDownListForApi(id);
                if (data != null)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        dtoData.Add(new ApiZoneListDto()
                        {
                            ZONEID = data[i].Id,
                            ZONE = data[i].Name,
                            CODE = data[i].Code,
                            CREATIONDATE = data[i].CreatedDate,
                            CREATEDBY = data[i].CreatedBy,
                        });
                    }

                    apiResponseDetails = new ApiZoneResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        response = dtoData
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    apiResponseDetails = new ApiZoneResponseDetails
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
                apiResponseDetails = new ApiZoneResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    response = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/DepartmentAPI/GetDivision")]
        public async Task<IActionResult> GetDivision(int id)
        {
            ApiDivisionResponseDetails apiResponseDetails = new ApiDivisionResponseDetails();
            List<ApiDivisionListDto> dtoData = new List<ApiDivisionListDto>();
            if (id != 0)
            {
                var data = await _propertyRegistrationService.GetDivisionDropDownListForApi(id);
                if (data != null)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        dtoData.Add(new ApiDivisionListDto()
                        {
                            DIVSIONID = data[i].Id,
                            DIVISION_CONTACT = data[i].Name,
                            CODE = data[i].Code,
                            CREATIONDATE = data[i].CreatedDate,
                            CREATEDBY = data[i].CreatedBy,
                        });
                    }

                    apiResponseDetails = new ApiDivisionResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        response = dtoData
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    apiResponseDetails = new ApiDivisionResponseDetails
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
                apiResponseDetails = new ApiDivisionResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    response = dtoData
                };
                return Ok(apiResponseDetails);
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/DepartmentAPI/GetPrimaryList")]
        public async Task<IActionResult> GetPrimaryList(int deptid, int zoneid , int divisionid)
        {
            ApiPrimaryListResponseDetails apiResponseDetails = new ApiPrimaryListResponseDetails();
            List<ApiPrimaryListDto> dtoData = new List<ApiPrimaryListDto>();
            if (deptid != 0 && zoneid != 0 && divisionid != 0)
            {
                var data = await _propertyRegistrationService.GetPrimaryListForAPI(deptid, zoneid, divisionid);
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        dtoData.Add(new ApiPrimaryListDto()
                        {
                            Id = data[i].Id,
                            Name = data[i].PrimaryListNo
                        });
                    }

                    apiResponseDetails = new ApiPrimaryListResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        response = dtoData
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    apiResponseDetails = new ApiPrimaryListResponseDetails
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
                apiResponseDetails = new ApiPrimaryListResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    response = dtoData
                };
                return Ok(apiResponseDetails);
            }
        }


        [HttpGet]
        [Route("[action]")]
        [Route("api/DepartmentAPI/GetPropertyDetail")]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            ApiPrimaryResponseDetails apiPrimaryResponse = new ApiPrimaryResponseDetails();
            GetPropertyDetailDto getPropertyDetail = new GetPropertyDetailDto();
            if (id != 0)
            {
                var data = await _propertyRegistrationService.GetPropertyregistrationDetail(id);
                if (data != null)
                {

                getPropertyDetail = new GetPropertyDetailDto
                {
                    uniqueId = data.Id,
                    locality = data.Locality.Name,
                    landUse = data.MainLandUse.Name,
                    layoutPlan = data.LayoutFilePath == null || data.LayoutFilePath ==""?"No":"Yes"
                };
                apiPrimaryResponse = new ApiPrimaryResponseDetails
                {
                    responseCode = "200",
                    responseMessage = "details fetched successfully",
                    response = getPropertyDetail
                };
                return Ok(apiPrimaryResponse);
                }
              else
              {
                apiPrimaryResponse = new ApiPrimaryResponseDetails
                {
                    responseCode = "404",
                    responseMessage = " details not found",
                    response = getPropertyDetail
                };
                return Ok(apiPrimaryResponse);
              }
            }
            else
            {
                apiPrimaryResponse = new ApiPrimaryResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    response = getPropertyDetail
                };
                return Ok(apiPrimaryResponse);
            }

        }
    }
}
