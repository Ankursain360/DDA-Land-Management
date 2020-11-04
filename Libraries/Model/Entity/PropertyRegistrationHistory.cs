using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class PropertyRegistrationHistory : AuditableEntity<int>
    {
        public int PropertyRegistrationId { get; set; }
        public int LandTransferId { get; set; }
        public int? HandedOverDepartmentId { get; set; }
        public int HandedOverZoneId { get; set; }
        public int HandedOverDivisionId { get; set; }
        public string HandedOverByNameDesingnation { get; set; }
        public DateTime? HandedOverDate { get; set; }
        public string HandedOverEmailId { get; set; }
        public decimal? HandedOverMobileNo { get; set; }
        public decimal? HandedOverLandLineNo { get; set; }
        public string HandedOverFile { get; set; }
        public string HandedOverCommments { get; set; }
        public string OrderNo { get; set; }
        public string CopyofOrderDocPath { get; set; }
        public string TransferorderIssueAuthority { get; set; }
        public int? TakenOverDepartmentId { get; set; }
        public int TakenOverZoneId { get; set; }
        public int TakenOverDivisionId { get; set; }
        public string TakenOverByNameDesingnation { get; set; }
        public string TakenOverEmailId { get; set; }
        public decimal? TakenOverMobileNo { get; set; }
        public decimal? TakenOverLandLineNo { get; set; }
        public string TakenOverCommments { get; set; }
        public string TakenOverDocument { get; set; }
        public DateTime? DateofTakenOver { get; set; }
        public byte? IsActive { get; set; }
    }
}
