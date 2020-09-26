using Core.Enum;

namespace Dto.Common
{
    public class BaseSearchDto
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public SortOrder SortOrder { get; set; }
        public string SortBy { get; set; }
    }
}
