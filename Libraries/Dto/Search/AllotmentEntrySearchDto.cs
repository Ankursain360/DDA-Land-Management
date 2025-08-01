﻿using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class AllotmentEntrySearchDto : BaseSearchDto
    {
        public string applicantname { get; set; }
        public string Lease { get; set; }
        public string RefNo { get; set; }

        // public DateTime AllotmentDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
