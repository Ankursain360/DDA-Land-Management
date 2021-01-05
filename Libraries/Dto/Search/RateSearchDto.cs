using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class RateSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string property { get; set; }
    }
}
