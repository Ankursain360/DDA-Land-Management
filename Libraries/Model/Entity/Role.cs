using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Role : AuditableEntity<int>
    {
        [Required]
        public int ZoneId { get; set; }
        [Required]
        public string Name { get; set; }
        public byte IsActive { get; set; }
        [NotMapped]
        public string ZoneName { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        public virtual Zone Zone { get; set; }
    }
}
