using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public class DailyRoaster : AuditableEntity<int>
    {
        public DailyRoaster()
        {
            AssignedPropertyDailyRoaster = new HashSet<AssignedPropertyDailyRoaster>();
        }
        public int MonthlyRoasterId { get; set; }
        public string Day { get; set; }
        public DateTime Date { get; set; }
        public MonthlyRoaster MonthlyRoaster { get; set; }
        public ICollection<AssignedPropertyDailyRoaster> AssignedPropertyDailyRoaster { get; set; }
    }
}
