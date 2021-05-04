using Dto.Common;

namespace Dto.Master
{
    public class LegalReportListDto
    {
        public int Id { get; set; }
        public string LegalFileNo { get; set; }
        public string CaseNo { get; set; }
        public string CaseTitle { get; set; }
        public string Subject { get; set; }

        public string HearingDate { get; set; }

        public string NextDateOfHearing { get; set; }
        public string COC { get; set; }
        public string CourtType { get; set; }

        public string CaseStatus { get; set; }

        public string LastDecision { get; set; }

        public string Zone { get; set; }
        public string Locality { get; set; }

        public string CaseType { get; set; }
        public string InFavour { get; set; }
        public string PanelLawyer { get; set; }

        public string StayInterimGranted { get; set; }
        public string Jugdement { get; set; }

        public string Remarks { get; set; }
    }
}
