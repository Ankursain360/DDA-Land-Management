using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class MorLandsSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string propertyname { get; set; }
        public string sitedesc { get; set; }
    }
}
