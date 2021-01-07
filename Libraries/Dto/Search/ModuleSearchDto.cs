using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    
    public class ModuleSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string colname { get; set; }
        public int orderby { get; set; }
    }
}
