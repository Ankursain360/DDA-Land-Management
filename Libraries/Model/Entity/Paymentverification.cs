using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public partial class Paymentverification : AuditableEntity<int>
    {
      
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
        public byte? IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerifiedOn { get; set; }
      
        public byte? IsActive { get; set; }
    }
}
