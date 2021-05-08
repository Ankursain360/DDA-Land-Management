
using Dto.Common;

namespace Dto.Master
{
    public class OnlineComplaintListDto
    {  
        public int Id { get; set; }
        public string ReferenceNo { get; set; }
        public string ComplaintName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string ApprovalStatus { get; set; }
        
    }
}
