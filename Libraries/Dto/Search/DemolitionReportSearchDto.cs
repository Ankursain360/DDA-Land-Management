using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
   public class DemolitionReportSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public DateTime? FromDate  { get; set; }
        public DateTime? ToDate { get; set; }
        public int localityId { get; set; }


    }
}
