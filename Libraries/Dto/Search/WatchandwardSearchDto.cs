using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{

    public class WatchandwardSearchDto : BaseSearchDto
    {
        public string date { get; set; }
        public string locality { get; set; }
        public string khasrano { get; set; }
        public string primarylistno { get; set; }
    }
}
