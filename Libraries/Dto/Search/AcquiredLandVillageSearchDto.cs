using Dto.Common;

namespace Dto.Search
{
    public class AcquiredLandVillageSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string tehsil { get; set; }
        public string district { get; set; }
    }
}
