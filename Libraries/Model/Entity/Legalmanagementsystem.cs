using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Legalmanagementsystem : AuditableEntity<int>
    {
       

        public string FileNo { get; set; }
        public string CourtCaseNo { get; set; }
        public string CourtCaseTitle { get; set; }
        public string Subject { get; set; }
        public DateTime? HearingDate { get; set; }
        public DateTime? NextHearingDate { get; set; }
        public int? ContemptOfCourt { get; set; }
        public int? CourtType { get; set; }
        public int? CaseStatus { get; set; }
        public string LastDecision { get; set; }
        public int? ZoneId { get; set; }
        public int? LocalityId { get; set; }
        public int? CaseType { get; set; }
        public int? InFavour { get; set; }
        public string PanelLawyer { get; set; }
        public int? StayInterimGranted { get; set; }
        public int? Judgement { get; set; }
        public string JudgementFilePath { get; set; }
        public string Remarks { get; set; }
      

        public Locality Locality { get; set; }
        public Zone Zone { get; set; }

        [NotMapped]
        public List<Zone> ZoneList { get; set; }

        [NotMapped]
        public List<Locality> LocalityList { get; set; }
    }
}
