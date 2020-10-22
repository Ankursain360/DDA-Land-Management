using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public  class Currentstatusoflandhistory : AuditableEntity<int>
    {
       
        public int? LandTransferId { get; set; }
        public string Tsssurvey { get; set; }
        public string SurveyReportFilePath { get; set; }
        public string Encroachment { get; set; }
        public decimal? EncroachedArea { get; set; }
        public string ActionOnEncroachment { get; set; }
        public string ActionReportFilePath { get; set; }
        public string FencingBoundaryWall { get; set; }
        public decimal? AreaCovered { get; set; }
        public string Dimension { get; set; }
        public string PlotUtilization { get; set; }
        public decimal? AreaUtilised { get; set; }
        public decimal? BalanceArea { get; set; }
        public string Status { get; set; }
        public string PlannedUnplannedLand { get; set; }
        public string MainLandUse { get; set; }
        public string SubUse { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
       

        public Landtransfer LandTransfer { get; set; }
    }
}
