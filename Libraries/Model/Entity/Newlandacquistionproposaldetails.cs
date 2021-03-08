using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Newlandacquistionproposaldetails : AuditableEntity<int>
    {
        public Newlandacquistionproposaldetails()
        {
            Newlandacquistionproposalplotdetails = new HashSet<Newlandacquistionproposalplotdetails>();
        }

        [Required(ErrorMessage = " Proposal Name is mandatory")]
        [Remote(action: "Exist", controller: "NewLandProposalDetails", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Scheme name is mandatory", AllowEmptyStrings = false)]
        public int? SchemeId { get; set; }
        [Required(ErrorMessage = "Required Agency is mandatory")]
        public string RequiredAgency { get; set; }
        [Required(ErrorMessage = "Proposal FileNo  is mandatory")]
        public string ProposalFileNo { get; set; }
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
       

        public string Description { get; set; }
        [Required]
        public DateTime? ProposalDate { get; set; }
        [Required(ErrorMessage = "Status is mandatory ")]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Newlandscheme> SchemeList { get; set; }

        public Newlandscheme Scheme { get; set; }
        public ICollection<Newlandacquistionproposalplotdetails> Newlandacquistionproposalplotdetails { get; set; }
    }
}
