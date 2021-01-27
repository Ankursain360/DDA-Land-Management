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

       
        public string PaymentMode { get; set; }
        public DateTime CreatedDate { get; set; }


       
        public Decimal AmountPaid { get; set; }

       
      
        public Decimal PropertyArea { get; set; }
        public string DemandNo { get; set; }
        public DateTime DemandDate { get; set; }
        public string DemandAmount { get; set; }
        

    }
}