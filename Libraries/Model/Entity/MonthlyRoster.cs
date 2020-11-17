using Libraries.Model.Common;

namespace Libraries.Model
{
    public class MonthlyRoster : AuditableEntity<int>
    {
        public string Day { get; set; }
        public string Date { get; set; }
    }
}
