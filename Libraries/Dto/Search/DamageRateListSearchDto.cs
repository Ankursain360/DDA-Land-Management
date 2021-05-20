using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class DamageRateListSearchDto : BaseSearchDto
    {
        public int propertyid { get; set; }
        public int daterangeid { get; set; }
        public int localityid { get; set; }
    }
}
