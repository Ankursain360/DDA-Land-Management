using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public  class Jaraifarmer : AuditableEntity<int>
    {
        
        public int JaraiDetailId { get; set; }
        public string FarmerName { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public byte? IsActive { get; set; }
       

        public Jaraidetails JaraiDetail { get; set; }
    }
}
