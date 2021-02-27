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
        public Undersection17()
        {
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();

            Awardmasterdetail = new HashSet<Awardmasterdetail>();
            Newlandawardmasterdetail = new HashSet<Newlandawardmasterdetail>();
        }
        [Required(ErrorMessage = "Notification 6 is Mandatory Field", AllowEmptyStrings = false)]
        public int? UnderSection6Id { get; set; }
        [Required(ErrorMessage = "Notification 17 is mandatory")]
        public string Number { get; set; }
        public DateTime? NotificationDate { get; set; }
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Undersection6> Undersection6List { get; set; }
        public virtual Undersection6 UnderSection6 { get; set; }



        //[NotMapped]
        //public List<LandNotification> LandNotificationList { get; set; }
        //public virtual LandNotification LandNotification { get; set; }
      
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }
        public ICollection<Undersection17plotdetail> Undersection17plotdetail { get; set; }
        public ICollection<Awardmasterdetail> Awardmasterdetail { get; set; }
        public ICollection<Newlandawardmasterdetail> Newlandawardmasterdetail { get; set; }
    }
}
