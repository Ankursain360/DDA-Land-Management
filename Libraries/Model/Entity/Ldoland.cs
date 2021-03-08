using Libraries.Model.Common;
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
        [Required]
        public string SiteDescription { get; set; }
        [Required(ErrorMessage = " Area is mandatory")]
        public decimal? Area { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public string StatusOfLand { get; set; }
        [Required]
        public string OccupiedBy { get; set; }
        [Required]
        public DateTime? DateofPossession { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<LandNotification> LandNotificationList { get; set; }
        public virtual LandNotification LandNotification { get; set; }

        //[NotMapped]
        //public List<Serialnumber> SerialnumberList { get; set; }
        //public virtual Serialnumber Serialnumber { get; set; }
        
    }
}
