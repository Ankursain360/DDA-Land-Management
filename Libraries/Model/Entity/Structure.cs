using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Structure : AuditableEntity<int>
    {
        public Structure()
        {
            Demolitionstructure = new HashSet<Demolitionstructure>();
            Demolishedstructurerpt = new HashSet<Demolishedstructurerpt>();
        }

        [Required(ErrorMessage = " Structure name is mandatory")]
        public string Name { get; set; }
        
        public byte? IsActive { get; set; }

        public ICollection<Demolitionstructure> Demolitionstructure { get; set; }
        public ICollection<Demolishedstructurerpt> Demolishedstructurerpt { get; set; }
    }
}
