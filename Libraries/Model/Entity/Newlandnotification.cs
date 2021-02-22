using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity 
{
   public class Newlandnotification :  AuditableEntity<int>
    {
        public int NotificationTypeId { get; set; }
        public string NotificationNo { get; set; }
        public DateTime Date { get; set; }
        public string GazetteNotificationFilePath { get; set; }
        public string Remarks { get; set; }
        public int IsActive { get; set; }
       // public Newlandnotification Newlandnotification { get; set; }
        [NotMapped]
        public List<NewlandNotificationtype> notificationtypeList { get; set; }

    }
}
