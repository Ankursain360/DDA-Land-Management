using System;
using System.Collections.Generic;
using System.Text;
using Dto.Common;

namespace Dto.Search
{
    public class PaymentTranscationReportDto : BaseSearchDto
    {
       
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
