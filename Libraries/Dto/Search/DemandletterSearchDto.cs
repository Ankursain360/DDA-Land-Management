using Dto.Common;

namespace Dto.Search
{
   public class DemandletterSearchDto : BaseSearchDto
    {
        public string locality { get; set; }
        public string demandno { get; set; }
        public string fileno { get; set; }
    }
}
