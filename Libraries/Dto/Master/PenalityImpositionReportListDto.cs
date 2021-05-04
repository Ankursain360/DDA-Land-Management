using Dto.Common;

namespace Dto.Master
{
   public class PenalityImpositionReportListDto
    {
        public int Id { get; set; }
        public string Locality { get; set; }
        public string FileNo { get; set; }
        public string PropertyNumber { get; set; }
        public string PayeeName { get; set; }
        public string DemandNo { get; set; }
      
        public string PenaltyInterestCharges { get; set; }
    }
}
