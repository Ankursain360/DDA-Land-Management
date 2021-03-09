using Dto.Common;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dto.Master
{
    public class GISKhasraBasisOtherDetailsDto 
    {
        public string VillageName { get; set; }
        public string KhasraNo { get; set; }
        public string Area { get; set; }
        public string Us4 { get; set; }
        public string Us6 { get; set; }
        public string Us17 { get; set; }
        public string Us22 { get; set; }
        public string Award { get; set; }
        public string PossessionDate { get; set; }
        public string AllotmentDate { get; set; }
        public string TransferDepartment { get; set; }
        public string SchemeTransfer { get; set; }
        public string Remarks { get; set; }
        public string PartyName { get; set; }
        public string DemandListNo { get; set; }
        public string LBNo { get; set; }
        public string LACNo { get; set; }
        public string RFANo { get; set; }
        public string SLPNo { get; set; }
        public string Court { get; set; }
        public string PayableAmt { get; set; }
        public string AppealableAmt { get; set; }
        public string Status { get; set; }
        public string CourtCaseNumber { get; set; }
        public string Title { get; set; }
        public string CaseFiledDate { get; set; }
        public string Lawyer { get; set; }
        public string CurrentStatus { get; set; }
        public string NextHearingDate { get; set; }
        public string CourtName { get; set; }
    }
}
