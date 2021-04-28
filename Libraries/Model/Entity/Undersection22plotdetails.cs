using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public  class Undersection22plotdetails : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Notification No  US 22 is mandatory")]
        public int? UnderSection22Id { get; set; }
       
        [Required(ErrorMessage = "Village is mandatory")]
        public int? AcquiredlandvillageId { get; set; }
        [Required(ErrorMessage = "Khasra No is mandatory")]
        public int? KhasraId { get; set; }
        [Required(ErrorMessage = "Bigha is mandatory")]
        public decimal Bigha { get; set; }
        [Required(ErrorMessage = "Biswa is mandatory")]
        public decimal Biswa { get; set; }
        [Required(ErrorMessage = "Biswanshi is mandatory")]
        public decimal Biswanshi { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> AcquiredlandvillageList { get; set; }
       
        [NotMapped]

        public List<Undersection22> Undersection22List { get; set; }
        public Khasra Khasra { get; set; }
        public Acquiredlandvillage Acquiredlandvillage { get; set; }
        
        public Undersection22 UnderSection22 { get; set; }


    }
}
