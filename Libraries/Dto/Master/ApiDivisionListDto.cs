using Dto.Common;
using System;
using System.Collections.Generic;

namespace Dto.Master
{
    public class ApiDivisionListDto
    {
        public int DIVSIONID { get; set; }
        public string DIVISION_CONTACT { get; set; }
        public string CODE { get; set; }
        public DateTime CREATIONDATE { get; set; }
        public int CREATEDBY { get; set; }
    }

    public class ApiDivisionResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiDivisionListDto> ApiDivisionListDto { get; set; }
    }
}
