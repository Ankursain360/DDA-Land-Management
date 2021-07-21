using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PaymentverificationandReconciliationDto : BaseSearchDto
    {

        public int Id { get; set; }

        public string FileNo { get; set; }
        public string PayeeName { get; set; }
        public string PropertyNo { get; set; }

        public decimal? AmountPaid { get; set; }
        public decimal? InterestPaid { get; set; }
        public decimal? TotalAmount { get; set; }
        public string TransactionId { get; set; }
        public string BankTransactionId { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }

        public string verified { get; set; }

        public string Status { get; set; }
    }
}
