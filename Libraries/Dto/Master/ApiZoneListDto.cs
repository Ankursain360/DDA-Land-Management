using Dto.Common;
using System;
using System.Collections.Generic;

namespace Dto.Master
{
    public class ApiZoneListDto
    {
        public int ZONEID { get; set; }
        public string ZONE { get; set; }
        public string CODE { get; set; }
        public DateTime CREATIONDATE { get; set; }
        public int CREATEDBY { get; set; }
    }

    public class ApiZoneResponseDetails 
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiZoneListDto> ApiZoneListDto { get; set; }
    }
}
