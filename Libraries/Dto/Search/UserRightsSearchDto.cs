using Dto.Common;

namespace Dto.Search
{
   public class UserRightsSearchDto : BaseSearchDto
    {
        public int department { get; set; }
        public int userid { get; set; }
    }
}
