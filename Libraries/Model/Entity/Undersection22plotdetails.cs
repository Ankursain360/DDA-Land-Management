using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public  class Undersection22plotdetails : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Notification No  US 22 is mandatory field")]
        public int? UnderSection22Id { get; set; }
        public int? UnderSection4Id { get; set; }
        public int? UnderSection6Id { get; set; }
        public int? UnderSection17Id { get; set; }
        [Required(ErrorMessage = "Village is mandatory feild")]
        public int? AcquiredlandvillageId { get; set; }
        [Required(ErrorMessage = "Khasra No is mandatory feild")]
        public int? KhasraId { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string Remarks { get; set; }
       
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> AcquiredlandvillageList { get; set; }
        [NotMapped]
        public List<Undersection4> Undersection4List { get; set; }
        [NotMapped]
        public List<Undersection6> Undersection6List { get; set; }
        [NotMapped]
        public List<Undersection17> Undersection17List { get; set; }
        [NotMapped]

        public List<Undersection22> Undersection22List { get; set; }
        public Khasra Khasra { get; set; }
        public Acquiredlandvillage Acquiredlandvillage { get; set; }
        public Undersection17 UnderSection17 { get; set; }
        public Undersection22 UnderSection22 { get; set; }
        public Undersection4 UnderSection4 { get; set; }
        public Undersection6 UnderSection6 { get; set; }

    }
}
