
using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class TimeextensionSearchDto : BaseSearchDto
    {
     
        public string FromDate { get; set; }
        public string ToDate { get; set; }
       
    }
}

