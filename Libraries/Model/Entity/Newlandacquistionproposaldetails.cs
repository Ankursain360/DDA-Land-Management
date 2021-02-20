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

        [Remote(action: "Exist", controller: "NewLandProposalDetails", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Scheme is mandatory feild")]
        public int? SchemeId { get; set; }
        [Required]
        public string RequiredAgency { get; set; }
        [Required]
        public string ProposalFileNo { get; set; }
        [Required]
        public decimal Bigha { get; set; }
        [Required]
        public decimal Biswa { get; set; }
        [Required]
        public decimal Biswanshi { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]
        public DateTime? ProposalDate { get; set; }
        [Required]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Scheme> SchemeList { get; set; }

        public Scheme Scheme { get; set; }
        public ICollection<Newlandacquistionproposalplotdetails> Newlandacquistionproposalplotdetails { get; set; }
    }
}
