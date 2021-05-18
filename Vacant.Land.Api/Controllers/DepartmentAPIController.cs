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
            var data= await _propertyRegistrationService.GetDepartmentDropDownList();
            return Ok(data);
        }
        [HttpGet]
        [Route("[action]")]
        [Route("api/DepartmentAPI/GetZone")]
        public async Task<IActionResult> GetZone()
        {
            var data = await _propertyRegistrationService.GetDepartmentDropDownList();
            return NotFound();
            //return Ok(data);
        }
    }
}
