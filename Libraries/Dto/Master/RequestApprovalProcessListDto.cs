using Dto.Common;

namespace Dto.Master
{
   public class RequestApprovalProcessListDto
    {
        public int Id { get; set; }
        public string ProposalName { get; set; }
        public string Fileno { get; set; }
        public string RequiringBody { get; set; }
        public string Area { get; set; }
        public string ApprovedStatus { get; set; }


        public string IsActive { get; set; }
    }
}
