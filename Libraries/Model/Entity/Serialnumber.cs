using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
     public class Serialnumber : AuditableEntity<int>

     {
        [Required]
        public int? SerialNo { get; set; }
        [Required]
        public byte? IsActive { get; set; }
        public virtual ICollection<Morland> Morland { get; set; }

    }
}
