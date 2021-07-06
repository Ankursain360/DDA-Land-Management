using System;
using System.Collections.Generic;
using System.Text;
using Dto.Common;

namespace Dto.Search
{


    public class UserWiseLandStatusReportSearchDto : BaseSearchDto
    {

        public string Name { get; set; }
        public int? RoleId { get; set; }
        public int? departmentId { get; set; }
        public int? zoneId { get; set; }
        public int? divisionId { get; set; }
  
        public int Id { get; set; }
    }
}
