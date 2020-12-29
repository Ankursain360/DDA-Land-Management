using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{

    public class HearingReportSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public DateTime HearingDate { get; set; }
        public DateTime NextHearingDate { get; set; }
    }
}
