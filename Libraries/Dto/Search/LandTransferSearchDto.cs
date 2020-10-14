using Dto.Common;

namespace Dto.Search
{
    public class LandTransferSearchDto: BaseSearchDto
    {
        public string name { get; set; }
        public int? reportType { get; set; }
        public int? departmentId { get; set; }
        public int? zoneId { get; set; }
        public int? divisionId { get; set; }
        public int? localityId { get; set; }
    }
}