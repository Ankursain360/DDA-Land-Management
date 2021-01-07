using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    
     public class LocalitySearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string localityCode { get; set; }
        public string landmark { get; set; }
        public string address { get; set; }
    }
}
