using Dto.Common;
using System;
using System.Collections.Generic;

namespace Dto.Master
{
    public class ApiDepartmentListDto
    {
        public int DEPTID { get; set; }
        public string DEPT_NAME { get; set; }
        public DateTime CREATIONDATE { get; set; }
        public int CREATEDBY { get; set; }
    }

    public class ApiResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiDepartmentListDto> response { get; set; }
    }
}
