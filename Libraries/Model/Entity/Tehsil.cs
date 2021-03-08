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
        [Required(ErrorMessage = "Tehsil Name is mandatory ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Status is mandatory ")]
        public byte? IsActive { get; set; }
        public virtual ICollection<Acquiredlandvillage> Acquiredlandvillage { get; set; }
        public ICollection<Newlandvillage> Newlandvillage { get; set; }

    }
}
