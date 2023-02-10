using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class LandAcquisitionAwardsDto: BaseSearchDto
    {
        public int Id { get; set; } 
        public string village { get; set; }
        public string title { get; set; }
    }
}
