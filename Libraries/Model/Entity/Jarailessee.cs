using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public class Jarailessee : AuditableEntity<int>
    {
       
        public int JaraiDetailId { get; set; }
        public string LesseeName { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string MortgageDetails { get; set; }
        public byte? IsActive { get; set; }
       

        public Jaraidetails JaraiDetail { get; set; }
    }
}
