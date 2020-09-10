using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class PropertyType : AuditableEntity<int>
    {
        public string Name { get; set; }
        public byte IsActive { get; set; }

        public virtual ICollection<Interest> Interest { get; set; }

    }
}
