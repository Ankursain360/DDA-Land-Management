using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public  class Premiumrate : AuditableEntity<int>
    {
       
        public int PropertyTypeId { get; set; }
        public decimal PremiumRate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public byte? IsActive { get; set; }
       

        public PropertyType PropertyType { get; set; }
    }
}
