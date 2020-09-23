using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public  class Restoreproperty : AuditableEntity<int>
    {
       
        public int PropertyRegistrationId { get; set; }
        public string RestoreReason { get; set; }
        public int RestoreBy { get; set; }
        public DateTime RestoreDate { get; set; }
      
    }
}
