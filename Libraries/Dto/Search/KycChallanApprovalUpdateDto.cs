


using Dto.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Dto.Search
{
    public class KycChallanApprovalUpdateDto : BaseSearchDto
    {
        public int KycId { get; set; }
        public int DemandPaymentId { get; set; }
        public string IsVerified { get; set; }
        public string PaymentType { get; set; }
        public string Period { get; set; }
        public string ChallanNo { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DateofPaymentByAllottee { get; set; }
        public string Proofinpdf { get; set; }
        public string Ddabankcredit { get; set; }
        public decimal? TotalPayable { get; set; }
        public decimal? TotalPayableInterest { get; set; }
        public decimal? TotalPayableDues { get; set; }
       
    }
}

