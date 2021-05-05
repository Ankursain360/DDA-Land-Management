using Dto.Common;

namespace Dto.Master
{
   public class LeaseSubpurposeListDto
    {
        public int Id { get; set; }
        public string PurposeUse { get; set; }
        public string SubpurposeUse { get; set; }
        public string Status { get; set; }
    }
}
