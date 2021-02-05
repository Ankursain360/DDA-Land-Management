using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{

    public class FileStatusReportSearchDto : BaseSearchDto
    {
        //public int Userprofile { get; set; }
        public int Department { get; set; }
        //public int Branch { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}

