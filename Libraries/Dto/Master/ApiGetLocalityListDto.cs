
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class ApiGetLocalityListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public byte? IsActive { get; set; }
    }

    public class ApiGetLocalityListDtoResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiGetLocalityListDto> ApiGetLocalityListDto { get; set; }
    }
}
