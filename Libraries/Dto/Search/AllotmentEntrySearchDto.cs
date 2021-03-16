using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class AllotmentEntrySearchDto : BaseSearchDto
    {
        public string application { get; set; }
        public DateTime AllotmentDate { get; set; }
    }
}
