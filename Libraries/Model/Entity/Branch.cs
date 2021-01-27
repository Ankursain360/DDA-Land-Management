using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public partial class Branch : AuditableEntity<int>
    {
        public Branch()
        {
            Issuereturnfile = new HashSet<Issuereturnfile>();
        }

       
        public string Name { get; set; }
        public string Code { get; set; }
        public int? DepartmentId { get; set; }
        public byte? IsActive { get; set; }
        public Department Department { get; set; }
        public ICollection<Issuereturnfile> Issuereturnfile { get; set; }
    }
}
