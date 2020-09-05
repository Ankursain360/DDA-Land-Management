using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public class Module : AuditableEntity<int>
    {
     
        public string Name { get; set; }
        public byte? IsActive { get; set; }

    }
}
