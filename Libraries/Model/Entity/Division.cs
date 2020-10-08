using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Division : AuditableEntity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public byte IsActive { get; set; }
        [Required]
        public int ZoneId { get; set; }
        [Required]
        public int DepartmentId { get; set; }

        public virtual Zone Zone { get; set; }
        public virtual Department Department { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        public ICollection<Propertyregistration> Propertyregistration { get; set; }
        public ICollection<Landtransfer> Landtransfer { get; set; }

    }
}
