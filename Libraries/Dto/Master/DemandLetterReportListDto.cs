
using Dto.Common;
using System;

namespace Dto.Master
{
    public class DemandLetterReportListDto
    {
        
        public int Id { get; set; }
        public string Loaclity { get; set; }
        public string FileNo { get; set; }
        
        public string PayeeName { get; set; }
        public string PropertyNumber { get; set; }
        public string DemandNo { get; set; }
        public string DemandDate { get; set; }
        public string DemandPeriodFromDate { get; set; }
        public string DemandPeriodToDate { get; set; }
        public string DemandAmount { get; set; }

    }
}
