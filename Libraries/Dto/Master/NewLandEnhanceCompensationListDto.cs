using Dto.Common;

namespace Dto.Master
{
   public class NewLandEnhanceCompensationListDto
    {
        public int Id { get; set; }
        public string DemandListNo { get; set; }
        public string VillageName { get; set; }
        public string KhasraName { get; set; }
        public string Area { get; set; }
        public string PayableAppealable { get; set; }
     

        public string CaseInvolesWhichCourt { get; set; }
        public string IsActive { get; set; }
    }
}
