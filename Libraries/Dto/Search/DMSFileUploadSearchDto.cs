using Dto.Common;

namespace Dto.Search
{
    public class DMSFileUploadSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int departmentId { get; set; }
        public int localityId { get; set; }
        public int KhasraId { get; set; }
        public int CategoryId { get; set; } 
    }
}
