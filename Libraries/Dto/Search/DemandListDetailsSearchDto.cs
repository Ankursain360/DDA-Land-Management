using Dto.Common;

namespace Dto.Search
{
    public class DemandListDetailsSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int villageId { get; set; }
        public string demandlistno { get; set; }
        public int KhasraId { get; set; }
    }
}
