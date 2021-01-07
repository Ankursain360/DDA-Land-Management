using Dto.Common;

namespace Dto.Search
{

    public class MenuSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string parentname { get; set; }
        public string modulename { get; set; } 
         
        
    }
}
