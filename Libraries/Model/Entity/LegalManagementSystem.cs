using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Libraries.Model.Entity
{
  public  class LegalManagementSystem : AuditableEntity<int>
    {
        public static object ZoneId;

        public LegalManagementSystem()
        { }
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
        public int? Zone { get; set; }
        public int? Village { get; set; }
        public int? CaseType { get; set; }
        public int? InFavour { get; set; }
        public string PanelLawyer { get; set; }
        public int? StayInterimGranted { get; set; }
        public int? Judgement { get; set; }
        public string JudgementFilePath { get; set; }
        public string Remarks { get; set; }
       
        public Locality VillageNavigation { get; set; }
        public Zone ZoneNavigation { get; set; }
       
        public object Legaldetails { get; internal set; }

        public ICollection<LegalManagementSystem> LegalManagementSystems { get; set; }
        public object LocalityList { get; set; }
        public List<Zone> ZoneList { get; set; }

       
    }
}
