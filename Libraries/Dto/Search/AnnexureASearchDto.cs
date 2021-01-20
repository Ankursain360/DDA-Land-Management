using Dto.Common;

namespace Dto.Search
{
    public class AnnexureASearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string locality { get; set; }
        public string khasra { get; set; }
        public string policestation { get; set; }
        public string colname { get; set; }
        public int orderby { get; set; }
    }
}
