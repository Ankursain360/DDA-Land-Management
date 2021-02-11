using Dto.Common;

namespace Dto.Search
{
   public class WeeklyFileReportSearchDto : BaseSearchDto
    {
        public int DeptId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
