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
        public Requestforproceeding()
        {
            Actiontakenbydda = new HashSet<Actiontakenbydda>();
            Allotteeevidenceupload = new HashSet<Allotteeevidenceupload>();
            Evidancedoc = new HashSet<Evidancedoc>();
            Hearingdetails = new HashSet<Hearingdetails>();
            Leasenoticegeneration = new HashSet<Leasenoticegeneration>();
            Judgement = new HashSet<Judgement>();
        }
        [Required(ErrorMessage = "FIle No is mandatory ")]
        public int? AllotmentId { get; set; }
        [Required(ErrorMessage = "Letter Reference No is mandatory ")]
        public string LetterReferenceNo { get; set; }
        [Required(ErrorMessage = "Subject is mandatory ")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Ground of violation is mandatory ")]
        public string GroundOfViolations { get; set; }
        [Required(ErrorMessage = "Date of cancellation of lease is mandatory ")]
        public DateTime? DateOfCancellationofLease { get; set; }
        [Required(ErrorMessage = "Honble Name is mandatory ")]
        public int? HonebleLgOrCommon { get; set; }
        [Required(ErrorMessage = "Proceeding for eviction")]
        public string ProceedingEvictionPossession { get; set; }
        [Required(ErrorMessage = "Court Case is mandatory ")]
        public string CourtCaseifAny { get; set; }

        public string DemandLetter { get; set; }
        public string Noc { get; set; }
        public string CancellationOrder { get; set; }
        public byte? IsActive { get; set; }
        [Required(ErrorMessage = "Username is mandatory ")]
        public int? UserId { get; set; }
        public string ProcedingLetter { get; set; }
        public int? IsGenerate { get; set; }
        public int? IsUpload { get; set; }
        public int? IsSend { get; set; }
        public int? PendingAt { get; set; }
        public int? Status { get; set; }

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

        [NotMapped]
        public string ApprovalStatus { get; set; }

        [NotMapped]
        public string ApprovalRemarks { get; set; }

        [NotMapped]
        public IFormFile ApprovalDocument { get; set; }

        [NotMapped]
        public List<Approvalstatus> ApprovalStatusList { get; set; }
        public ICollection<Actiontakenbydda> Actiontakenbydda { get; set; }
        public ICollection<Allotteeevidenceupload> Allotteeevidenceupload { get; set; }
        public ICollection<Leasenoticegeneration> Leasenoticegeneration { get; set; }


        public ICollection<Judgement> Judgement { get; set; }
        public ICollection<Evidancedoc> Evidancedoc { get; set; }
        public ICollection<Hearingdetails> Hearingdetails { get; set; }


        
    }
}
