using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Undersection4: AuditableEntity<int>
    {

        public int ProposalId { get; set; }
        public string Number { get; set; }
        public DateTime? Ndate { get; set; }
        public string Npurpose { get; set; }
        public string TypeDetails { get; set; }
        public byte IsActive { get; set; }

        public string BoundaryDescription { get; set; }

      
        [NotMapped]
        public List<Proposaldetails> ProposalList { get; set; }
     
        public Proposaldetails Proposal { get; set; }
        public virtual ICollection<Undersection4plot> Undersection4plot { get; set; }

    }
}
