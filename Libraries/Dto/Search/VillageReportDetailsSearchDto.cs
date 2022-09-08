using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class VillageReportDetailsSearchDto : BaseSearchDto
    {
        public int VillageId { get; set; }
        public string VillageSmallMap { get; set; }
        public string VillageFullMap { get; set; } 
        public List<string> VillageMassaviesMap { get; set; }
    }
}
