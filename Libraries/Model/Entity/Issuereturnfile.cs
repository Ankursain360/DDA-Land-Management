using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Issuereturnfile : AuditableEntity<int>
    {
        public int DataStorageDetailsId { get; set; }
        public string IssueToEmployee { get; set; }
        public int? DepartmentId { get; set; }
        public int? BranchId { get; set; }
        public int? DesignationId { get; set; }
        public DateTime? IssuingDate { get; set; }

        public Branch Branch { get; set; }
        public Datastoragedetails DataStorageDetails { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }

        //[NotMapped]
        //public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Branch> BranchList { get; set; }
        [NotMapped]
        public List<Designation> DesignationList { get; set; }

    }
}
