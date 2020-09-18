using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Disposalland : AuditableEntity<int>
  
    {
        [Required]
        public int? VillageId { get; set; }
        [Required]
        public int? KhasraId { get; set; }
        [Required]
        public string TransferToWhichDept { get; set; }
        [Required]
        public decimal AreaDisposed { get; set; }
        [Required]
        public DateTime? DateOfDisposed { get; set; }
        [Required]
        public string TransferTo { get; set; }
        [Required]
        public string TransferBy { get; set; }
        [Required]
        public int? UtilizationtypeId { get; set; }
        [Required]
        public string FileNoRefNo { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Village> VillageList { get; set; }
        public virtual Village Village { get; set; }

        [NotMapped]
        public List<Utilizationtype> UtilizationtypeList { get; set; }
        public virtual Utilizationtype Utilizationtype { get; set; }


        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }

    }
}
