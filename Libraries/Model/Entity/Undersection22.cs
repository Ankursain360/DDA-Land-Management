using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Undersection22 : AuditableEntity<int>
    {
        public Undersection22()
        {
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
        }
        [Required(ErrorMessage = "Notification  is mandatory")]
        public string NotificationNo { get; set; }
        [Required]
        public DateTime? NotificationDate { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte? IsActive { get; set; }
        public string DocumentName { get; set; }
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }

        [NotMapped]
        public IFormFile DocumentIFormFile { get; set; }
    }
}
