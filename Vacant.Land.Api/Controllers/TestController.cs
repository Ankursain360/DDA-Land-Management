using AutoMapper.Configuration;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacant.Land.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IPropertyRegistrationService _propertyRegistrationService;
        public IConfiguration _configuration;
        public TestController(IPropertyRegistrationService propertyRegistrationService,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _propertyRegistrationService = propertyRegistrationService;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/test/GetDepartment")]
        public async Task<IEnumerable<Department>> GetDepartment()
        {
            return await _propertyRegistrationService.GetDepartmentDropDownList();
        }
    }
}
