using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Currentstatusoflandhistory : AuditableEntity<int>
    {
        [Required]
        public int? LandTransferId { get; set; }
        public string Tsssurvey { get; set; }
        public string SurveyReportFilePath { get; set; }
        public int Encroachment { get; set; }
        public decimal? VacantArea { get; set; }
        public int? EncroachmentStatus { get; set; }
        public DateTime VacationDate { get; set; }
        public int? NatureOfUtilization { get; set; }
        public decimal? EncroachementArea { get; set; }
        public decimal? BuildUpInEncroachementArea { get; set; }
        public string ActionOnEncroachment { get; set; }
        public string EncroachmentDetails { get; set; }

        public string ActionReportFilePath { get; set; }
        public string FencingBoundaryWall { get; set; }
        public decimal? AreaCovered { get; set; }
        public string Dimension { get; set; }
        public string PlotUtilization { get; set; }
        public int? AreaUnit { get; set; }
        public decimal? TotalAreaInBigha { get; set; }
        public decimal? TotalAreaInBiswa { get; set; }
        public decimal? TotalAreaInBiswani { get; set; }
        public decimal? TotalAreaInSqAcreHt { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? TotalArea { get; set; }
        public decimal? AreaUtilised { get; set; }
        public decimal? BalanceArea { get; set; }
        public string Status { get; set; }
        public string PlannedUnplannedLand { get; set; }
        public string MainLandUse { get; set; }
        public string SubUse { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
        [NotMapped]
        public IFormFile SurveyReportFile { get; set; }
        [NotMapped]
        public IFormFile ActionReportFile { get; set; }
        public Landtransfer LandTransfer { get; set; }

    }
}
