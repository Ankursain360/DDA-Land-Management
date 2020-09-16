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
        public string Name { get; set; }
        public string Code { get; set; }
        public byte IsActive { get; set; }
        public int ZoneId { get; set; }
        public int DepartmentId { get; set; }

        public virtual Zone Zone { get; set; }
        public virtual Department Department { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
      

    }
}
