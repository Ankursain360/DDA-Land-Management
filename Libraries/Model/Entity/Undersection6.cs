using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Undersection6 : AuditableEntity<int>
    {
        public Undersection6()
        {
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
        }
        [Required]
        public string Number { get; set; }
        [Required]
        public DateTime? Ndate { get; set; }
        public byte IsActive { get; set; }

         [Required]
        public int? Undersection4Id { get; set; }
        [NotMapped]
        public List<Undersection4> NotificationList { get; set; }
        public Undersection4 Undersection4 { get; set; }
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }
        public ICollection<Undersection17> Undersection17 { get; set; }
        public ICollection<Undersection6plot> Undersection6plot { get; set; }
    }
}