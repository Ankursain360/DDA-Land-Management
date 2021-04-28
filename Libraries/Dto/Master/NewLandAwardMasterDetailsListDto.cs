using Dto.Common;

namespace Dto.Master
{
    public class NewLandAwardMasterDetailsListDto
    {
        public int Id { get; set; }
        public string AwardNo { get; set; }
        public string AwardDate { get; set; }
        public string VillageName { get; set; }
        public string ProposalName { get; set; }

        public string IsActive { get; set; }
    }
}
