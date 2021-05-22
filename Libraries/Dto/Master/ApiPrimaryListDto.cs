using Dto.Common;
using System;
using System.Collections.Generic;

namespace Dto.Master
{
    public class ApiPrimaryListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ApiPrimaryListResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiPrimaryListDto> ApiPrimaryListDto { get; set; }
    }
}
