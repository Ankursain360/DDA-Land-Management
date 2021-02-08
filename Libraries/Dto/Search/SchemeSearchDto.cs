using Dto.Common;

namespace Dto.Search
{
   public class SchemeSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string code { get; set; }
        public string fileno { get; set; }
    }
}
