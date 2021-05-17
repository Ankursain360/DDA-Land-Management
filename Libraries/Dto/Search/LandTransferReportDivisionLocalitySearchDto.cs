using Dto.Common;

namespace Dto.Search
{
    public class LandTransferReportDivisionLocalitySearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int? departmentId { get; set; }
        public int? zoneId { get; set; }
        public int? divisionId { get; set; }
        public int? localityId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}