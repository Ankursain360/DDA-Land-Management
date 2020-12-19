using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Doortodoorsurvey : AuditableEntity<int>
    {

        public Doortodoorsurvey()
        {
            Familydetails = new HashSet<Familydetails>();

        }

        public string PropertyAddress { get; set; }
        public string MuncipalNo { get; set; }
        public string GeoReferencing { get; set; }
        public int? PresentUseId { get; set; }
        public decimal? ApproxPropertyArea { get; set; }
        public string NumberOfFloors { get; set; }
        public string CaelectricityNo { get; set; }
        public string KwaterNo { get; set; }
        public string PropertyHouseTaxNo { get; set; }
        public string OccupantName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
        public string MobileNo { get; set; }
        public string OccupantAadharNo { get; set; }
        public string VoterIdNo { get; set; }
        public string OccupantIdentityPrrofFilePath { get; set; }
        public string DamagePaidPast { get; set; }
        public string PropertyFilePath { get; set; }
        public string Remarks { get; set; }


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
