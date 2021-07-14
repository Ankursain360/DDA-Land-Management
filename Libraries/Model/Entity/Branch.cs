using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Branch : AuditableEntity<int>
    {
        public Branch()
        {
            Datastoragedetails = new HashSet<Datastoragedetails>();
            Issuereturnfile = new HashSet<Issuereturnfile>();
            Userprofile = new HashSet<Userprofile>();
            Kycform = new HashSet<Kycform>();
        }

        [Required(ErrorMessage = "Branch Name is mandatory")]
        [Remote(action: "Exist", controller: "Branch", AdditionalFields = "Id")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = " Branch Code is mandatory")]
        [Remote(action: "IsCodeExist", controller: "Branch", AdditionalFields = "Id")]
        [StringLength(100)]
        public string Code { get; set; }
        public int? DepartmentId { get; set; }
        public byte? IsActive { get; set; }
        public Department Department { get; set; }
        [NotMapped]
        public string DepartmentName { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        public ICollection<Issuereturnfile> Issuereturnfile { get; set; }
        public ICollection<Datastoragedetails> Datastoragedetails { get; set; }
        public virtual ICollection<Userprofile> Userprofile { get; set; }
        public ICollection<Kycform> Kycform { get; set; }
    }
}
