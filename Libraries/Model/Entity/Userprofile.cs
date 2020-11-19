using Libraries.Model.Common;
using Libraries.Model.Entity;
using System.Collections.Generic;

namespace Model.Entity
{
    public partial class Userprofile : AuditableEntity<int>
    {
        public Userprofile()
        {
            MonthlyRoaster = new HashSet<MonthlyRoaster>();
        }
        public int? RoleId { get; set; }
        public int? ZoneId { get; set; }
        public int? DistrictId { get; set; }
        public short? IsActive { get; set; }
        public int UserId { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual District District { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
        public virtual Zone Zone { get; set; }
        public ICollection<MonthlyRoaster> MonthlyRoaster { get; set; }
    }
}
