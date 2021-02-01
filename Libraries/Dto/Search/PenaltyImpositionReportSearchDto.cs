using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PenaltyImpositionReportSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int? fileNo { get; set; }
        public int? locality { get; set; }
    }
}
