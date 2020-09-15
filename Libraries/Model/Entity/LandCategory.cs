using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class LandCategory : AuditableEntity<int>
    {

        public string Name { get; set; }
        public byte? IsActive { get; set; }

    }
}
