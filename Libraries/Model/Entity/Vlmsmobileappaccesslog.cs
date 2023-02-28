using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Vlmsmobileappaccesslog : AuditableEntity<int>
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public string Brand { get; set; }
        public string OSVersion { get; set; }
        public string LoginStatus { get; set; }
        public string ModelNumber { get; set; } 
        public byte IsActive { get; set; }
        public ApplicationUser  user { get; set; }

    }
}
