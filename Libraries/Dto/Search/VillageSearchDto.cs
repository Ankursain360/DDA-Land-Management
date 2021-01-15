using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class VillageSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string zone { get; set; }
    }
}
