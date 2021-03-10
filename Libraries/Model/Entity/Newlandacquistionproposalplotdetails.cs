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

        [Required(ErrorMessage = "Proposal plot is Mandatory", AllowEmptyStrings = false)]
        public int? ProposaldetailsId { get; set; }
        [Required(ErrorMessage = "Village is Mandatory", AllowEmptyStrings = false)]
        public int? AcquiredlandvillageId { get; set; }
        [Required(ErrorMessage = "Khasra is Mandatory", AllowEmptyStrings = false)]
        public int? KhasraId { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Bigha is mandatory")]
        public decimal Bigha { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Biswa is mandatory")]
        public decimal Biswa { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Biswanshi is mandatory")]
        public decimal Biswanshi { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
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
