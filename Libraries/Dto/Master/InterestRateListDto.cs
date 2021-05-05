using Dto.Common;

namespace Dto.Master
{
   public class InterestRateListDto
    {
        public int Id { get; set; }

        public string PropertyType { get; set; }

        public string InterestRate { get; set; }

       

        public string FromDate { get; set; }

        public string ToDate { get; set; }
        public string LateInterestRate { get; set; }

        public string Status { get; set; }
    }
}
