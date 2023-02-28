using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class GetPropertyDetailDto
    {
        public int uniqueId { get; set; }
        public string locality { get; set; }
        public string landUse { get; set; }
        public string layoutPlan { get; set; }
    }
    public class ApiPrimaryResponseDetails 
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public GetPropertyDetailDto response { get; set; } 

    }
}
