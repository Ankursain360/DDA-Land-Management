using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Designation : AuditableEntity<int>
    {
       [Required]
        public string Name { get; set; }
        public byte IsActive { get; set; }

    }
}
