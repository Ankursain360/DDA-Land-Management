using Dto.Common;
using System;

namespace Dto.Search
{
    public class ViewPaymentHistoryListDataDto
    {
        public int Id { get; set; }
        public int LeasePaymentTypeId { get; set; }
        public string LeasePaymentType { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public Decimal DueAmount { get; set; }
        public Decimal Interest { get; set; }
        public Decimal TotalPayableAmount { get; set; }
        public string Status { get; set; }
    }
}