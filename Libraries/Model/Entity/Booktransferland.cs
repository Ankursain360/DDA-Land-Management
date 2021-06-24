using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Booktransferland : AuditableEntity<int>
   
    {
        [Required(ErrorMessage = " Notification No is mandatory")]
        public int? OtherLandNotificationId { get; set; }
        [Required(ErrorMessage = "  Notification Date is mandatory")]
        public DateTime? NotificationDate { get; set; }
        [Required(ErrorMessage = " Village is mandatory")]
        public int? LocalityId { get; set; }
        [Required(ErrorMessage = " Khasra is mandatory")]
        public int? KhasraId { get; set; }
        [Required(ErrorMessage = " Part is mandatory")]
        public string Part { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        
        public decimal? Area { get; set; }
       
        public string StatusOfLand { get; set; }
        public DateTime? DateofPossession { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Otherlandnotification> OtherlandnotificationList { get; set; }
        public Otherlandnotification OtherLandNotification { get; set; }

        //[NotMapped]
        //public List<LandNotification> LandNotificationList { get; set; }
        //public virtual LandNotification LandNotification { get; set; }


        [NotMapped]
        public List<Acquiredlandvillage> LocalityList { get; set; }
        public virtual Acquiredlandvillage Locality { get; set; }
       


        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }

       
    }
}
