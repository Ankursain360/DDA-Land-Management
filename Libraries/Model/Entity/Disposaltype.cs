using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Disposaltype : AuditableEntity<int>
    {
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public ICollection<Propertyregistration> Propertyregistration { get; set; }

    }
}
