using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
namespace Libraries.Model.Entity
{
    public class Awardmasterdetail : AuditableEntity<int>
    {
      
        public string AwardNumber { get; set; }
        public DateTime? AwardDate { get; set; }
        public int VillageId { get; set; }
        public int ProposalId { get; set; }
        public string Compensation { get; set; }
        public string LandArate { get; set; }
        public string LandCrate { get; set; }
        public string Type { get; set; }
        public string Nature { get; set; }
        public string Purpose { get; set; }
        public int Us4id { get; set; }
        public int Us6id { get; set; }
        public int Us17id { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
        public virtual ICollection<Awardplotdetails> Awardplotdetails { get; set; }


    }
}
