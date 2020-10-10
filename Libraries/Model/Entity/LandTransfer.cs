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
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int ZoneId { get; set; }
        [Required]
        public int DivisionId { get; set; }
        [Required]
        public int LocalityId { get; set; }
        [Required]
        public string KhasraNo { get; set; }
        public string Address { get; set; }
        [Required]
        public int? HandedOverDepartmentId { get; set; }
        [Required]
        public string HandedOverByNameDesingnation { get; set; }
        [Required]
        public DateTime? HandedOverDate { get; set; }
        [Required]
        public string OrderNo { get; set; }
        public string CopyofOrderDocPath { get; set; }
        [Required]
        public string TransferorderIssueAuthority { get; set; }
        [Required]
        public int? TakenOverDepartmentId { get; set; }
        [Required]
        public string TakenOverByNameDesingnation { get; set; }
        [Required]
        public DateTime? DateofTakenOver { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual Department Department { get; set; }
        public virtual Division Division { get; set; }
        public virtual Locality Locality { get; set; }
        [NotMapped]
        public IFormFile CopyofOrder { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }

        [NotMapped]
        public List<Landtransfer> handeoverdepartmentlist { get; set; }
    }
}