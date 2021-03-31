using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class CancellationEntrySearchDto : BaseSearchDto
    {
        public string refno { get; set; }
        public string name { get; set; }
    }
}
