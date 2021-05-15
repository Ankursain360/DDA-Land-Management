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
            Newlandus17plot = new HashSet<Newlandus17plot>();
            Newlandus22plot = new HashSet<Newlandus22plot>();
            Newlandus4plot = new HashSet<Newlandus4plot>();
            Newlandus6plot = new HashSet<Newlandus6plot>();
        }
        public int? NotificationTypeId { get; set; }
        public string NotificationNo { get; set; }
        public DateTime? Date { get; set; }
        public string GazetteNotificationFilePath { get; set; }
        public byte? IsActive { get; set; }
        public string Remarks { get; set; }
        
        public NewlandNotificationtype NotificationType { get; set; }
        public ICollection<Newlandnotificationfilepath> Newlandnotificationfilepath { get; set; }
        public ICollection<Newlandus17plot> Newlandus17plot { get; set; }
        public ICollection<Newlandus22plot> Newlandus22plot { get; set; }
        public ICollection<Newlandus4plot> Newlandus4plot { get; set; }
        public ICollection<Newlandus6plot> Newlandus6plot { get; set; }   

        [NotMapped]
        public List<NewlandNotificationtype> notificationtypeList { get; set; }
        [NotMapped]
        public List<IFormFile> NewlandNotificationFile { get; set; }

       
    }
}
