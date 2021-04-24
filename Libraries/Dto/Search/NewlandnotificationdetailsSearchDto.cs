


using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class NewlandnotificationdetailsSearchDto : BaseSearchDto
    {
        public string type { get; set; }
        public string notification { get; set; }
        public string locality { get; set; }
        public string khasra { get; set; }
    }

}
