using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Newlandannexure1 : AuditableEntity<int>
    {
        public Newlandannexure1()
        {
            Newlandannexure1khasrarpt = new HashSet<Newlandannexure1khasrarpt>();
        }

        [Required(ErrorMessage = " Village name is mandatory")]
        public string VillageName { get; set; }
        [Required(ErrorMessage = " Address is mandatory")]
        public string Address { get; set; }
        [Required(ErrorMessage = " Taluk name is mandatory")]
        public string TalukName { get; set; }
        [Required(ErrorMessage = " Municipality is mandatory")]
        public int MunicipalityId { get; set; }
        [Required(ErrorMessage = " District name is mandatory")]
        public int DistrictId { get; set; }
        [Required(ErrorMessage = " Area Unit is mandatory")]
        public string AreaUnit { get; set; }
        [Required(ErrorMessage = " Area is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal Area { get; set; }
       
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? AreaAcquiredEast { get; set; }
       
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? AreaAcquiredWest { get; set; }
       
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? AreaAcquiredNorth { get; set; }
       
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? AreaAcquiredSouth { get; set; }
        [Required(ErrorMessage = " Agricultural Land Area is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal AgriculturalLandArea { get; set; }
        public string Reasons { get; set; }
        public string BuildingNo { get; set; }
        public string BuildingDesc { get; set; }
        public string TanksNo { get; set; }
        public string TanksDesc { get; set; }
        public string WellsNo { get; set; }
        public string WellsDesc { get; set; }
        public string TreesNo { get; set; }
        public string TreesDesc { get; set; }
        public string ReligiousBuildingNo { get; set; }
        public string ReligiousBuildingDesc { get; set; }
        public string TombNo { get; set; }
        public string TombDesc { get; set; }
        public string OthersNo { get; set; }
        public string OthersDesc { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte? IsActive { get; set; }

        public District District { get; set; }
        public Muncipality Municipality { get; set; }
        public ICollection<Newlandannexure1khasrarpt> Newlandannexure1khasrarpt { get; set; }

        [NotMapped]
        public List<District> DistrictList { get; set; }
        [NotMapped]
        public List<Muncipality> MunicipalityList { get; set; }

        //****** Khasra details repeater *****

        [NotMapped]
        public List<string> KhasaNo { get; set; }

        [NotMapped]
        public List<decimal> Bigha { get; set; }

        [NotMapped]
        public List<decimal> Biswa { get; set; }
        [NotMapped]
        public List<decimal> Biswanshi { get; set; }
        [NotMapped]
        public List<string> OwnershipStatus { get; set; }
        [NotMapped]
        public List<string> OwnerName { get; set; }

    }
}
