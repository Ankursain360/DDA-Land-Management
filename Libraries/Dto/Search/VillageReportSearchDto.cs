using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    
        public class VillageReportSearchDto : BaseSearchDto
    {
        public int Name { get; set; }
        public string village { get; set; } 
    }
}
