using Dto.Common;

namespace Dto.Search
{
    public class LandTransferSearchDto: BaseSearchDto
    {
        public string name { get; set; }
        public int inventoriedIn { get; set; }
        public int classificationofland { get; set; }
        public int department { get; set; }
        public int zone { get; set; }
        public int division { get; set; }
        public int locality { get; set; }
        public string plannedUnplannedLand { get; set; }
        public int mainLandUse { get; set; }
        public int litigation { get; set; }
        public int encroached { get; set; }
        public string khasraNo { get; set; }
        public string colony { get; set; }
        public string sector { get; set; }
        public string block { get; set; }
        public string pocket { get; set; }
        public string plotNo { get; set; }
        public int? reportType { get; set; }
        public int? departmentId { get; set; }
        public int? zoneId { get; set; }
        public int? divisionId { get; set; }
        public int? localityId { get; set; }
    }
}