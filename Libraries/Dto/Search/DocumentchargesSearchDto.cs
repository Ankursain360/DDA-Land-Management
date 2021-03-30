using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
   public class DocumentchargesSearchDto : BaseSearchDto
   
    {
        public string name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string sub { get; set; }

    }
}
