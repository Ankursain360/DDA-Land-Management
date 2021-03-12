using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class LogSearchDto : BaseSearchDto
    {
        public string application { get; set; }
        public string logger { get; set; }
        
    }
}
