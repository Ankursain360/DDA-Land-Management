using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    
    public class Zone : AuditableEntity<int>
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public byte IsActive { get; set; }

        [NotMapped]
        public string DepartmentName { get; set; }
    }
}
