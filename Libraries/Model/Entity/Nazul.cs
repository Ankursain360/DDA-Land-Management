using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Nazul : AuditableEntity<int>
    {

        public int VillageId { get; set; }
        [Required]
        public int JaraiSakni { get; set; }
        [Required]
        public int Language { get; set; }

        [Required]
        public DateTime YearOfConsolidation { get; set; }

        [Required]
        public DateTime YearOfJamabandi { get; set; }

        [Required]
        public string LastMutationNo { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Village> VillageList { get; set; }
        public virtual Village Village { get; set; }
    }
}


