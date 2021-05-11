using Dto.Common;
namespace Dto.Master
{
    public class NewLandProposalListDto
    {
        public int Id { get; set; }
        public string SchemeName { get; set; }
        public string ProposalName { get; set; }
        public string RequiredAgency { get; set; }
        public string ProposalNo { get; set; }
        public string ProposalDate { get; set; }
        public string Area { get; set; }
        public string Status { get; set; }
    }
}
