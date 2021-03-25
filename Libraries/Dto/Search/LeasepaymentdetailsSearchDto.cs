using Dto.Common;

namespace Dto.Search
{
    public class LeasepaymentdetailsSearchDto : BaseSearchDto
    {
        public string AllotmentId { get; set; }
        public string Mode { get; set; }
        public string PaymentDate { get; set; }
        public string Number { get; set; }

    }
}
