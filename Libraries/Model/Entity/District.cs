using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Model.Entity;

namespace Libraries.Model.Entity
{
   public partial class District : AuditableEntity<int>
    {
        public District()
        {
            Userprofile = new HashSet<Userprofile>();
        }
        [Required(ErrorMessage = "Department Name is Mandatory Field")]

        public string Name { get; set; }
       
        public string Code { get; set; }
        [Required]
        public byte IsActive { get; set; }

        public virtual ICollection<Userprofile> Userprofile { get; set; }
        public virtual ICollection<Acquiredlandvillage> Acquiredlandvillage { get; set; }

    }
}
