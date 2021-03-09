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
            Damagecalculation = new HashSet<Damagecalculation>();
            Goldrate = new HashSet<Goldrate>();
        }
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public ICollection<Interest> Interest { get; set; }
        public ICollection<Rate> Rate { get; set; }
        public ICollection<Damagecalculation> Damagecalculation { get; set; }
        public ICollection<Premiumrate> Premiumrate { get; set; }
        public ICollection<Goldrate> Goldrate { get; set; }
    }
}
