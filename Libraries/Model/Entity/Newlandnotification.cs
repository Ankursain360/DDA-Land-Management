using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity 
{
    public class Newlandnotification : AuditableEntity<int>
    {
        public Newlandnotification()
        {
            Newlandnotificationfilepath = new HashSet<Newlandnotificationfilepath>();
          //  NewlandNotificationtype = new HashSet<NewlandNotificationtype>();
        }
        public int? NotificationTypeId { get; set; }
        public string NotificationNo { get; set; }
        public DateTime? Date { get; set; }
        public string GazetteNotificationFilePath { get; set; }
        public byte? IsActive { get; set; }
        public string Remarks { get; set; }

        public NewlandNotificationtype NewlandNotificationType { get; set; }
        public ICollection<Newlandnotificationfilepath> Newlandnotificationfilepath { get; set; }
      //  public ICollection<NewlandNotificationtype> NewlandNotificationtype { get; set; }

        [NotMapped]
        public List<NewlandNotificationtype> notificationtypeList { get; set; }
        [NotMapped]
        public List<IFormFile> NewlandNotificationFile { get; set; }
    }
}
