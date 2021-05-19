using Dto.Common;
using System;
using System.Collections.Generic;

namespace Dto.Master
{
    public class ApiResponseDetailssssss
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiDepartmentListDto> ApiDepartmentListDto { get; set; }
    }
}
