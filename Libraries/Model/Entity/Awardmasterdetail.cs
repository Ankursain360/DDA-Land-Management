using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Awardmasterdetail : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Award Number is mandatory")]
        [Remote(action: "Exist", controller: "Awardmasterdetails", AdditionalFields = "Id")]
        public string AwardNumber { get; set; }
        [Required(ErrorMessage = "Award Date is mandatory")]
        public DateTime? AwardDate { get; set; }
        [Required(ErrorMessage = "Village is mandatory", AllowEmptyStrings = false)]
        public int VillageId { get; set; }
        [Required(ErrorMessage = "Proposal name is mandatory", AllowEmptyStrings = false)]
        public int ProposalId { get; set; }
        [Required(ErrorMessage = "Compensation is mandatory")]
        public string Compensation { get; set; }

        public string Rate1 { get; set; }
        public string Rate2 { get; set; }
        [Required(ErrorMessage = "Rate is mandatory")]
        public string Rate3 { get; set; }
        public string Rate4 { get; set; }
        public string Type { get; set; }
        [Required(ErrorMessage = "Nature is mandatory")]
        public string Nature { get; set; }
        [Required(ErrorMessage = "Purpose is mandatory")]
        public string Purpose { get; set; }
        [Required(ErrorMessage = "Undersection 4 is mandatory")]
        public int US4Id { get; set; }
        [Required(ErrorMessage = "Undersection 6 is mandatory")]
        public int US6Id { get; set; }
        [Required(ErrorMessage = "Undersection 17  is mandatory")]
        public int US17Id { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
      
        
        public Proposaldetails Proposal { get; set; }
       
        public Undersection17 Us17 { get; set; }
       
        public Undersection4 Us4 { get; set; }
       
        public Undersection6 Us6 { get; set; }
        public Acquiredlandvillage Acquiredlandvillage { get; set; }
        public ICollection<Awardplotdetails> Awardplotdetails { get; set; }
        [NotMapped]
        public List<Undersection6> section6List { get; set; }
        [NotMapped]
        public List<Undersection4> section4List { get; set; }
        [NotMapped]
        public List<Undersection17> section17List { get; set; }
        [NotMapped]
        public List<Proposaldetails> purposalList { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> AcquiredlandvillageList { get; set; }
    }
}
