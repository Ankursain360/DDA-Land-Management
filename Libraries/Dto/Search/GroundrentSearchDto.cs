using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class GroundrentSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }

    }
}
