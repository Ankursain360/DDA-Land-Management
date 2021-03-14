using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Disposalland : AuditableEntity<int>
  
    {
       
        [Required(ErrorMessage = "Village is Mandatory", AllowEmptyStrings = false)]
        public int? VillageId { get; set; }
        [Required(ErrorMessage = "Khasra is Mandatory", AllowEmptyStrings = false)]
        public int? KhasraId { get; set; }
        [Required(ErrorMessage = "Transfer To Which Dept is Mandatory")]

        public string TransferToWhichDept { get; set; }
        [Required(ErrorMessage = "Area Disposed is Mandatory")]
        public decimal AreaDisposed { get; set; }
        [Required(ErrorMessage = "Date Of Disposed is Mandatory")]
        public DateTime? DateOfDisposed { get; set; }
        [Required(ErrorMessage = "Transfer To is Mandatory")]
        public string TransferTo { get; set; }
        [Required(ErrorMessage = "Transfer By is Mandatory")]
        public string TransferBy { get; set; }
        [Required(ErrorMessage = "Utilization Type is Mandatory", AllowEmptyStrings = false)]
        public int? UtilizationtypeId { get; set; }
        [Required(ErrorMessage = "FileNo/RefNo is Mandatory")]
        public string FileNoRefNo { get; set; }
        [Required(ErrorMessage = "Remarks is Mandatory")]
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public virtual Acquiredlandvillage Village { get; set; }

        [NotMapped]
        public List<Utilizationtype> UtilizationtypeList { get; set; }
        public virtual Utilizationtype Utilizationtype { get; set; }


        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }

    }
}
