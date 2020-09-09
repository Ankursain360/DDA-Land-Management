using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    
    public class Zone : AuditableEntity<int>
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [Remote(action: "Exist", controller: "Zone", AdditionalFields = "Id")]
        public string Name { get; set; }

        [Required]
        [Remote(action: "IsCodeExist", controller: "Zone", AdditionalFields = "Id")]
        public string Code { get; set; }
        public byte IsActive { get; set; }

        [NotMapped]
        public string DepartmentName { get; set; }

        public virtual Department  Department{ get; set; }
        public virtual ICollection<Village> Village { get; set; }
        public virtual ICollection<Role> Role { get; set; }

        [NotMapped]
        public List<Department> DepartmentList { get; set; }
    }
}