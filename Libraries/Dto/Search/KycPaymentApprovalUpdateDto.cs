

using Dto.Common;
using System;
using System.Collections.Generic;

namespace Dto.Search
{
    public class KycPaymentApprovalUpdateDto : BaseSearchDto
    {
        public int KycId { get; set; }
        public int DemandPaymentId { get; set; }
        public string DemandPeriod { get; set; }
        public decimal? GroundRent { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? TotdalDues { get; set; }
        public decimal? TotalPayable{ get; set; }
        public decimal? TotalPayableInterest { get; set; }
        public decimal? TotalPayableDues { get; set; }

    } 
}
