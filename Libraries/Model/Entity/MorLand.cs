using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Morland : AuditableEntity<int>
    {

       
        public int NotificationId { get; set; }
        public DateTime? NotificationDate { get; set; }
        public int SerialNoId { get; set; }
        public string PropertySiteNo { get; set; }
        public string Name { get; set; }
        public string SiteDescription { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string StatusOfLand { get; set; }
        public string OccupiedBy { get; set; }
        public string Developed { get; set; }
        public string LandType { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }





        [NotMapped]
        public List<Serialnumber> SerialnumberList { get; set; }
        public virtual Serialnumber Serialnumber { get; set; }



        [NotMapped]
        public List<LandNotification> LandNotificationList { get; set; }
        public virtual LandNotification LandNotification { get; set; }



    }
}
