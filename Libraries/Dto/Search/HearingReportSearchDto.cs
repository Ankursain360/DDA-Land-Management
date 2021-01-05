using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{

    public class HearingReportSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public DateTime hearingDate { get; set; }
        public DateTime nextHearingDate { get; set; }
    }
}
