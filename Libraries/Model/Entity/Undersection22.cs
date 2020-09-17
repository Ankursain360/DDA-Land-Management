using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public class Undersection22 : AuditableEntity<int>
    {
        
        public string NotificationNo { get; set; }
        public DateTime? NotificationDate { get; set; }
        public byte? IsActive { get; set; }
       
    }
}
