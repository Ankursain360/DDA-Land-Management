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

        [Required(ErrorMessage = " Land Notification name is mandatory")]
        public int LandNotificationId { get; set; }
        [Required(ErrorMessage = " Notofication Date is mandatory")]
        public DateTime? NotificationDate { get; set; }
        //[Required(ErrorMessage = " Serial Number is mandatory")]
        //public int SerialnumberId { get; set; }
        [Required(ErrorMessage = " Property name is mandatory")]
        public string PropertySiteNo { get; set; }
        [Required(ErrorMessage = " Location name is mandatory")]
        [Remote(action: "Exist", controller: "MorLands", AdditionalFields = "Id")]
        public string Name { get; set; }
        public string SiteDescription { get; set; }
        [Required(ErrorMessage = " Area is mandatory")]
        public decimal Area { get; set; }
        //public decimal Bigha { get; set; }
       
        //public decimal Biswa { get; set; }
        // public decimal Biswanshi { get; set; }
        public string StatusOfLand { get; set; }
        public string OccupiedBy { get; set; }
        public string Developed { get; set; }
        public string LandType { get; set; }
        public string Remarks { get; set; }
        public string SerialNumber { get; set; }
        public byte? IsActive { get; set; }





        //[NotMapped]
        //public List<Serialnumber> SerialnumberList { get; set; }
        //public virtual Serialnumber Serialnumber { get; set; }



        [NotMapped]
        public List<LandNotification> LandNotificationList { get; set; }
        public virtual LandNotification LandNotification { get; set; }



    }
}
