using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Legalmanagementsystem : AuditableEntity<int>
    {
             [Required(ErrorMessage = "File number name is required")]
        public string FileNo { get; set; }
        public string CourtCaseNo { get; set; }
        public string CourtCaseTitle { get; set; }
        public string Subject { get; set; }
        public DateTime? HearingDate { get; set; }
        public DateTime? NextHearingDate { get; set; }
        public int? ContemptOfCourt { get; set; }
        public int? CourtTypeId { get; set; }
        public int? CaseStatusId { get; set; }
        public string LastDecision { get; set; }
        public int? ZoneId { get; set; }
        public int? LocalityId { get; set; }
        public string CaseType { get; set; }
        public string InFavour { get; set; }
        public string PanelLawyer { get; set; }
        public int? StayInterimGranted { get; set; }
        public string StayInterimGrantedDocument { get; set; }
        public string StayInterimGrantedRemarks { get; set; }
        public int? Judgement { get; set; }
        public string JudgementFilePath { get; set; }
        public string JudgementRemarks { get; set; }
        public string DocumentFilePath { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }

        public Casestatus CaseStatus { get; set; }
        public Courttype CourtType { get; set; }
        public Zone Zone { get; set; }
        public Locality Locality { get; set; }

        [NotMapped]
        public List<Casestatus> CasestatusList { get; set; }
        [NotMapped]
        public List<Courttype> CourttypeList { get; set; }

        [NotMapped]
        public List<Zone> ZoneList { get; set; }

        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Legalmanagementsystem> FileNoList { get; set; }
        [NotMapped]
        public List<Legalmanagementsystem> CourtCaseNoList { get; set; }
        [NotMapped]
        public IFormFile DocumentFile { get; set; }

        [NotMapped]
        public IFormFile JudgementFile { get; set; }
        [NotMapped]
        public IFormFile StayFile { get; set; }
    }
}
