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
        [Required]
        public decimal? BuildupArea { get; set; }
        [Required]
        public decimal? VacantArea { get; set; }
        [Required]
        public decimal? TotalArea { get; set; }
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

        //Current status of land history table entry feilds:

        //[NotMapped]
        //public string TSSSurvey { get; set; }

        //[NotMapped]
        //public IFormFile  SurveyReportFile { get; set; }
        
      
        //[NotMapped]
        //public string SurveyReportFilePath { get; set; }
        //[NotMapped]
        //public string Encroachment { get; set; }
        //[NotMapped]
        //public decimal? EncroachedArea { get; set; }
        //[NotMapped]
        //public string ActionOnEncroachment { get; set; }
        //[NotMapped]
        //public IFormFile ActionReportFile{ get; set; }

        //[NotMapped]
        //public string ActionReportFilePath { get; set; }

        //[NotMapped]
        //public string FencingBoundaryWall { get; set; }

        //[NotMapped]
        //public decimal? AreaCovered { get; set; }
        //[NotMapped]
        //public string Dimension { get; set; }
        //[NotMapped]
        //public string PlotUtilization { get; set; }
        //[NotMapped]
        //public decimal? AreaUtilised { get; set; }
        //[NotMapped]
        //public decimal? BalanceArea { get; set; }
        //[NotMapped]
        //public string Status { get; set; }
        //[NotMapped]
        //public string PlannedUnplannedLand { get; set; }
        //[NotMapped]
        //public string MainLandUse { get; set; }
        //[NotMapped]
        //public string SubUse { get; set; }
        //[NotMapped]
        //public string currentLandRemarks { get; set; }

        
    }
}