using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
     public class Serialnumber : AuditableEntity<int>

     {
      
        public int? SerialNo { get; set; }
        public byte? IsActive { get; set; }
       
     }
}
