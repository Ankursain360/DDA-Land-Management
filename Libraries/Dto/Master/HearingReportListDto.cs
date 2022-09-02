using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class HearingReportListDto
    {
        public string fileNo { get; set; }
        public string courtCaseNo { get; set; }
        public string courtCaseTitle { get; set; }
        public string Subject { get; set; }
        public string HearingDate { get; set; }
        public string NextHearingDate { get; set; }
        public string CaseType { get; set; }
        public string InFavour { get; set; }
        public string PanelLawyer { get; set; }
    }
}
