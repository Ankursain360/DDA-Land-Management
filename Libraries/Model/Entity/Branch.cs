using Libraries.Model.Common;
using Model.Entity;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public partial class Branch : AuditableEntity<int>
    {
        public Branch()
        {
            Datastoragedetails = new HashSet<Datastoragedetails>();
            Issuereturnfile = new HashSet<Issuereturnfile>();
            Userprofile = new HashSet<Userprofile>();
        }

       
        public string Name { get; set; }
        public string Code { get; set; }
        public int? DepartmentId { get; set; }
        public byte? IsActive { get; set; }
        public Department Department { get; set; }
        public ICollection<Issuereturnfile> Issuereturnfile { get; set; }
        public ICollection<Datastoragedetails> Datastoragedetails { get; set; }
        public virtual ICollection<Userprofile> Userprofile { get; set; }
    }
}
