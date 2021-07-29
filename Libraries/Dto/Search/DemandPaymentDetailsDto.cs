using Dto.Common;


namespace Dto.Search
{
    public class DemandPaymentDetailsDto : BaseSearchDto
    {
        public string DemandPeriod { get; set; }
       
        public string GroundRentLeaseRent { get; set; }
       
        public string InterestAmount { get; set; }
      
        public string TotalDues { get; set; }
     
        public string FileNo { get; set; }

    }
}
