using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public  class Damagepayeepersonelinfo : AuditableEntity<int>
    {

      
        public int DamagePayeeRegisterId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
       
        public byte? IsActive { get; set; }

        public Damagepayeeregister DamagePayeeRegister { get; set; }
    }
}
