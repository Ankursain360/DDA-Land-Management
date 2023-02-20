using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class GetPropertyDetailDto
    {
        public int Id { get; set; }
        public string Locality { get; set; }
        public string LandUse { get; set; }
        public string LayoutPlan { get; set; }
    }
    public class ApiPrimaryResponseDetails 
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public GetPropertyDetailDto getPropertyDetailDto { get; set; } 

    }
}
