using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public  class Otherlandnotification : AuditableEntity<int>
    {
      
        public string LandType { get; set; }
        public string NotificationNumber { get; set; }
      
        public byte? IsActive { get; set; }
    }
}
