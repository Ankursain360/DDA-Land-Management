using Dto.Common;
using System;
namespace Dto.Search
{
   public class LeasePaymentDemandLetterDetailsSearchDto : BaseSearchDto
    {
        public string FileNo { get; set; }
        public string AllotteeName { get; set; }

        public string AllotteeAddress { get; set; }

        public string PropertyNo { get; set; }

        public int DatePeriodBy { get; set; }

        public decimal PayAmount { get; set; }
        public string DatePeriod { get; set; }

        public decimal? GroundRentLicense { get; set; }

       

    }
}
