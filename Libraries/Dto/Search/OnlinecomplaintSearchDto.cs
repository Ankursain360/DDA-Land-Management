using Dto.Common;
namespace Dto.Search
{
    public class OnlinecomplaintSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string colname { get; set; }
        public int orderby { get; set; }
    }
}
