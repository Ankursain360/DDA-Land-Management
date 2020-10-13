using System.Collections.Generic;
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
        public string Name { get; set; }
        public string Code { get; set; }
        public byte IsActive { get; set; }

        public virtual ICollection<Userprofile> Userprofile { get; set; }
        public virtual ICollection<Acquiredlandvillage> Acquiredlandvillage { get; set; }

    }
}
