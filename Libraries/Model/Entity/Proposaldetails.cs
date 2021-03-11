using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public partial class Proposaldetails: AuditableEntity<int>
    {

        public Proposaldetails()
        {
            Proposalplotdetails = new HashSet<Proposalplotdetails>();
            Undersection4 = new HashSet<Undersection4>();
            Awardmasterdetail = new HashSet<Awardmasterdetail>();
            Newlandawardmasterdetail = new HashSet<Newlandawardmasterdetail>();
        }

        [Required]
        //[Remote("IsAdvertisement_Exist", "RemotDataEx", AdditionalFields = "AdvertisementNo,AdvertisementID", ErrorMessage = "Entered Advertisement No Already exist in database. Please give unique Advertisement No.")]
        [Remote(action: "Exist", controller: "ProposalDetails", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required (ErrorMessage ="Scheme is mandatory")]
        public int? SchemeId { get; set; }
        [Required(ErrorMessage = "Required Agency is mandatory")]
        public string RequiredAgency { get; set; }
        [Required]
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
        [Required(ErrorMessage = "Description  is mandatory")]

        public string Description { get; set; }
        [Required]
        public DateTime? ProposalDate { get; set; }
        [Required(ErrorMessage = "Status is mandatory ")]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Scheme> SchemeList { get; set; }
        public virtual Scheme Scheme { get; set; }
        public ICollection<Proposalplotdetails> Proposalplotdetails { get; set; }
        public virtual ICollection<Undersection4> Undersection4 { get; set; }
        public ICollection<Awardmasterdetail> Awardmasterdetail { get; set; }
        public ICollection<Newlandawardmasterdetail> Newlandawardmasterdetail { get; set; }

    }
}
