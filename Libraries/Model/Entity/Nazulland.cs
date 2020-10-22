using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    
     public class Nazulland : AuditableEntity<int>
    {

        //[Remote("IsAdvertisement_Exist", "RemotDataEx", AdditionalFields = "AdvertisementNo,AdvertisementID", ErrorMessage = "Entered Advertisement No Already exist in database. Please give unique Advertisement No.")]
        // [Remote(action: "Exist", controller: "Page", AdditionalFields = "Id")]
        [Required(ErrorMessage = "Please fill Khasra number")]
        public string KhasraNo { get; set; }
        [Required(ErrorMessage = "Land Area Acquired feild is required")]

        public string LandAreaAcquired { get; set; }
        [Required(ErrorMessage = "Award Number feild is required")]
        public string AwardNo { get; set; }

        [Required(ErrorMessage = "Please fill Award date feild ")]
        public DateTime? AwardDate { get; set; }
        [Required(ErrorMessage = "Please fill Date Of Possession feild ")]
        public DateTime? DateOfPossession { get; set; }
        [Required(ErrorMessage = "Please fill Area Of Which Possession Taken Over feild ")]
        public string AreaOfWhichPossessionTakenOver { get; set; }
        [Required(ErrorMessage = "Please fill Date On Which Possession Taken Over feild ")]
        public DateTime? DateOnWhichPossessionTakenOver { get; set; }
        [Required(ErrorMessage = "Please fill Amount Of Award feild ")]
        public int? AmountOfAward { get; set; }
        [Required(ErrorMessage = "Please fill Adi Court feild ")]
        public string AdiCourt { get; set; }
        [Required(ErrorMessage = "Please fill High Court  feild ")]
        public string HighCourt { get; set; }
        [Required(ErrorMessage = "Please fill Supreme Court feild ")]
        public string SupremeCourt { get; set; }
        [Required(ErrorMessage = "Please fill Certificate To Correctness Of Entry feild ")]
        public string CertificateToCorrectnessOfEntry { get; set; }
        [Required(ErrorMessage = "Please fill Division feild ")]
        public int? DivisionId { get; set; }
        [Required(ErrorMessage = "Please fill Date Of Transfer feild ")]
        public DateTime? DateOfTransfer { get; set; }
        [Required(ErrorMessage = "Please fill Remarks feild ")]
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Please fill Scheme For Which Acquired feild ")]
        public string SchemeForWhichAcquired { get; set; }
        [Required(ErrorMessage = "Please fill Status feild ")]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
        public virtual Division Division { get; set; }

      
    }
}
