using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class WorkflowTemplateSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string version { get; set; }
        public int moduleid { get; set; }
        public string colname { get; set; }
        public int orderby { get; set; }
    }
}
