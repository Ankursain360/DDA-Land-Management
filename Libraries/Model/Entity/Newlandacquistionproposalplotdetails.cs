using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Newlandacquistionproposalplotdetails : AuditableEntity<int>
    {

        [Required(ErrorMessage = "Proposal plot is Mandatory Field", AllowEmptyStrings = false)]
        public int? ProposaldetailsId { get; set; }
        [Required(ErrorMessage = "Village is Mandatory Field", AllowEmptyStrings = false)]
        public int? AcquiredlandvillageId { get; set; }
        [Required(ErrorMessage = "Khasra is Mandatory Field", AllowEmptyStrings = false)]
        public int? KhasraId { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
       
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Newlandacquistionproposaldetails> ProposaldetailsList { get; set; }

        [NotMapped]
        public List<Newlandvillage> AcquiredlandvillageList { get; set; }

        [NotMapped]
        public List<Newlandkhasra> KhasraList { get; set; }

        public Newlandvillage Acquiredlandvillage { get; set; }
        public Newlandkhasra Khasra { get; set; }
        public Newlandacquistionproposaldetails Proposaldetails { get; set; }
    }
}
