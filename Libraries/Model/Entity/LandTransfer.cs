using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Libraries.Model.Entity
{
    public class LandTransfer : AuditableEntity<int>
    {
        [Required]
        public int ZoneId { get; set; }
        [Required]
        public int DivisionId { get; set; }
        [Required]
        public int VillageId { get; set; }
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
        public string FileName { get; set; }
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
    }
}