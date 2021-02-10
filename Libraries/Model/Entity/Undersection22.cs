using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Undersection22 : AuditableEntity<int>
    {
        public Undersection22()
        {
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
        }
        [Required]
        public string NotificationNo { get; set; }
        [Required]
        public DateTime? NotificationDate { get; set; }
        [Required]
        public byte? IsActive { get; set; }
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }
    }
}
