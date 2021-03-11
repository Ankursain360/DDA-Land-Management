using Dto.Common;

namespace Dto.Search
{
    public class DocumentChecklistSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int serviceId { get; set; }
    }
}
