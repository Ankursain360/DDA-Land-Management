using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class ApiGetAreaUnitDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte IsActive { get; set; }
    }
        public class ApiGetAreaUnitResponseDetails
        {
            public string responseCode { get; set; }
            public string responseMessage { get; set; }
            public List<ApiGetAreaUnitDto> ApiGetAreaUnitUseDto { get; set; }
        }    
}
