using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacant.Land.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class VlmsmobileappaccesslogController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IVlmsmobileappaccesslogService _vlmsmobileappaccesslog;

        public VlmsmobileappaccesslogController(IConfiguration configuration, IVlmsmobileappaccesslogService vlmsmobileappaccesslog)
        {
            _configuration = configuration;
            _vlmsmobileappaccesslog = vlmsmobileappaccesslog;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        [Route("api/Vlmsmobileappaccesslog/SaveVLMSMobileApp")]
        public async Task<IActionResult> SaveVLMSMobileApp([FromBody] ApiSaveVlmsmobileappaccesslogDto dto)
        {
            ApiSaveVlmsmobileappaccesslogResponseDetails responseDetails = new ApiSaveVlmsmobileappaccesslogResponseDetails();
            if (dto != null && ModelState.IsValid)
            {
                if (dto.LoginStatus.ToUpper() == "T" || dto.LoginStatus.ToUpper() == "F")
                {               
                    var data = await _vlmsmobileappaccesslog.Create(dto);
                    if (data == true)
                    {
                        List<ApiSaveVlmsmobileappaccesslogDto> dtoData = new List<ApiSaveVlmsmobileappaccesslogDto>();
                        responseDetails = new ApiSaveVlmsmobileappaccesslogResponseDetails()
                        {
                            responseCode = "200",
                            responseMessage = "Record saved successfully!",
                            response = dtoData

                        };
                        return Ok(responseDetails);
                    }
                    else
                    {
                        List<ApiSaveVlmsmobileappaccesslogDto> dtoData = new List<ApiSaveVlmsmobileappaccesslogDto>();
                        responseDetails = new ApiSaveVlmsmobileappaccesslogResponseDetails()
                        {
                            responseCode = "404",
                            responseMessage = "Details Not Found",
                            response = dtoData

                        };
                        return Ok(responseDetails);
                    }
                }
                else
                {
                    List<ApiSaveVlmsmobileappaccesslogDto> dtoData = new List<ApiSaveVlmsmobileappaccesslogDto>();
                    responseDetails = new ApiSaveVlmsmobileappaccesslogResponseDetails()
                    {
                        responseCode = "404",
                        responseMessage = "Please insert only T/F in LoginStatus",
                        response = dtoData
                    };
                    return Ok(responseDetails);
                }
            }
            else
            {
                List<ApiSaveVlmsmobileappaccesslogDto> dtoData = new List<ApiSaveVlmsmobileappaccesslogDto>();
                responseDetails = new ApiSaveVlmsmobileappaccesslogResponseDetails()
                {
                    responseCode = "404",
                    responseMessage = "Bad Request. Insufficient Parameters",
                    response = dtoData

                };
                return Ok(responseDetails);
            }
            
        }
    }
}
