using Dto.Common;

namespace Dto.Search
{
    public class ManualPaymentSearchDto : BaseSearchDto
    {
        public string fileno { get; set; }
        public string payeeName { get; set; }
    }
}
