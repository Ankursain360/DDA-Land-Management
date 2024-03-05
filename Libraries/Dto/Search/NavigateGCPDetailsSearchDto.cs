using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class NavigateGCPDetailsSearchDto : BaseSearchDto
    {
        public string Zone { get; set; }
        public string village { get; set; }
        public string gisLabel { get; set; }
        
    }
}
 