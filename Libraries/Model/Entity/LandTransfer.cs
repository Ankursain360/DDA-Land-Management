using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Landtransfer : AuditableEntity<int>
    {

        public Landtransfer()
        {
            Currentstatusoflandhistory = new HashSet<Currentstatusoflandhistory>();
        }
        [Required(ErrorMessage = "The Department field is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "The Zone field is required.")]
        public int ZoneId { get; set; }
        [Required(ErrorMessage = "The Division field is required.")]
        public int DivisionId { get; set; }
        [Required(ErrorMessage = "The Locality field is required.")]
        public int LocalityId { get; set; }
        [Required(ErrorMessage = "The Khasra Number is required.")]
        public string KhasraNo { get; set; }
        [Required(ErrorMessage = "The Buildup Area is required.")]
        public decimal? BuildupArea { get; set; }
        [Required(ErrorMessage = "The Vacant Area is required.")]
        public decimal? VacantArea { get; set; }
        [Required(ErrorMessage = "The Total Area is required.")]
        public decimal? TotalArea { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "The Handed Over Department is required.")]
        public int? HandedOverDepartmentId { get; set; }
        [Required(ErrorMessage = "The Handed Over By Name Desingnation is required.")]
        public string HandedOverByNameDesingnation { get; set; }
        [Required(ErrorMessage = "The Handed Over By Date is required.")]
        public DateTime? HandedOverDate { get; set; }
        [Required(ErrorMessage = "The Order No is required.")]
        public string OrderNo { get; set; }
        public string CopyofOrderDocPath { get; set; }
        [Required(ErrorMessage = "The Transfer order Issue Authority is required.")]
        public string TransferorderIssueAuthority { get; set; }
        [Required(ErrorMessage = "The Taken Over Department is required.")]
        public int? TakenOverDepartmentId { get; set; }
        [Required(ErrorMessage = "The Taken Over By Name Desingnation is required.")]
        public string TakenOverByNameDesingnation { get; set; }
        [Required(ErrorMessage = "The Taken Over is required.")]
        public DateTime? DateofTakenOver { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual Department Department { get; set; }
        public virtual Department HandedOverDepartment { get; set; }
        public virtual Department TakenOverDepartment { get; set; }
        public virtual Division Division { get; set; }
        public virtual Locality Locality { get; set; }
        [NotMapped]
        public IFormFile CopyofOrder { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Landtransfer> LandTransferList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }

        [NotMapped]
        public List<Landtransfer> handeoverdepartmentlist { get; set; }
        [NotMapped]
        public int ReportType { get; set; }

        public ICollection<Currentstatusoflandhistory> Currentstatusoflandhistory { get; set; }

       
        
    }
}