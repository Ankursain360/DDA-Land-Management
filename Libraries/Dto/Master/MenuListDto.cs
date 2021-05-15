using Dto.Common;

namespace Dto.Master
{
     public  class MenuListDto
     {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string MenuName { get; set; }
        public string SortBy { get; set; }
        public string ParentMenu { get; set; }
        public string Status { get; set; }
    }
}
