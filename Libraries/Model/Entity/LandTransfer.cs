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
            Propertyregistrationhistory = new HashSet<PropertyRegistrationHistory>();
        }
        [Required(ErrorMessage = "The Handed Over Department is required.")]
        public int? HandedOverDepartmentId { get; set; }
        [Required(ErrorMessage = "The Handed Over By Name Desingnation is required.")]
        public string HandedOverByNameDesingnation { get; set; }
        [Required(ErrorMessage = "The Handed Over By Date is required.")]
        public DateTime? HandedOverDate { get; set; }
        [Required(ErrorMessage = "The Order No is required.")]
        public string OrderNo { get; set; }
        public string CopyofOrderDocPath { get; set; }
        public string HandedOverFile { get; set; }
        [Required(ErrorMessage = "The Transfer order Issue Authority is required.")]
        public string TransferorderIssueAuthority { get; set; }
        [Required(ErrorMessage = "The Taken Over Department is required.")]
        public int? TakenOverDepartmentId { get; set; }
        [Required(ErrorMessage = "The Taken Over By Name Desingnation is required.")]
        public string TakenOverByNameDesingnation { get; set; }
        [Required(ErrorMessage = "The Taken Over is required.")]
        public DateTime? DateofTakenOver { get; set; }
        public string Remarks { get; set; }
        public byte? IsValidate { get; set; }
        public byte? IsActive { get; set; }
        [Required]
        public int PropertyRegistrationId { get; set; }
        [Required(ErrorMessage = "The Zone field is required.")]
        public int HandedOverZoneId { get; set; }
        [Required(ErrorMessage = "The Division field is required.")]
        public int HandedOverDivisionId { get; set; }
        public string HandedOverEmailId { get; set; }
        public decimal? HandedOverMobileNo { get; set; }
        public decimal? HandedOverLandLineNo { get; set; }
        public string HandedOverCommments { get; set; }
        [Required(ErrorMessage = "The Zone field is required.")]
        public int TakenOverZoneId { get; set; }
        [Required(ErrorMessage = "The Division field is required.")]
        public int TakenOverDivisionId { get; set; }
        public string TakenOverEmailId { get; set; }
        public decimal? TakenOverMobileNo { get; set; }
        public decimal? TakenOverLandLineNo { get; set; }
        public string TakenOverCommments { get; set; }
        public string TakenOverDocument { get; set; }
        public byte? Encroachment { get; set; }
        public int? EncroachmentStatus { get; set; }
        public decimal? EncroachementArea { get; set; }
        public decimal? BuildUpInEncroachementArea { get; set; }
        public string ActionOnEncroachment { get; set; }
        public string ActionTakenReportPath { get; set; }
        public string EncroachmentDetails { get; set; }
        public virtual Department HandedOverDepartment { get; set; }
        public virtual Division HandedOverDivision { get; set; }
        public virtual Zone HandedOverZone { get; set; }
        public virtual Propertyregistration PropertyRegistration { get; set; }
        public virtual Department TakenOverDepartment { get; set; }
        public virtual Division TakenOverDivision { get; set; }
        public virtual Zone TakenOverZone { get; set; }
        [NotMapped]
        public bool IsValidateData { get; set; }
        [NotMapped]
        public IFormFile CopyofOrder { get; set; }
        [NotMapped]
        public IFormFile HandedOverFiles { get; set; }
        [NotMapped]
        public IFormFile TakenOverFile { get; set; }
        [NotMapped]
        public IFormFile ActionTakenReport { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Propertyregistration> PropertyRegistrationList { get; set; }
        [NotMapped]
        public List<Landtransfer> LandTransferList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Zone> HandedOverZoneList { get; set; }
        [NotMapped]
        public List<Zone> TakenOverZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
         [NotMapped]
        public List<Division> TakenOverDivisionList { get; set; }
         [NotMapped]
        public List<Division> HandedOverDivisionList { get; set; }
        [NotMapped]
        public List<Landtransfer> handeoverdepartmentlist { get; set; }
        [NotMapped]
        public int ReportType { get; set; }
        public virtual ICollection<PropertyRegistrationHistory> Propertyregistrationhistory { get; set; }
        public ICollection<Currentstatusoflandhistory> Currentstatusoflandhistory { get; set; }
        [NotMapped]
        public Propertyregistration Propertyregistration { get; set; }
    }
}