using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PropertyRegisterationSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int departmentId { get; set; }
        public int zoneId { get; set; }
        public int divisionId { get; set; }

        public int inventoriedId { get; set; }
        public int classificationOfLandId { get; set; }
        public string plannedUnplannedLand { get; set; }
        public int Id { get; set; }
    }
}
