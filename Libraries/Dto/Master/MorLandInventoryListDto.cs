using Dto.Common;

namespace Dto.Master
{
   public class MorLandInventoryListDto
    {
        public int Id { get; set; }
        public string InventoriedIn { get; set; }
        public string PlannedUnplannedLand { get; set; }
        public string ClassificationofLand { get; set; }
        public string Department { get; set; }
        public string Zone { get; set; }
        public string Division { get; set; }
        public string PrimaryListNo { get; set; }
    }
}
