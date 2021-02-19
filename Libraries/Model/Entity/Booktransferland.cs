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
        
        public int? LandNotificationId { get; set; }
        [Required(ErrorMessage = "  Notification Date is mandatory")]
        public DateTime? NotificationDate { get; set; }
        public int? LocalityId { get; set; }
        public int? KhasraId { get; set; }
        [Required(ErrorMessage = " Part is mandatory")]
        public string Part { get; set; }
   //  [Required(ErrorMessage = " Area is mandatory")]
        public decimal? Area { get; set; }
       
        public string StatusOfLand { get; set; }
        public DateTime? DateofPossession { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }



        [NotMapped]
        public List<LandNotification> LandNotificationList { get; set; }
        public virtual LandNotification LandNotification { get; set; }


        [NotMapped]
        public List<Acquiredlandvillage> LocalityList { get; set; }
        public virtual Acquiredlandvillage Locality { get; set; }
       


        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }

       
    }
}
