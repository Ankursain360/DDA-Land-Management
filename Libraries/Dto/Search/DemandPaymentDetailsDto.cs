using Dto.Common;
using System;

namespace Dto.Search
{
    public class DemandPaymentDetailsDto : BaseSearchDto
    {
      
       
        public  decimal GroundRentLeaseRent { get; set; }
       
        public  decimal InterestAmount { get; set; }
      
        public  decimal TotalDues { get; set; }

        public int KycId { get; set; }

        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }

        public decimal? amount { get; set; }
        
        public int days { get; set; }
               
 
    }
}
