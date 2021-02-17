using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Awardmasterdetail : AuditableEntity<int>
    {

        public string AwardNumber { get; set; }
        public DateTime? AwardDate { get; set; }
        public int VillageId { get; set; }
        public int ProposalId { get; set; }
        public string Compensation { get; set; }
        public string Rate1 { get; set; }
        public string Rate2 { get; set; }
        public string Rate3 { get; set; }
        public string Rate4 { get; set; }
        public string Type { get; set; }
        public string Nature { get; set; }
        public string Purpose { get; set; }
        public int Us4id { get; set; }
        public int Us6id { get; set; }
        public int Us17id { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
       // public virtual ICollection<Awardplotdetails> Awardplotdetails { get; set; }
        
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
