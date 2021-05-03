using Dto.Common;

namespace Dto.Master
{
   public class RestorePropertyReportListDto
    {
        public int Id { get; set; }
        public string InventoriedIn { get; set; }
        public string PlannedUnplannedLand { get; set; }
        public string ClassificationofLand { get; set; }
        public string Department { get; set; }
        public string Zone { get; set; }
        public string Division { get; set; }
        public string Locality { get; set; }
        public string KhasraNo { get; set; }
        public string AddressWithLandmark { get; set; }
        public string PrimaryListNo { get; set; }
        public string AreaInSqm { get; set; }
        public string RestoreReason { get; set; }
        public string RestoreDate { get; set; }
    }
}
