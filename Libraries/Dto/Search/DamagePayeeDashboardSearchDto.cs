using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class DamagePayeeDashboardSearchDto : AuditableDto<int>
    {
        public DateTime toDate { get; set; }
        public DateTime fromDate { get; set; }
        public string Villagetype { get; set; }
    }
}
