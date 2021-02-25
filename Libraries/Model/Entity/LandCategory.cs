using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class LandCategory : AuditableEntity<int>
    {
        public LandCategory()
        {
            Newlandkhasra = new HashSet<Newlandkhasra>();
        }

        public string Name { get; set; }
        public byte? IsActive { get; set; }
        public virtual ICollection<Newlandkhasra> Newlandkhasra { get; set; }
       
    }
}
