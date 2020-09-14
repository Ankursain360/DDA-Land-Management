using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Proposaldetails: AuditableEntity<int>
    {
        [Required]
        //[Remote("IsAdvertisement_Exist", "RemotDataEx", AdditionalFields = "AdvertisementNo,AdvertisementID", ErrorMessage = "Entered Advertisement No Already exist in database. Please give unique Advertisement No.")]
        [Remote(action: "Exist", controller: "ProposalDetails", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required]
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
        //[NotMapped]
        //public List<Scheme> SchemeList { get; set; }
        //public virtual Scheme Scheme { get; set; }


    }
}
