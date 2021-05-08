using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{    
    public class ModuleSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int modulecategoryId { get; set; }
        public string url { get; set; }
    }
}
