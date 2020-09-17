using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Undersection22 : AuditableEntity<int>
    {
        [Required]
        public string NotificationNo { get; set; }
        [Required]
        public DateTime? NotificationDate { get; set; }
        [Required]
        public byte? IsActive { get; set; }
       
    }
}
