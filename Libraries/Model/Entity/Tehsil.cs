using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Tehsil: AuditableEntity<int>
    {
        public Tehsil()
        {
            Acquiredlandvillage = new HashSet<Acquiredlandvillage>();
        }
        [Required(ErrorMessage ="Please enter Tehsil Name")]
        public string Name { get; set; }
        public byte? IsActive { get; set; }
        public virtual ICollection<Acquiredlandvillage> Acquiredlandvillage { get; set; }


    }
}
