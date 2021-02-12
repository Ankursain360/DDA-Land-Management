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

        [Required]
        public int? UnderSection6Id { get; set; }
        [Required]
        public int? VillageId { get; set; }
        [Required]
        public int? KhasraId { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
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
