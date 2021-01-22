using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Landuse : AuditableEntity<int>
    {
        public Landuse()
        {
            Propertyregistration = new HashSet<Propertyregistration>();
        }
        [Required(ErrorMessage = " Land Use name is mandatory")]
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public ICollection<Propertyregistration> Propertyregistration { get; set; }

    }
}
