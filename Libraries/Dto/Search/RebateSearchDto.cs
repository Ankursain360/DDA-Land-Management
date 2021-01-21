using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class RebateSearchDto : BaseSearchDto
    {
        public string RebateOn { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string RebatePercentage { get; set; }
    }
}
