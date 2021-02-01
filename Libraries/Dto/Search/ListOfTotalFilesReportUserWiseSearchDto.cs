using Dto.Common;

namespace Dto.Search
{
   public class ListOfTotalFilesReportUserWiseSearchDto : BaseSearchDto
    {
        public int name { get; set; }
        public string searchCol { get; set; }

        public string searchText { get; set; }
    }
}
