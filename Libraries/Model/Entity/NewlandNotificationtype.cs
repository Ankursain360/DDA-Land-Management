using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class NewlandNotificationtype : AuditableEntity<int>
    {
        public NewlandNotificationtype()
        {
            Newlandnotification = new HashSet<Newlandnotification>();
        }
        public string NotificationType { get; set; }
        public byte? IsActive { get; set; }

        public ICollection<Newlandnotification> Newlandnotification { get; set; }
    }
    }
