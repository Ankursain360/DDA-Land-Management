using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class VillageReportDto
    {
        public string name { get; set; }
        public string yearofConsolidation { get; set; } 
        public int? totalNoOfSheet { get; set; } 
        public string circle { get; set; } 
        public string acquired { get; set; } 
    }
}
