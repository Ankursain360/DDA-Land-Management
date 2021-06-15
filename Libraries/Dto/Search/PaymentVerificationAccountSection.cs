using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PaymentVerificationAccountSection : BaseSearchDto
    {
     
        public string name { get; set; }
        public Byte IsVerified { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
    }
}
