using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Cancellationentry : AuditableEntity<int>
    {

        [Required(ErrorMessage = "File No is mandatory ")]
        public int? AllotmentId { get; set; }
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

        public Allotmententry Allotment { get; set; }
        public Honble HonebleLgOrCommonNavigation { get; set; }

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
    }
}
