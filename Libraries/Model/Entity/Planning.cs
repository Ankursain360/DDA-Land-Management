using Libraries.Model.Common;
using System;
using System.Collections.Generic;
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
    }
}
