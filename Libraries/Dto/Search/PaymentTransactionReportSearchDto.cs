using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{

    public class PaymentTransactionReportSearchDto : BaseSearchDto
    {
        public int localityId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}
