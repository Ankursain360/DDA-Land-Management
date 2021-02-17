using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{

    public class VillageDetailsKhasraWiseReportSearchDto : BaseSearchDto
    {
        public int villageId { get; set; }
        public int Name { get; set; }
    }
}
