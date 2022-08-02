using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class DamagePayeeDashboardList : AuditableDto<int>
    {
        public string VillageName { get; set; }
        public int TotalapplicationReceived { get; set; }
    }
}
