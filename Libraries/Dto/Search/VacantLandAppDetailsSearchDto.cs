using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class VacantLandAppDetailsSearchDto : BaseSearchDto
    {
        public int departmentId { get; set; }
        public int zoneId { get; set; }
        public int divisionId { get; set; }
        public int id { get; set; }
        public string path { get; set; }
    } 
}
