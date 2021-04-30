
using Dto.Common;

namespace Dto.Master
{
    public class MutationListDto
    {
        
        public int Id { get; set; }
        public string Village { get; set; }
        public string KhasraNo { get; set; }

        public string MutationOwnerLessee { get; set; }
        public string MutationNo { get; set; }
        public string MutationFees { get; set; }
      
        public string Status { get; set; }
    }
}
