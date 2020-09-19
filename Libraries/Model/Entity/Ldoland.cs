using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public  class Ldoland : AuditableEntity<int>
    {
        [Required]
        public int? LandNotificationId { get; set; }
        [Required]
        public DateTime? NotificationDate { get; set; }
        [Required]
        public int? SerialnumberId { get; set; }
        [Required]
        public string PropertySiteNo { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string SiteDescription { get; set; }
        [Required]
        public decimal? Bigha { get; set; }
        [Required]
        public decimal? Biswa { get; set; }
        [Required]
        public decimal? Biswanshi { get; set; }
        [Required]
        public string StatusOfLand { get; set; }
        [Required]
        public string OccupiedBy { get; set; }
        [Required]
        public DateTime? DateofPossession { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<LandNotification> LandNotificationList { get; set; }
        public virtual LandNotification LandNotification { get; set; }

        [NotMapped]
        public List<Serialnumber> SerialnumberList { get; set; }
        public virtual Serialnumber Serialnumber { get; set; }

    }
}
