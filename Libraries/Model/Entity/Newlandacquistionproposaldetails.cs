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

        [Required(ErrorMessage = " Proposal Name is mandatory Field")]
        [Remote(action: "Exist", controller: "NewLandProposalDetails", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Scheme name is mandatory", AllowEmptyStrings = false)]
        public int? SchemeId { get; set; }
        [Required(ErrorMessage = "Required Agency is mandatory Field")]
        public string RequiredAgency { get; set; }
        [Required(ErrorMessage = "Proposal FileNo  is mandatory Field")]
        public string ProposalFileNo { get; set; }
        [Required(ErrorMessage = "Bigha is mandatory Field")]
        public decimal Bigha { get; set; }
        [Required(ErrorMessage = "Biswa is mandatory Field")]
        public decimal Biswa { get; set; }
        [Required(ErrorMessage = "Biswanshi is mandatory Field")]
        public decimal Biswanshi { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]
        public DateTime? ProposalDate { get; set; }
        [Required]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Newlandscheme> SchemeList { get; set; }

        public Newlandscheme Scheme { get; set; }
        public ICollection<Newlandacquistionproposalplotdetails> Newlandacquistionproposalplotdetails { get; set; }
    }
}
