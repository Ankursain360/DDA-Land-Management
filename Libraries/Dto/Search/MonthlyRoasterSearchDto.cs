using Dto.Common;

namespace Dto.Search
{
    public class MonthlyRoasterSearchDto : BaseSearchDto
    {
        public string department { get; set; }
        public string zone { get; set; }
        public string division { get; set; }
        public string locality { get; set; }
        public string guard { get; set; }
        public string year { get; set; }
    }
}
