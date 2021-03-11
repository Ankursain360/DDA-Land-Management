using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class PropertyType : AuditableEntity<int>
    {
        public PropertyType()
        {
            Interest = new HashSet<Interest>();
            Rate = new HashSet<Rate>();
            Premiumrate = new HashSet<Premiumrate>();
            Documentcharges = new HashSet<Documentcharges>();
            Damagecalculation = new HashSet<Damagecalculation>();
            Groundrate = new HashSet<Groundrent>();
            Intersetrate = new HashSet<Interestrate>();
        }
        [Required(ErrorMessage = "Name is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
        public ICollection<Interest> Interest { get; set; }
        public ICollection<Rate> Rate { get; set; }
        public ICollection<Damagecalculation> Damagecalculation { get; set; }
        public ICollection<Premiumrate> Premiumrate { get; set; }
        public ICollection<Groundrent> Groundrate { get; set; }
        public ICollection<Documentcharges> Documentcharges { get; set; }
        public ICollection<Interestrate> Intersetrate { get; set; }
    }
}
