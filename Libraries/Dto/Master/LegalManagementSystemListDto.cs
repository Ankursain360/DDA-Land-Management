using Dto.Common;

namespace Dto.Master
{
   public class LegalManagementSystemListDto
    {
        public int Id { get; set; }
        public string LegalfileNo { get; set; }
        public string LMFileNo { get; set; }
        public string courtCaseNo { get; set; }
        public string courtCaseTitle { get; set; }
        public string Subject { get; set; }
        public string HearingDate { get; set; }
        public string NextHearingDate { get; set; }
        public string ContemptOfCourt { get; set; }
        public string Courttype { get; set; }
        public string Casestatus { get; set; }
        public string LastDecision { get; set; }
        public string Zone { get; set; }
        public string Locality { get; set; }
        public string CaseType { get; set; }
        public string InFavour { get; set; }
        public string PanelLawyer { get; set; }
        public string StayInterimGranted { get; set; }
        public string Judgement { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    }
}
