using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Dto.Master;
using Microsoft.AspNetCore.Http;


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
        public int? UserId { get; set; }
        public string ProcedingLetter { get; set; }
        public int? IsGenerate { get; set; }
        public int? IsUpload { get; set; }        
        public int? IsSend { get; set; }
        public int? PendingAt { get; set; }

        public Allotmententry Allotment { get; set; }

        public Honble Honble { get; set; }
        public ApplicationUser User { get; set; }

        [NotMapped]
        public List<Allotmententry> AllotmententryList { get; set; }
        [NotMapped]
        public List<Honble> HonbleList { get; set; }

        [NotMapped]
        public IFormFile DemandLetterPhoto { get; set; }

        [NotMapped]
        public IFormFile NocPhoto { get; set; }

        [NotMapped]
        public IFormFile CancellationPhoto { get; set; }

        [NotMapped]
        public IFormFile ProcedingLetterDocument { get; set; }

        [NotMapped]
        public int checkIsSend { get; set; }

        [NotMapped]
        public List<UserBindDropdownDto> UserNameList { get; set; }


    }
}
