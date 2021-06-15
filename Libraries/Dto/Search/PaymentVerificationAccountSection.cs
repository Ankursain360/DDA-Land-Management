using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PaymentVerificationAccountSection : BaseSearchDto
    {
     
     //   public string name { get; set; }
        public int IsVerified { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public string colname { get; set; }
        public int orderby { get; set; }
    }
}
