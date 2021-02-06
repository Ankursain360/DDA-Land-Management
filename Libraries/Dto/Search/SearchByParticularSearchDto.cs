using Dto.Common;

namespace Dto.Search
{
    public class SearchByParticularSearchDto : BaseSearchDto
    {
        public int name { get; set; }
        public int DeptId { get; set; }
        public int LocalityId { get; set; }
        public int AlmirahId { get; set; }
        public int RowId { get; set; }
        public int BundleId { get; set; }
        public int ColId { get; set; }
        public int RRNo { get; set; }
        public string FileNo { get; set; }
        public string FileName { get; set; }
    }
}
