using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Doortodoorsurvey : AuditableEntity<int>
    {

        [Required(ErrorMessage = " The Property Address field is required")]
        public string PropertyAddress { get; set; }
        
        [Required(ErrorMessage = " The Geo Referencing/Lattitude field is required")]
        public string GeoReferencingLattitude { get; set; }

        [Required(ErrorMessage = " The Geo Referencing/Longitude field is required")]
        public string Longitude { get; set; }

        [Required(ErrorMessage = " The PresentUse field is required")]
        public int? PresentUseId { get; set; }

        [Required(ErrorMessage = " The Approx Property Area field is required")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Approx Area of the Property; Max 18 digits")]
        public decimal? ApproxPropertyArea { get; set; }

        [Required(ErrorMessage = " The Number Of Floors field is required")]
        public string NumberOfFloors { get; set; }
        
        public string CaelectricityNo { get; set; }
        
        public string KwaterNo { get; set; }
        
        public string PropertyHouseTaxNo { get; set; }
        [Required(ErrorMessage = " The Occupant Name field is required")]
        public string OccupantName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        

        [Required(ErrorMessage = " The Mobile No field is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Invalid Mobile No Format.")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = " The Occupant Aadhar No field is required")]
        public string OccupantAadharNo { get; set; }
        [Required(ErrorMessage = " The Voter Id No field is required")]
        public string VoterIdNo { get; set; }
       
        public string OccupantIdentityPrrofFilePath { get; set; }
                
        public string DamagePaidPast { get; set; }
        public string PropertyFilePath { get; set; }
        public string Remarks { get; set; }

        public byte IsActive { get; set; }



        [NotMapped]
        public List<Presentuse> PresentuseList { get; set; }
        public virtual Presentuse PresentUseNavigation { get; set; }

        [NotMapped]
        public IFormFile DocumentPhoto { get; set; }
        [NotMapped]
        public IFormFile PropertyPhoto { get; set; }


  


    }
}
