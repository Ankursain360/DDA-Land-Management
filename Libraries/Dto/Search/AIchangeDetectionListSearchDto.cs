using Dto.Common;
using System.Collections.Generic;

namespace Dto.Search
{
    public class AIchangeDetectionListSearchDto : BaseSearchDto
    {
        public int id { get; set; } 
        public string Village { get; set; }
        public string FirstPhoto { get; set; }
        public string SecondPhoto { get; set; }

    }
}
