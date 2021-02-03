using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class DisplayLabelSearchDto : BaseSearchDto
    {
        public string fileNo { get; set; }
        public string name { get; set; }
        //public int? almirah { get; set; }
        //public int? row { get; set; }
        //public int? column { get; set; }
        //public int? bundle { get; set; }
    }
}
