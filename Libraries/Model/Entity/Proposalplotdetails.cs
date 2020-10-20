using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
   public partial class Proposalplotdetails : AuditableEntity<int>
    {
        [Required]
        public int? ProposaldetailsId { get; set; }
        [Required]
        public int? LocalityId { get; set; }
        [Required]
        public int? KhasraId { get; set; }
        [Required]
        public decimal Bigha { get; set; }
        [Required]
        public decimal Biswa { get; set; }
        [Required]
        public decimal Biswanshi { get; set; }
        [Required]
        public byte? IsActive { get; set; }

        [NotMapped]
        public List<Proposaldetails> ProposaldetailsList { get; set; }
        public virtual Proposaldetails Proposaldetails { get; set; }


        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        public virtual Locality Locality { get; set; }
   

        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }

       
    }
}
