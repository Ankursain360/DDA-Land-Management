using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class legalmanagementdto
    {
        public string fileNo { get; set; }
        public string LMFileNo { get; set; }
        public string courtCaseNo { get; set; }
        public string courtCaseTitle { get; set; }
        public string Subject { get; set; }
        public DateTime? HearingDate { get; set; }
        public DateTime? NextHearingDate { get; set; }
        public int? ContemptOfCourt { get; set; }
        public string Courttype { get; set; }
        public string Casestatus { get; set; }
        public string LastDecision { get; set; }
        public string Zone { get; set; }
        public string Locality { get; set; }
        public string CaseType { get; set; }
        public string InFavour { get; set; }
        public string PanelLawyer { get; set; }
        public int? StayInterimGranted { get; set; }
        public string Judgement { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    }
}
