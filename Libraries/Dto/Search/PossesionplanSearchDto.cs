using Dto.Common;

namespace Dto.Search
{
    public class PossesionplanSearchDto : BaseSearchDto
    {
        public string AllotmentId { get; set; }
        public string allotteename { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
