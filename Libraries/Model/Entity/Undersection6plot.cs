using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Undersection6plot : AuditableEntity<int>
    {

        [Required(ErrorMessage = "The Notification6 field is required")]
        public int? UnderSection6Id { get; set; }
        [Required(ErrorMessage = "The Village field is required")]
        public int? VillageId { get; set; }
        [Required(ErrorMessage = "The Khasra No field is required")]
        public int? KhasraId { get; set; }
        [Required]
        public decimal Bigha { get; set; }
        [Required]
        public decimal Biswa { get; set; }
        [Required]
        public decimal Biswanshi { get; set; }
      
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public Khasra Khasra { get; set; }

        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public Acquiredlandvillage Village { get; set; }

        [NotMapped]
        public List<Undersection6> NotificationList { get; set; }
        public Undersection6 Undersection6 { get; set; }
    }
}
