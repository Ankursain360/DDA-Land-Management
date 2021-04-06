using Dto.Common;
using System;

namespace Dto.Search
{
    public class PaymentPremiumListDataDto
    {
        public int Id { get; set; }
        public string LeasePaymentType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Decimal Amount { get; set; }
    }
}