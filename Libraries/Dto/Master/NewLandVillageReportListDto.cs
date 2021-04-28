using Dto.Common;

namespace Dto.Master
{
   public class NewLandVillageReportListDto
    {
        public int Id { get; set; }
        public string Village { get; set; }
        public string YearOfConsolidation { get; set; }
        public string TotalSheet { get; set; }
        public string Circle { get; set; }


        public string Acquired { get; set; }
    }
}
