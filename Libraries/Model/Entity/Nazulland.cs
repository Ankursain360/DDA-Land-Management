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
        [Required]
        //[Remote("IsAdvertisement_Exist", "RemotDataEx", AdditionalFields = "AdvertisementNo,AdvertisementID", ErrorMessage = "Entered Advertisement No Already exist in database. Please give unique Advertisement No.")]
       // [Remote(action: "Exist", controller: "Page", AdditionalFields = "Id")]

        public string KhasraNo { get; set; }
        [Required]
        public string LandAreaAcquired { get; set; }
        [Required]
        public string AwardNo { get; set; }
        [Required]
        public DateTime? AwardDate { get; set; }
        [Required]
        public DateTime? DateOfPossession { get; set; }
        [Required]
        public string AreaOfWhichPossessionTakenOver { get; set; }
        [Required]
        public DateTime? DateOnWhichPossessionTakenOver { get; set; }
        [Required]
        public int? AmountOfAward { get; set; }
        [Required]
        public string AdiCourt { get; set; }
        [Required]
        public string HighCourt { get; set; }
        [Required]
        public string SupremeCourt { get; set; }
        [Required]
        public string CertificateToCorrectnessOfEntry { get; set; }
        [Required]
        public int? DivisionId { get; set; }
        [Required]
        public DateTime? DateOfTransfer { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public string SchemeForWhichAcquired { get; set; }
        [Required]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
        public virtual Division Division { get; set; }

      
    }
}
