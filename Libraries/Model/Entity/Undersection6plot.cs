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

        [Required(ErrorMessage = "Notification6 is mandatory", AllowEmptyStrings = false)]
        public int? UnderSection6Id { get; set; }
        [Required(ErrorMessage = "Village is required mandatory", AllowEmptyStrings = false)]
        public int? VillageId { get; set; }
        [Required(ErrorMessage = "Khasra No field is mandatory", AllowEmptyStrings = false)]
        public int? KhasraId { get; set; }
        [Required(ErrorMessage = "Bigha field is mandatory")]
        public decimal Bigha { get; set; }
        [Required(ErrorMessage = "Biswa field is mandatory")]
        public decimal Biswa { get; set; }
        [Required(ErrorMessage = "Biswanshi field is mandatory")]
        public decimal Biswanshi { get; set; }
        [Required(ErrorMessage = "Status field is mandatory")]
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
