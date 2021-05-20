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
            var data = await _propertyRegistrationService.GetDepartmentDropDownList();
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
                return NotFound(apiResponseDetails);
            }
        }
        [HttpPost]
        [Route("[action]")]
        [Route("api/DepartmentAPI/GetZone")]
        public async Task<IActionResult> GetZone(int id)
        {
            ApiZoneResponseDetails apiResponseDetails = new ApiZoneResponseDetails();
            List<ApiZoneListDto> dtoData = new List<ApiZoneListDto>();
            if (id != 0)
            {
                var data = await _propertyRegistrationService.GetZoneDropDownList(id);
                if (data != null)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        dtoData.Add(new ApiZoneListDto()
                        {
                            ZONEID = data[i].Id,
                            ZONE = data[i].Name,
                            CREATIONDATE = data[i].CreatedDate,
                            CREATEDBY = data[i].CreatedBy,
                        });
                    }

                    apiResponseDetails = new ApiZoneResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        ApiZoneListDto = dtoData
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    apiResponseDetails = new ApiZoneResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        ApiZoneListDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            else
            {
                apiResponseDetails = new ApiZoneResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    ApiZoneListDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/DepartmentAPI/GetDivision")]
        public async Task<IActionResult> GetDivision(int id)
        {
            ApiDivisionResponseDetails apiResponseDetails = new ApiDivisionResponseDetails();
            List<ApiDivisionListDto> dtoData = new List<ApiDivisionListDto>();
            if (id != 0)
            {
                var data = await _propertyRegistrationService.GetDivisionDropDownList(id);
                if (data != null)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        dtoData.Add(new ApiDivisionListDto()
                        {
                            DIVSIONID = data[i].Id,
                            DIVISION_CONTACT = data[i].Name,
                            CREATIONDATE = data[i].CreatedDate,
                            CREATEDBY = data[i].CreatedBy,
                        });
                    }

                    apiResponseDetails = new ApiDivisionResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "details fetched successfully",
                        ApiDivisionListDto = dtoData
                    };

                    return Ok(apiResponseDetails);
                }
                else
                {
                    apiResponseDetails = new ApiDivisionResponseDetails
                    {
                        responseCode = "404",
                        responseMessage = " details not found",
                        ApiDivisionListDto = dtoData
                    };
                    return NotFound(apiResponseDetails);
                }
            }
            else
            {
                apiResponseDetails = new ApiDivisionResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    ApiDivisionListDto = dtoData
                };
                return NotFound(apiResponseDetails);
            }
        }
    }
}
