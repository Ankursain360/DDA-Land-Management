using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
   public class Requestforproceeding : AuditableEntity<int>
    {


        public int? AllotmentId { get; set; }
        public string LetterReferenceNo { get; set; }
        public string Subject { get; set; }
        public string GroundOfViolations { get; set; }
        public DateTime? DateOfCancellationofLease { get; set; }
        public int? HonebleLgOrCommon { get; set; }
        public string ProceedingEvictionPossession { get; set; }
        public string CourtCaseifAny { get; set; }

        public string DemandLetter { get; set; }
        public string Noc { get; set; }
        public string CancellationOrder { get; set; }


        public byte? IsActive { get; set; }
     

        public Allotmententry Allotment { get; set; }

        public Honble HonebleLgOrCommonNavigation { get; set; }


    }
}
