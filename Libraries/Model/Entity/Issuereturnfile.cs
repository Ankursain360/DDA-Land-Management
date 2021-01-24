using Libraries.Model.Common;
using System;
using System.Collections.Generic;

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

        public Datastoragedetails DataStorageDetails { get; set; }

    }
}
