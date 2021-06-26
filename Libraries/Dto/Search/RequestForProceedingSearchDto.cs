using Dto.Common;

namespace Dto.Search
{
   public class RequestForProceedingSearchDto : BaseSearchDto
    {
        public string letterReferenceNo { get; set; }
        public string AllotmentNo { get; set; }
        public string subject { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
