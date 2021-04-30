

using Dto.Common;

namespace Dto.Master
{
    public class AwardmasterdetailListDto
    {
         
        public int Id { get; set; }
        public string AwardNumber { get; set; }
        public string Awarddate { get; set; }

        public string VillageName { get; set; }
        public string ProposalName { get; set; }

       
        public string Status { get; set; }
    }
}
