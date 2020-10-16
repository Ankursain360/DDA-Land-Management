using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
     public class Serialnumber : AuditableEntity<int>

     {
        public Serialnumber()
        {
            Ldoland = new HashSet<Ldoland>();
            Morland = new HashSet<Morland>();
        }
        [Required]
        public int? SerialNo { get; set; }
        [Required]
        public byte? IsActive { get; set; }
        public virtual ICollection<Morland> Morland { get; set; }
        public ICollection<Ldoland> Ldoland { get; set; }
        
    }
}
