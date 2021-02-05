
using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class SchemeFileLoadingSearchDto : BaseSearchDto
    {
        public string schemename { get; set; }
        public string schemecode { get; set; }
    }
}

