using Dto.Common;

namespace Dto.Search
{
    public class CourtSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string address { get; set; }
        public string phoneno { get; set; }
    }
}
