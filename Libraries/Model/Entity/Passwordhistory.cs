using Libraries.Model.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public  class Passwordhistory : AuditableEntity<int>
    {
       
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
        
    }
}
