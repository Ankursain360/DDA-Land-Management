using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public  class Ldoland : AuditableEntity<int>
    {
        [Required(ErrorMessage = " Notification is mandatory")]
        public int? LandNotificationId { get; set; }
        [Required(ErrorMessage = " Date is mandatory")]
        public DateTime? NotificationDate { get; set; }
        [Required(ErrorMessage = " Serial Number is mandatory")]
        public int? SerialNumber{ get; set; }
        [Required(ErrorMessage = " Site No is mandatory")]
        public string PropertySiteNo { get; set; }
        [Required(ErrorMessage = "Location is mandatory")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Site Description is mandatory")]
        public string SiteDescription { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = " Area is mandatory")]
        public decimal? Area { get; set; }
        [Required(ErrorMessage = " Status of Land is mandatory")]
        public string StatusOfLand { get; set; }
        [Required(ErrorMessage = " Occupied By is mandatory")]
        public string OccupiedBy { get; set; }
        [Required(ErrorMessage = " Date of Possesion is mandatory")]
        public DateTime? DateofPossession { get; set; }

        public string GOINotificationDocumentName { get; set; }

        public string OrderDocumentName { get; set; }

        public string PossessionDocumentName { get; set; }

        public DateTime? DateOfLandResume { get; set; }

        [Required(ErrorMessage = " Remarks is mandatory")]
        public string Remarks { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<LandNotification> LandNotificationList { get; set; }
        public virtual LandNotification LandNotification { get; set; }

        [NotMapped]
        public IFormFile GOINotificationDocumentIFormFile { get; set; }

        [NotMapped]
        public IFormFile OrderDocumentIFormFile { get; set; }

        [NotMapped]
        public IFormFile PossessionDocumentIFormFile { get; set; }


    }
}
