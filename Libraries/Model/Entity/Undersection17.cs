using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Undersection17 : AuditableEntity<int>
    {

        public int? UnderSection6Id { get; set; }
        public int? LandNotificationId { get; set; }
        public DateTime? NotificationDate { get; set; }

        public byte IsActive { get; set; }
        [NotMapped]
        public List<Undersection6> Undersection6List { get; set; }
        public virtual Undersection6 Undersection6 { get; set; }



        [NotMapped]
        public List<LandNotification> LandNotificationList { get; set; }
        public virtual LandNotification LandNotification { get; set; }

    }
}
