using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class LandAcquisitionAwardsSearchDto : BaseSearchDto
    {
        public string village { get; set; }
        public string title { get; set; }
    }
}
