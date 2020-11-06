using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Planning :AuditableEntity<int>
    {
        public Planning()
        {
            PlanningProperties = new HashSet<PlanningProperties>();
        }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
        public virtual ICollection<PlanningProperties> PlanningProperties { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public int DepartmentId { get; set; }
        [NotMapped]
        public int DivisionId { get; set; }
        [NotMapped]
        public int ZoneId{ get; set; }
    }
}
