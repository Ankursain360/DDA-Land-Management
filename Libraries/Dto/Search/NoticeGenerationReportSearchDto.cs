using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    
    public class NoticeGenerationReportSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int FileNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
