using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class MonthlyRoaster : AuditableEntity<int>
    {
        public int DepartmentId { get; set; }
        public int ZoneId { get; set; }
        public int DivisionId { get; set; }
        public int? Locality { get; set; }
        public int SecurityGuard { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
