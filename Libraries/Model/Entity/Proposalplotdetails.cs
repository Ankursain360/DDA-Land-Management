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
        [Required(ErrorMessage = "Proposal name is mandatory", AllowEmptyStrings = false)]
        public int? ProposaldetailsId { get; set; }
        [Required(ErrorMessage = "Village name is mandatory", AllowEmptyStrings = false)]
        public int? AcquiredlandvillageId { get; set; }
        [Required(ErrorMessage = "Khasra  No is mandatory", AllowEmptyStrings = false)]
        public int? KhasraId { get; set; }
        [Required(ErrorMessage = "Bigha is mandatory")]
        public int Bigha { get; set; }
        [Required(ErrorMessage = "Biswa is mandatory")]
        public int Biswa { get; set; }
        [Required(ErrorMessage = "Biswanshi is mandatory")]
        public int Biswanshi { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte? IsActive { get; set; }

        [NotMapped]
        public List<Proposaldetails> ProposaldetailsList { get; set; }

        [NotMapped]
        public List<Acquiredlandvillage> AcquiredlandvillageList { get; set; }

        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
       

        public Acquiredlandvillage Acquiredlandvillage { get; set; }
        public Khasra Khasra { get; set; }
        public Proposaldetails Proposaldetails { get; set; }

    }
}
