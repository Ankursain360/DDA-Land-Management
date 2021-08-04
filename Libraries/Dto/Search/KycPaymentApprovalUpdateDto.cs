

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
        public string GroundRent { get; set; }
        public string InterestRate { get; set; }
        public string TotdalDues { get; set; }

    }
    public class PaymentListData
    {        
        public List<KycPaymentApprovalUpdateDto> jsondata { get; set; }
    }
    public class PaymentdataDTO
    {
        public int KycId { get; set; }
        public string jsondata { get; set; }
        //public List<KycPaymentApprovalUpdateDto> dto { get; set; }
    }
}
