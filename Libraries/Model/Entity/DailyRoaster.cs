using Libraries.Model.Common;
using System;

namespace Libraries.Model.Entity
{
    public class DailyRoaster : AuditableEntity<int>
    {
        public int MonthlyRoasterId { get; set; }
        public string Day { get; set; }
        public DateTime Date { get; set; }
    }
}
