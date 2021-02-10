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
        public Undersection4()
        {
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
            Undersection4plot = new HashSet<Undersection4plot>();
            Undersection6 = new HashSet<Undersection6>();
        }
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
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }
        public ICollection<Undersection4plot> Undersection4plot { get; set; }
        public ICollection<Undersection6> Undersection6 { get; set; }
    }
}
