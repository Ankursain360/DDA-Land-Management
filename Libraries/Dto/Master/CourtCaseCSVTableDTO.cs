using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class CourtCaseCSVTableDTO
    {
        public string FileNo { get; set; }
        public string CourtCaseNo { get; set; }
        public string CourtCaseTitle { get; set; }
        public string Subject { get; set; }
        public int? ContemptOfCourt { get; set; }
        public int? CourtTypeId { get; set; }
        public int? CaseStatusId { get; set; }
        public string LastDecision { get; set; }
        public int? ZoneId { get; set; }
        public int? LocalityId { get; set; }
        public string PanelLawyer { get; set; }
        public int? StayInterimGranted { get; set; }
        public int? Judgement { get; set; }
        public string DocumentFilePath { get; set; }

    }
}
