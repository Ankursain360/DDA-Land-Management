using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Purpose : AuditableEntity<int>
    {

        public string Name { get; set; }
        public byte? IsActive { get; set; }
        //public virtual ICollection<Undersection4> Undersection4 { get; set; }


    }
}
