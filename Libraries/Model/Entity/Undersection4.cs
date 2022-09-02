using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Undersection4: AuditableEntity<int>
    {
        public Undersection4()
        {
            
            Undersection4plot = new HashSet<Undersection4plot>();
            Undersection6 = new HashSet<Undersection6>();
            Awardmasterdetail = new HashSet<Awardmasterdetail>();
            Newlandawardmasterdetail = new HashSet<Newlandawardmasterdetail>();
        }
        [Required(ErrorMessage = "Proposal is mandatory")]
        public int ProposalId { get; set; }
        [Required(ErrorMessage = "Notification No(U/S-4) is mandatory")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Notification Date is mandatory")]
        public DateTime? Ndate { get; set; }
        [Required(ErrorMessage = "Purpose is mandatory")]
        public string Npurpose { get; set; }
        public string TypeDetails { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
        public string DocumentName { get; set; }

        public string BoundaryDescription { get; set; }

      
        [NotMapped]
        public List<Proposaldetails> ProposalList { get; set; }
     
        public Proposaldetails Proposal { get; set; }
       
        public ICollection<Undersection4plot> Undersection4plot { get; set; }
        public ICollection<Undersection6> Undersection6 { get; set; }
        public ICollection<Awardmasterdetail> Awardmasterdetail { get; set; }
        public ICollection<Newlandawardmasterdetail> Newlandawardmasterdetail { get; set; }
        public ICollection<Newlandpossessiondetails> NewlandPossessiondetails { get; set; }

        [NotMapped]
        public IFormFile DocumentIFormFile { get; set; }

    }
}
