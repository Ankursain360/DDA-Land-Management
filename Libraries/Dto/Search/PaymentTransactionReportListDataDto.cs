using Dto.Common;
using System;

namespace Dto.Search
{
    public class PaymentTransactionReportListDataDto
    {
        public int Id { get; set; }
        public string FileNo { get; set; }
       
        public string LocalityName { get; set; }
        public string PropertyNo { get; set; }
        public string PayeeName { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMode { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}