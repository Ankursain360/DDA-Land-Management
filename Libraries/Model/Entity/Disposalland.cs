using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Disposalland : AuditableEntity<int>
  
    {
        //public Disposalland()
        //{
        //}

        [Required(ErrorMessage = "Village is Mandatory Field", AllowEmptyStrings = false)]
        public int? VillageId { get; set; }
        [Required(ErrorMessage = "Khasra is Mandatory Field", AllowEmptyStrings = false)]
        public int? KhasraId { get; set; }
        [Required(ErrorMessage = "Transfer To Which Dept is Mandatory Field")]

        public string TransferToWhichDept { get; set; }
        [Required(ErrorMessage = "Area Disposed is Mandatory Field")]
        public decimal AreaDisposed { get; set; }
        [Required(ErrorMessage = "Date Of Disposed is Mandatory Field")]
        public DateTime? DateOfDisposed { get; set; }
        [Required(ErrorMessage = "Transfer To is Mandatory Field")]
        public string TransferTo { get; set; }
        [Required(ErrorMessage = "Transfer By is Mandatory Field")]
        public string TransferBy { get; set; }
        [Required(ErrorMessage = "Utilization Type is Mandatory Field", AllowEmptyStrings = false)]
        public int? UtilizationtypeId { get; set; }
        [Required(ErrorMessage = "FileNo/RefNo is Mandatory Field")]
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
