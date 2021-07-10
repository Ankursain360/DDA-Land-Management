
using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PaymentLedgerSearchDto : BaseSearchDto
    {
        public int AllotmentId { get; set; }
       
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
