
using Dto.Common;

namespace Dto.Master
{
    public class EnchroachmentListDto
    {  
        public int Id { get; set; }
        public string VillageName { get; set; }
        public string KhasraNo { get; set; }
        public string LandUse { get; set; }
        public string ReferenceNo { get; set; }
        public string ActionTaken { get; set; }
        
        public string Status { get; set; }
    }
}
