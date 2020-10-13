using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Undersection4: AuditableEntity<int>
    {
      
        public int PurposeId { get; set; }
        public string Number { get; set; }
        public DateTime? Ndate { get; set; }
        public string Npurpose { get; set; }
        public string TypeDetails { get; set; }
        public string TypePurpose { get; set; }
        public byte IsActive { get; set; }

        [NotMapped]
        public List<Purpose> PurposeList { get; set; }
        public virtual Purpose Purpose { get; set; }

        public virtual ICollection<Undersection4plot> Undersection4plot { get; set; }

    }
}
