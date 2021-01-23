using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Doortodoorsurvey : AuditableEntity<int>
    {

        public Doortodoorsurvey()
        {
            Familydetails = new HashSet<Familydetails>();

        }
        [Required(ErrorMessage = " The Property Address field is required")]
        public string PropertyAddress { get; set; }
        [Required(ErrorMessage = " The Muncipal No field is required")]
        public string MuncipalNo { get; set; }
        [Required(ErrorMessage = " The Geo Referencing field is required")]
        public string GeoReferencing { get; set; }
        [Required(ErrorMessage = " The PresentUse field is required")]
        public int? PresentUseId { get; set; }
        [Required(ErrorMessage = " The Approx Property Area field is required")]
        public decimal? ApproxPropertyArea { get; set; }
        [Required(ErrorMessage = " The Number Of Floors field is required")]
        public string NumberOfFloors { get; set; }
        [Required(ErrorMessage = " The Caelectricity No field is required")]
        public string CaelectricityNo { get; set; }
        [Required(ErrorMessage = " The Kwater No field is required")]
        public string KwaterNo { get; set; }
        [Required(ErrorMessage = " The Property House Tax No field is required")]
        public string PropertyHouseTaxNo { get; set; }
        [Required(ErrorMessage = " The Occupant Name field is required")]
        public string OccupantName { get; set; }
        [Required(ErrorMessage = " The Address field is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = " The Email field is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = " The Telephone No field is required")]
        public string TelephoneNo { get; set; }
        [Required(ErrorMessage = " The Mobile No field is required")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = " The Occupant Aadhar No field is required")]
        public string OccupantAadharNo { get; set; }
        [Required(ErrorMessage = " The Voter Id No field is required")]
        public string VoterIdNo { get; set; }
        [Required(ErrorMessage = " The Occupant Identity PrrofFile field is required")]
        public string OccupantIdentityPrrofFilePath { get; set; }

        [Required(ErrorMessage = "Damage Paid Past")]
        public string DamagePaidPast { get; set; }
        public string PropertyFilePath { get; set; }
        public string Remarks { get; set; }

        public byte IsActive { get; set; }


        [NotMapped]

        public List<string> Name { get; set; }

        [NotMapped]

        public List<string> Age { get; set; }
        [NotMapped]

        public List<string> FGender { get; set; }



        [NotMapped]
        public List<Presentuse> PresentuseList { get; set; }
        public virtual Presentuse PresentUseNavigation { get; set; }


        public ICollection<Familydetails> Familydetails { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }


    }
}
