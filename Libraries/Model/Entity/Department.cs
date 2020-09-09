using System;
using System.Collections.Generic;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Department : AuditableEntity<int>
    {
        public string Name { get; set; }
        public byte? IsActive { get; set; }
        public virtual ICollection<Village> Village { get; set; }
    }
}
