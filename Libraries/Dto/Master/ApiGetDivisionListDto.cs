
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class ApiGetDivisionListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public byte? IsActive { get; set; }
    }

    public class ApiGetDivisionListDtoResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiGetDivisionListDto> ApiGetDivisionListDto { get; set; }
    }
}
