using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PlanningSearchDto:BaseSearchDto
    {
        public string unplannedname { get; set; }
        public string plannedname { get; set; }
    }
}
