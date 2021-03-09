using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Goldrate : AuditableEntity<int>
    {
        public int PropertyTypeId { get; set; }
        public decimal GoldRate1 { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public byte? IsActive { get; set; }
       

        public PropertyType PropertyType { get; set; }
    }
}
