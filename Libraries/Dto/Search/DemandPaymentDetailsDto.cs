using Dto.Common;


namespace Dto.Search
{
    public class DemandPaymentDetailsDto : BaseSearchDto
    {
        public string DemandPeriod { get; set; }
       
        public  decimal GroundRentLeaseRent { get; set; }
       
        public  decimal InterestAmount { get; set; }
      
        public  decimal TotalDues { get; set; }

        public int KycId { get; set; }


    }
}
