using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class PaymentTransactionReportListDto
    {
        public string FileNo { get; set; }
        public string Locality { get; set; }
        public string PayeeName { get; set; }
        public string PropertyNo { get; set; }
        public string Amountpaid { get; set; }
        public string TransactionId { get; set; }
        public string BankReference { get; set; }
        public string BankName { get; set; }
        public DateTime PaymentDate {get;set;}
        public string PaymentMode { get; set; }
    }
}
