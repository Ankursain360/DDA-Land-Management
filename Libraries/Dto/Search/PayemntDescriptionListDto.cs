using Dto.Common;
using System;

namespace Dto.Search
{
   public class PayemntDescriptionListDto : AuditableDto<int>
    {
        public DateTime PYear { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Amount { get; set; }
        public string RecieptNo { get; set; }
        public string TransactionType { get; set; }
        public string Name { get; set; }
       
    }
}
