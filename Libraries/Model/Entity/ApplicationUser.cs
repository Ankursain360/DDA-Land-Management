using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class ApplicationUser : IdentityUser<int> 
    {
        public int? DepartmentId { get; set; }
        public int? ZoneId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? PasswordSetDate { get; set; }
        public string IsDefaultPassword { get; set; }

        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        public virtual Department Department { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        public virtual Zone Zone { get; set; }
        [NotMapped]
        public List<Role> RoleList { get; set; }
        public virtual Role Role { get; set; }
    }
}
