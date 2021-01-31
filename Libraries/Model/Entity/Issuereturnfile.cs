using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Issuereturnfile : AuditableEntity<int>
    {
        public int DataStorageDetailsId { get; set; }
        [Required(ErrorMessage = "Employee name is mandatory")]
        public string IssuedToEmployee { get; set; }
        [Required(ErrorMessage = "Issue date is mandatory")]
        public DateTime? IssuedDate { get; set; }
        //[Required(ErrorMessage = "Return date is mandatory")]
        public DateTime? ReturnedDate { get; set; }
        public string FileCategoryId { get; set; }
        public string LocalityHeader { get; set; }
        public int? SequenceNo { get; set; }
        public int? Year { get; set; }
        public string SchemeId { get; set; }
        public string LocalityId { get; set; }
        public int? IssuedBy { get; set; }
        public int? ReturnedBy { get; set; }
        [Required(ErrorMessage = "Department name is mandatory")]
        public int? DepartmentId { get; set; }
        [Required(ErrorMessage = "Branch name is mandatory")]
        public int? BranchId { get; set; }
        [Required(ErrorMessage = "Designation name is mandatory")]
        public int? DesignationId { get; set; }
       

        public Branch Branch { get; set; }
        public Datastoragedetails DataStorageDetails { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }

        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Branch> BranchList { get; set; }
        [NotMapped]
        public List<Designation> DesignationList { get; set; }

    }
}
